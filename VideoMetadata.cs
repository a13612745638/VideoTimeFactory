using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 视频时间工厂
{
    public class VideoMetadata
    {

        public string VideoPath { get; set; } = string.Empty;

        public string VideoName { get { return Path.GetFileName(VideoPath); } }

        public int Width { get; set; }
        public int Height { get; set; }
        public double Duration { get; set; } // 单位：秒
        public required string Format { get; set; }
        public double FrameRate { get; set; }

    }
}
