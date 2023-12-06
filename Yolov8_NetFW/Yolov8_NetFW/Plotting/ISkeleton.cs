using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yolov8_NetFW.Plotting
{
    public interface ISkeleton
    {
        IReadOnlyList<SkeletonConnection> Connections { get; }

        Color GetKeypointColor(int index);

        Color GetLineColor(int index);
    }
}
