using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yolov8_NetFW.Metadata
{
    public class YoloV8PoseMetadata : YoloV8Metadata
    {
        public KeypointShape KeypointShape { get; }

        public YoloV8PoseMetadata(string author,
                                  string description,
                                  string version,
                                  YoloV8Task task,
                                  int batch,
                                  Size imageSize,
                                  IReadOnlyList<YoloV8Class> classes,
                                  KeypointShape keypointShape)
            : base(author,
                   description,
                   version,
                   task,
                   batch,
                   imageSize,
                   classes)
        {
            KeypointShape = keypointShape;
        }
    }
}
