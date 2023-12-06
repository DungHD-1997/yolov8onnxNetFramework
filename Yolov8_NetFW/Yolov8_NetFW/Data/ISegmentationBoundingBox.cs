using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yolov8_NetFW.Data
{
    public interface ISegmentationBoundingBox : IBoundingBox
    {
        IMask Mask { get; }
    }
}
