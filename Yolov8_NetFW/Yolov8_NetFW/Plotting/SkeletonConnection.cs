using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yolov8_NetFW.Plotting
{
    public readonly struct SkeletonConnection
    {
        public int First { get; }

        public int Second { get; }
        public SkeletonConnection(int first, int second)
        {
            First = first;
            Second = second;
        }
    }
}
