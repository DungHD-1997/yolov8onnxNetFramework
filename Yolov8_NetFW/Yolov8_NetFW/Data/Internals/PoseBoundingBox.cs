using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Yolov8_NetFW.Metadata;

namespace Yolov8_NetFW.Data.Internals
{
    internal class PoseBoundingBox: BoundingBox, IPoseBoundingBox
    {
        public IReadOnlyList<IKeypoint> Keypoints { get; }

        public IKeypoint GetKeypoint(int index)
        {
            return Keypoints.SingleOrDefault(x => x.Index == index);
        }
        public PoseBoundingBox(YoloV8Class name,
                               Rectangle bounds,
                                float confidence,
                               IReadOnlyList<Keypoint> keypoints) : base(name, bounds, confidence)
        {
            this.Keypoints = keypoints;
            
        }
    }
}
