using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace 视频时间工厂
{
    public class FFmpegHelper
    {
        private static readonly string ffmpegPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "ffmpeg",
            "bin",
            "ffmpeg.exe"
        );

        // 字幕文件模板
        private const string ASS_TEMPLATE = @"[Script Info]
ScriptType: V4.00+
PlayResY: {height}
PlayResX: {width}
PlayDepth: 0
Timer: 100.0000

[v4+ Styles]
Format: Name,Fontname,Fontsize,PrimaryColour,SecondaryColour,OutlineColour,BackColour,Bold,Italic,Underline,StrikeOut,ScaleX,ScaleY,Spacing,Angle,BorderStyle,Outline,Shadow,Alignment,MarginL,MarginR,MarginV
Style: AAbc,{font},{fontsize},&HF6F6F6,0,&H000000,&H000000,0,0,0,0,100,100,0,0,1,1,0,{alignment},{marginL},0,{marginV}

[Events]
Format: Layer, Start,End,Style,Name,MarginL,MarginR,MarginV, Effect, Text
{events}";

        /// <summary>
        /// 生成字幕文件并合并到视频
        /// </summary>
        public static void AddTimestampToVideo(
            VideoMetadata videoMetadata,
            string outputPath,
            string startTime,
            string font = "宋体",
            int fontSize = 50,
            int marginL = 50,
            int marginV = 30,
            int alignment = 7, // 7=左下角
            Action<int> progressCallback = null)
        {
            Task.Factory.StartNew(() =>
            {

                // 1. 验证FFmpeg是否存在
                if (!File.Exists(ffmpegPath))
                    throw new FileNotFoundException("FFmpeg未找到", ffmpegPath);

                // 2. 创建临时字幕文件
                string tempSubPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "outPut");
                if (!Directory.Exists(tempSubPath))
                    Directory.CreateDirectory(tempSubPath);
                tempSubPath = Path.Combine(tempSubPath, $"{videoMetadata.VideoName}.ass");
                try
                {
                    // 4. 生成字幕文件
                    GenerateAssSubtitle(
                        tempSubPath,
                        startTime,
                        videoMetadata.Duration,
                        font,
                        fontSize,
                        marginL,
                        marginV,
                        alignment,
                        videoMetadata.Width,
                        videoMetadata.Height
                    );

                    // 5. 合并视频和字幕（使用您提供的命令格式）
                    MergeVideoWithSubtitle(
                        videoMetadata.VideoPath,
                        tempSubPath,
                        outputPath,
                        progressCallback
                    );
                }
                finally
                {
                    // 6. 清理临时文件
                    if (File.Exists(tempSubPath))
                        File.Delete(tempSubPath);
                }
            });
        }

        /// <summary>
        /// 生成ASS字幕文件
        /// </summary>
        private static void GenerateAssSubtitle(
            string outputPath,
            string startTime,
            double videoDuration,
            string font,
            int fontSize,
            int marginL,
            int marginV,
            int alignment,
            int width,
            int height)
        {
            DateTime start = DateTime.Parse(startTime);
            StringBuilder events = new StringBuilder();

            // 生成每秒一条的字幕事件
            for (int i = 0; i < Math.Ceiling(videoDuration); i++)
            {
                DateTime currentTime = start.AddSeconds(i);
                string timeStr = currentTime.ToString("yyyy年MM月dd日 HH:mm:ss");

                events.AppendLine(
                    $"Dialogue: 0," +
                    $"{TimeSpan.FromSeconds(i):hh\\:mm\\:ss\\.ff}," +
                    $"{TimeSpan.FromSeconds(i + 1):hh\\:mm\\:ss\\.ff}," +
                    $"AAbc,,0000,0000,0000,,{timeStr}");
            }

            // 填充模板并保存
            string assContent = ASS_TEMPLATE
                .Replace("{width}", width.ToString())
                .Replace("{height}", height.ToString())
                .Replace("{font}", font)
                .Replace("{fontsize}", fontSize.ToString())
                .Replace("{marginL}", marginL.ToString())
                .Replace("{marginV}", marginV.ToString())
                .Replace("{alignment}", alignment.ToString())
                .Replace("{events}", events.ToString());

            File.WriteAllText(outputPath, assContent, Encoding.UTF8);
        }

        /// <summary>
        /// 合并视频和字幕（使用您提供的命令格式）
        /// </summary>
        /// <summary>
        /// 合并视频和字幕（完全匹配手动执行的命令格式）
        /// </summary>
        private static void MergeVideoWithSubtitle(
            string videoPath,
            string subtitlePath,
            string outputPath,
            Action<int> progressCallback)
        {
            // 确保输出目录存在
            Directory.CreateDirectory(Path.GetDirectoryName(outputPath));

            // 使用原始路径（不进行转义处理）
            // 手动测试成功的格式：subtitles='C\:/Users/.../tmp.ass'
            string arguments = $"-y -nostdin -i \"{videoPath}\" " +
                              $"-vf \"subtitles='{EscapeForFFmpeg(subtitlePath)}'\" " +
                              $"-c:v libx264 -preset medium -crf 23 -pix_fmt yuv420p " +
                              $"-c:a copy \"{outputPath}\"";

            // 调试：输出完整命令
            Debug.WriteLine($"执行命令: {ffmpegPath} {arguments}");

            using (var process = new Process())
            {
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = ffmpegPath,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    StandardOutputEncoding = Encoding.UTF8,
                    StandardErrorEncoding = Encoding.UTF8
                };

                // 进度跟踪
                double totalDuration = 0;
                var lastProgress = -1;
                var progressRegex = new Regex(@"time=(\d{2}:\d{2}:\d{2}\.\d+)");
                var durationRegex = new Regex(@"Duration: (\d{2}:\d{2}:\d{2}\.\d+)");

                // 输出缓冲区
                var output = new StringBuilder();
                var error = new StringBuilder();

                process.OutputDataReceived += (s, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        output.AppendLine(e.Data);
                        Debug.WriteLine($"[Output] {e.Data}");
                    }
                };

                process.ErrorDataReceived += (s, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        error.AppendLine(e.Data);
                        Debug.WriteLine($"[Error] {e.Data}");

                        // 解析时长
                        if (totalDuration <= 0)
                        {
                            var match = durationRegex.Match(e.Data);
                            if (match.Success && TimeSpan.TryParse(match.Groups[1].Value, out var ts))
                            {
                                totalDuration = ts.TotalSeconds;
                            }
                        }

                        // 解析进度
                        if (totalDuration > 0)
                        {
                            var match = progressRegex.Match(e.Data);
                            if (match.Success && TimeSpan.TryParse(match.Groups[1].Value, out var currentTime))
                            {
                                int progress = (int)(currentTime.TotalSeconds / totalDuration * 100);
                                progress = Math.Clamp(progress, 0, 100);

                                if (progress != lastProgress)
                                {
                                    lastProgress = progress;
                                    progressCallback?.Invoke(progress);
                                }
                            }
                        }
                    }
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                // 设置超时（2小时）
                bool exited = process.WaitForExit(7200000);

                if (!exited)
                {
                    process.Kill();
                    throw new TimeoutException("FFmpeg处理超时（超过2小时）");
                }

                if (process.ExitCode != 0)
                {
                    throw new Exception($"FFmpeg失败 (ExitCode={process.ExitCode})\n{error}");
                }

                progressCallback?.Invoke(100);

                File.Delete(subtitlePath);
            }
        }

        /// <summary>
        /// 特殊路径转义处理（针对字幕路径）
        /// </summary>
        private static string EscapeForFFmpeg(string path)
        {
            // Windows路径转义规则
            return path
                .Replace("\\", "/")  // 反斜杠转双反斜杠
                .Replace(":", "\\:");  // 冒号转义
        }
    }
}
