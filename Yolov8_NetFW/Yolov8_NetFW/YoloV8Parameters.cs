using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yolov8_NetFW
{
    public class YoloV8Parameters
    {
        public static readonly YoloV8Parameters Default = new YoloV8Parameters();

        public float Confidence { get; set; }

        public float IoU { get; set; }

        public bool KeepOriginalAspectRatio { get; set; }

        public bool SuppressParallelInference { get; set; }

        public YoloV8Parameters()
        {
            Confidence = .3f;
            IoU = .45f;
            KeepOriginalAspectRatio = true;
            SuppressParallelInference = false;
        }
    }
}
