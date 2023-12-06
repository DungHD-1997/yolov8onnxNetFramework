using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yolov8_NetFW.Data.Internals
{
    internal class Mask : IMask
    {
        private readonly float[,] _xy;

        public float this[int x, int y] => _xy[x, y];

        public int Width { get; }

        public int Height { get; }
        public Mask(float[,] xy) { 
            this._xy = xy;
            this.Width = xy.GetLength(0);
            this.Height = xy.GetLength(1);
        }
        public float GetConfidence(int x, int y)
        {
            return _xy[x, y];
        }
    }
}
