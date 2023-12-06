using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yolov8_NetFW.Timing;
using Yolov8_NetFW.Extensions;
namespace Yolov8_NetFW.Data.Internals
{
    internal class DetectionResult: YoloV8Result, IDetectionResult
    
    {
        public IReadOnlyList<IBoundingBox> Boxes { get; }
        public DetectionResult(Size image,
                               SpeedResult speed,
                               IReadOnlyList<IBoundingBox> boxes) : base(image, speed)
        {
            this.Boxes = boxes;
        }
        public override string ToString()
        {
            return Boxes.Summary();
        }
    }
}
