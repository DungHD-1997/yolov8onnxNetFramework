using Microsoft.ML.OnnxRuntime.Tensors;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yolov8_NetFW.Metadata;
using Yolov8_NetFW.Utilities;

namespace Yolov8_NetFW.Parsers
{
    internal readonly struct IndexedBoundingBoxParser
    {
        private  YoloV8Metadata _metadata { get; }
        private  YoloV8Parameters _parameters { get; }
        public IndexedBoundingBoxParser(YoloV8Metadata metadata, YoloV8Parameters parameters)
        {
            _metadata = metadata;
            _parameters = parameters;
        }

        public IReadOnlyList<IndexedBoundingBox> Parse(Tensor<float> output, Size originSize)
        {
            int xPadding;
            int yPadding;

            if (_parameters.KeepOriginalAspectRatio)
            {
                var reductionRatio = Math.Min(_metadata.ImageSize.Width / (float)originSize.Width,
                                              _metadata.ImageSize.Height / (float)originSize.Height);

                xPadding = (int)((_metadata.ImageSize.Width - originSize.Width * reductionRatio) / 2);
                yPadding = (int)((_metadata.ImageSize.Height - originSize.Height * reductionRatio) / 2);
            }
            else
            {
                xPadding = 0;
                yPadding = 0;
            }

            return Parse(output, originSize, xPadding, yPadding);
        }

        public IReadOnlyList<IndexedBoundingBox> Parse(Tensor<float> output, Size originSize, int xPadding, int yPadding)
        {
            var metadata = _metadata;

            var xRatio = (float)originSize.Width / metadata.ImageSize.Width;
            var yRatio = (float)originSize.Height / metadata.ImageSize.Height;

            if (_parameters.KeepOriginalAspectRatio)
            {
                var maxRatio = Math.Max(xRatio, yRatio);

                xRatio = maxRatio;
                yRatio = maxRatio;
            }

            return Parse(output, originSize, xPadding, yPadding, xRatio, yRatio);
        }

        public IReadOnlyList<IndexedBoundingBox> Parse(Tensor<float> output, Size originSize, int xPadding, int yPadding, float xRatio, float yRatio)
        {
            var metadata = _metadata;
            var parameters = _parameters;

            var boxes = new IndexedBoundingBox[output.Dimensions[2]];

            Parallel.For(0, output.Dimensions[2], i =>
            {
                for (int j = 0; j < metadata.Classes.Count; j++)
                {
                    var confidence = output[0, j + 4, i];

                    if (confidence <= parameters.Confidence)
                        continue;

                    var x = output[0, 0, i];
                    var y = output[0, 1, i];
                    var w = output[0, 2, i];
                    var h = output[0, 3, i];

                    var xMin = (int)((x - w / 2 - xPadding) * xRatio);
                    var yMin = (int)((y - h / 2 - yPadding) * yRatio);
                    var xMax = (int)((x + w / 2 - xPadding) * xRatio);
                    var yMax = (int)((y + h / 2 - yPadding) * yRatio);

                    xMin = Clamp(xMin, 0, originSize.Width);
                    yMin = Clamp(yMin, 0, originSize.Height);
                    xMax = Clamp(xMax, 0, originSize.Width);
                    yMax = Clamp(yMax, 0, originSize.Height);

                    var bounds = Rectangle.FromLTRB(xMin, yMin, xMax, yMax);
                    var name = metadata.Classes[j];

                    boxes[i] = new IndexedBoundingBox(i, name, bounds, confidence);
                }
            });

            var selected = NonMaxSuppressionHelper.Suppress(boxes.Where(x => x.IsEmpty == false).ToList(), _parameters.IoU);

            return selected;
        }
        private static int Clamp(int value, int min, int max)
        {
            return Math.Max(min, Math.Min(value, max));
        }
    }
}
