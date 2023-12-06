using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yolov8_NetFW.Timing;

namespace Yolov8_NetFW.Data.Internals
{
    internal abstract class YoloV8Result : IYoloV8Result
    {
        public Size Image { get; }

        public SpeedResult Speed { get; }
        protected YoloV8Result(Size image, SpeedResult speed)
        { 
            this.Image = image;
            this.Speed = speed;
        }
    }
}
