using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yolov8_NetFW.Data
{
    public interface IMask
    {
        
        float this[int x, int y] { get; }

        int Width { get; }

        int Height { get; }

        float GetConfidence(int x, int y);
    }
}
