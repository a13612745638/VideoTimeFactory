using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace 视频时间工厂
{
    public class FFprobeHelper
    {
        private static readonly string ffmpegBasePath = AppDomain.CurrentDomain.BaseDirectory + "/ffmpeg";
        private static readonly string ffprobePath = Path.Combine(ffmpegBasePath, "bin", "ffprobe.exe");

        public static VideoMetadata GetVideoMetadata(string videoPath)
        {
            // 1. 验证文件是否存在
            if (!File.Exists(videoPath))
                throw new FileNotFoundException("视频文件不存在", videoPath);

            // 2. 验证 FFprobe 是否存在
            if (!File.Exists(ffprobePath))
                throw new FileNotFoundException($"FFprobe 未找到: {ffprobePath}");

            // 3. 构建命令参数（JSON 格式输出）
            string arguments = $"-v error -select_streams v:0 " +
                               $"-show_entries stream=width,height,r_frame_rate " +
                               $"-show_entries format=duration,format_name " +
                               $"-of json \"{videoPath}\"";

            // 4. 配置进程
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = ffprobePath,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    StandardOutputEncoding = System.Text.Encoding.UTF8,
                    StandardErrorEncoding = System.Text.Encoding.UTF8
                }
            };

            // 5. 启动进程并获取输出
            try
            {
                process.Start();
                string jsonOutput = process.StandardOutput.ReadToEnd();
                string errorOutput = process.StandardError.ReadToEnd();
                bool exited = process.WaitForExit(5000); // 最多等待5秒

                // 处理未正常退出的情况
                if (!exited)
                {
                    process.Kill();
                    throw new TimeoutException("FFprobe 执行超时");
                }

                // 6. 检查执行结果
                if (process.ExitCode != 0)
                    throw new Exception($"FFprobe 错误 (ExitCode={process.ExitCode}): {errorOutput}");

                if (string.IsNullOrWhiteSpace(jsonOutput))
                    throw new Exception("FFprobe 未返回有效数据");

                // 7. 解析 JSON 数据
                return ParseFfprobeOutput(jsonOutput, videoPath);
            }
            finally
            {
                process.Close();
            }
        }

        private static VideoMetadata ParseFfprobeOutput(string jsonOutput, string videoPath)
        {
            try
            {
                using JsonDocument doc = JsonDocument.Parse(jsonOutput);
                JsonElement root = doc.RootElement;

                // 获取视频流信息
                JsonElement stream = root.GetProperty("streams")[0];

                // 获取格式信息
                JsonElement format = root.GetProperty("format");

                return new VideoMetadata
                {
                    Width = stream.GetProperty("width").GetInt32(),
                    Height = stream.GetProperty("height").GetInt32(),
                    Duration = ParseDuration(format),
                    FrameRate = ParseFrameRate(stream),
                    Format = format.GetProperty("format_name").GetString() ?? "未知格式",
                    VideoPath = videoPath
                };
            }
            catch (Exception ex)
            {
                throw new Exception("解析 FFprobe 输出失败", ex);
            }
        }

        private static double ParseDuration(JsonElement format)
        {
            // 尝试两种可能的时长字段
            if (format.TryGetProperty("duration", out var durationProp))
                return double.Parse(durationProp.GetString() ?? "0");

            if (format.TryGetProperty("DURATION", out durationProp))
                return double.Parse(durationProp.GetString() ?? "0");

            throw new Exception("未找到视频时长字段");
        }

        private static double ParseFrameRate(JsonElement stream)
        {
            // 解析帧率 (格式如 "30/1" 或 "29.97")
            string frameRateStr = stream.GetProperty("r_frame_rate").GetString() ?? "";

            // 处理分数形式
            if (frameRateStr.Contains("/"))
            {
                string[] parts = frameRateStr.Split('/');
                return double.Parse(parts[0]) / double.Parse(parts[1]);
            }

            // 处理小数形式
            return double.Parse(frameRateStr);
        }

    }

    
}
