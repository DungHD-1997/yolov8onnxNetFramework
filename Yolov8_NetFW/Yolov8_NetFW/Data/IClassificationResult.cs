using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yolov8_NetFW.Metadata;

namespace Yolov8_NetFW.Data
{
    public interface IClassificationResult : IYoloV8Result
    {
        YoloV8Class Class { get; }

        float Confidence { get; }

        IReadOnlyList<(YoloV8Class Class, float Confidence)> Probabilities { get; }
    }
}
