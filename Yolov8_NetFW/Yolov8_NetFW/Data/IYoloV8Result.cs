using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yolov8_NetFW.Timing;

namespace Yolov8_NetFW.Data
{
    public interface IYoloV8Result
    {
        Size Image { get; }

        SpeedResult Speed { get; }
    }
}
