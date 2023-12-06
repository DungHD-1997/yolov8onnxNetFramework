using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Yolov8_NetFW.Data.Internals
{
    internal class Keypoint : IKeypoint
    {
        public int Index { get; }

        public Point Point { get; }

        public float Confidence { get; }
        public Keypoint(int index, int x, int y, float confidence)
        {
            this.Index = index;
            this.Point = new Point(x, y);
            this.Confidence = confidence;
        }
    }
}
