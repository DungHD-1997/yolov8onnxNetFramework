using Microsoft.ML.OnnxRuntime.Tensors;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yolov8_NetFW.Data.Internals;
using Yolov8_NetFW.Data;
using Yolov8_NetFW.Metadata;
using Yolov8_NetFW.Extensions;

namespace Yolov8_NetFW.Parsers
{
    internal struct DetectionOutputParser
    {
        private readonly YoloV8Metadata _metadata;
        private readonly YoloV8Parameters _parameters;

        public DetectionOutputParser(YoloV8Metadata metadata, YoloV8Parameters parameters)
        {
            _metadata = metadata;
            _parameters = parameters;
        }

        public List<IBoundingBox> Parse(Tensor<float> output, Size originSize)
        {
            var boxes = new IndexedBoundingBoxParser(_metadata, _parameters).Parse(output, originSize);

            return new List<IBoundingBox>(boxes.SelectParallely(CreateBoundingBox));

        }

        private static BoundingBox CreateBoundingBox(IndexedBoundingBox value)
        {
            return new BoundingBox(value.Class, value.Bounds, value.Confidence);
        }
    }

}
