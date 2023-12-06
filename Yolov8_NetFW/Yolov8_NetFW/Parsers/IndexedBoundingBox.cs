using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yolov8_NetFW.Metadata;

namespace Yolov8_NetFW.Parsers
{
    internal readonly struct IndexedBoundingBox
    {
        internal bool IsEmpty => Bounds.IsEmpty;

        public int Index { get; }

        public YoloV8Class Class { get; }

        public Rectangle Bounds { get; }

        public float Confidence { get; }
        public IndexedBoundingBox(int index, YoloV8Class name, Rectangle bounds, float confidence)
        {
            Index = index;
            Class = name;
            Bounds = bounds;
            Confidence = confidence;

        }
    }
}
