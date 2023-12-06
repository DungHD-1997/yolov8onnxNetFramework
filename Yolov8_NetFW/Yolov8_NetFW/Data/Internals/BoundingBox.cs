using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yolov8_NetFW.Metadata;
namespace Yolov8_NetFW.Data.Internals
{
    internal class BoundingBox : IBoundingBox
    {
        public YoloV8Class Class { get; }

        public Rectangle Bounds { get; }

        public float Confidence { get; }
        public BoundingBox(YoloV8Class name, Rectangle bounds, float confidence)
        {
            this.Class = name;
            this.Bounds = bounds;
            this.Confidence = confidence;  
        }
        public override string ToString()
        {
            return $"{Class.Name} ({Confidence:N})";
        }
    }
}
