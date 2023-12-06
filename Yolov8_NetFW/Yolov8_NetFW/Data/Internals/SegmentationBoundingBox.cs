using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yolov8_NetFW.Metadata;

namespace Yolov8_NetFW.Data.Internals
{
    internal class SegmentationBoundingBox: BoundingBox, ISegmentationBoundingBox
    {
        public IMask Mask { get; }
        public SegmentationBoundingBox(YoloV8Class name,
                                       Rectangle bounds,
                                       float confidence,
                                       IMask mask): base(name, bounds, confidence) { 
            Mask = mask;
                
        }    
    }
}
