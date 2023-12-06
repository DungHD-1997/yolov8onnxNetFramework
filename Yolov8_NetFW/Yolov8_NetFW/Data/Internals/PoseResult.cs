using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yolov8_NetFW.Extensions;
using Yolov8_NetFW.Timing;

namespace Yolov8_NetFW.Data.Internals
{
    internal class PoseResult: YoloV8Result, IPoseResult
    {
        public IReadOnlyList<IPoseBoundingBox> Boxes { get; }

        public override string ToString()
        {
            return Boxes.Summary();
        }
        public PoseResult(Size image,
                          SpeedResult speed,
                          IReadOnlyList<IPoseBoundingBox> boxes): base(image, speed){
            Boxes = boxes;
        
        }
    }
}
