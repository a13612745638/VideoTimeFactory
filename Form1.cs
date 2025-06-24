using System.Diagnostics;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace 视频时间工厂
{

    public partial class Form1 : Form
    {
        private VideoMetadata metadata = null;

        private static object lockObject = new object();
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern int ShellExecute(IntPtr hWnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, int nShowCmd);

        public Form1()
        {
            InitializeComponent();
            LoadFonts();
        }

        private void LoadFonts()
        {
            // 创建一个字体集合
            using (InstalledFontCollection fontCollection = new InstalledFontCollection())
            {
                // 获取所有已安装的字体
                FontFamily[] fontFamilies = fontCollection.Families;

                bool isFontFound = false;
                // 将字体名称添加到 ListBox
                foreach (FontFamily font in fontFamilies)
                {
                    if (font.Name == "IPix")
                    {
                        isFontFound = true;
                    }
                    comboBoxFonts.Items.Add(font.Name);
                }
                if (isFontFound)
                {
                    comboBoxFonts.Text = "IPix";
                }
                else
                {
                    comboBoxFonts.Text = "宋体";
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "视频文件|*.mp4;*.avi;*.mkv;*.mov;*.flv|所有文件|*.*";
            openFileDialog.Title = "选择一个视频";

            // 显示对话框并检查用户是否点击了“确定”
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            // 获取选择的文件路径
            string videoPath = openFileDialog.FileName;
            labelVideoPath.Text = videoPath;
            // 方法 1: 使用 FFProbe

            metadata = FFprobeHelper.GetVideoMetadata(videoPath);

            labelVideoAspect.Text = $"{metadata.Width} x {metadata.Height}";
            labelVideoDuration.Text = $"{metadata.Duration:F2} 秒";
            labelVideoFormat.Text = metadata.Format ?? "未知格式";
            labelVideoFrameRate.Text = metadata.FrameRate > 0 ? $"{metadata.FrameRate} FPS" : "未知帧率";
            toolStripStatusLabel2.Text = "视频已加载，请设置时间戳。";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (metadata == null)
            {
                MessageBox.Show("请先导入视频！");
            }
            progressBar1.Value = 0;

            // 获取用户选择的日期和时间
            DateTime selectedDate = dateTimePickerDate.Value;
            DateTime selectedTime = dateTimePickerTime.Value;

            // 合并日期和时间
            DateTime combinedDateTime = selectedDate.Date.Add(selectedTime.TimeOfDay);
            int tempProgress = 0;

            // 进度回调
            Action<int> progressHandler = progress =>
            {
                lock (lockObject)
                {
                    if (progress <= tempProgress)
                    {
                        return;
                    }
                    tempProgress = progress;
                    toolStripStatusLabel2.Text = $"当前进度 {progress}%";
                    if (progressBar1.InvokeRequired)
                    {
                        progressBar1.Invoke(new Action(() => progressBar1.Value = progress));
                    }
                    else
                    {
                        progressBar1.Value = progress;
                    }
                    if (progress == 100)
                    {
                        string msg = "视频保存完成，视频目录位置：" + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "outPut", metadata.VideoName ?? "output.mp4");
                        MessageBox.Show(msg);
                        // 调用 ShellExecute 打开文件夹
                        toolStripStatusLabel2.Text = msg;
                        ShellExecute(IntPtr.Zero, "open", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "outPut"), null, null, 1);
                    }
                }

            };

            FFmpegHelper.AddTimestampToVideo(
                videoMetadata: metadata,
                outputPath: Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "outPut", metadata.VideoName ?? "output.mp4"),
                startTime: combinedDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                font: comboBoxFonts.Text,      // 使用您指定的字体
                fontSize: int.Parse(textBoxFontsSize.Text),       // 字体大小
                marginL: int.Parse(textBoxVideoLeft.Text),      // 左边距（从您的字幕文件）
                marginV: int.Parse(textBoxVideoTop.Text),       // 上边距（从您的字幕文件）
                alignment: 7,       // 7=左下角
                progressCallback: progressHandler
            );

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShellExecute(IntPtr.Zero, "open", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "outPut"), null, null, 1);
        }
    }
}
