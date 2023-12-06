using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yolov8_NetFW.Metadata;
using Yolov8_NetFW.Timing;

namespace Yolov8_NetFW.Data.Internals
{
    internal class ClassificationResult : YoloV8Result, IClassificationResult
    {
        public YoloV8Class Class { get; }

        public float Confidence { get; }

        public IReadOnlyList<(YoloV8Class Class, float Confidence)> Probabilities { get; }

        public ClassificationResult(Size image,
                                    SpeedResult speed,
                                    IReadOnlyList<(YoloV8Class Class, float Confidence)> probabilities)
            : base(image, speed)
        {
            //var top = probabilities.MaxBy(x => x.Confidence);
            var top = probabilities.OrderByDescending(x => x.Confidence).FirstOrDefault();

            Class = top.Class;
            Confidence = top.Confidence;

            Probabilities = probabilities;
        }

        public override string ToString()
        {
            return $"{Class.Name} ({Confidence:N})";
        }
    }
}
