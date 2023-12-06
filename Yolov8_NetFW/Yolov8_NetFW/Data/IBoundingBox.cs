using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yolov8_NetFW.Metadata;

namespace Yolov8_NetFW.Data
{
    public interface IBoundingBox
    {
        YoloV8Class Class { get; }

        Rectangle Bounds { get; }

        float Confidence { get; }
    }
}
