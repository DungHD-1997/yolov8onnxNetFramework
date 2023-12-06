using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yolov8_NetFW.Data
{
    public interface IKeypoint
    {
        int Index { get; }

        Point Point { get; }

        float Confidence { get; }
    }
}
