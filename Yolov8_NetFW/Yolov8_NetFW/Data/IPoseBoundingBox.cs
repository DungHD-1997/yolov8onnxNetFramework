using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yolov8_NetFW.Data
{
    public interface IPoseBoundingBox : IBoundingBox
    {
        IReadOnlyList<IKeypoint> Keypoints { get; }

        IKeypoint GetKeypoint(int id);
    }
}
