using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yolov8_NetFW.Metadata
{
    public readonly struct KeypointShape
    {
        public int Count { get; }

        public int Channels { get; }
        public KeypointShape(int count, int channels)
        {
            Count = count;
            Channels = channels;
        }
    }
}
