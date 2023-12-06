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
    internal class SegmentationResult: YoloV8Result, ISegmentationResult
    {
        public IReadOnlyList<ISegmentationBoundingBox> Boxes { get; }
        public SegmentationResult(Size image,
                                  SpeedResult speed,
                                  IReadOnlyList<ISegmentationBoundingBox> boxes) : base (image, speed){
            Boxes = boxes;
        }
        public override string ToString()
        {
            return Boxes.Summary();
        }
    }
}
