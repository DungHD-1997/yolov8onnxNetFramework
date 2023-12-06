using Microsoft.ML.OnnxRuntime.Tensors;
using Microsoft.ML.OnnxRuntime;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yolov8_NetFW.Metadata;
using Yolov8_NetFW.Selectors;
using Yolov8_NetFW.Timing;
using Yolov8_NetFW.Utilities;

namespace Yolov8_NetFW
{
    public class YoloV8 : IDisposable
    {
        #region Private Memebers

        private readonly YoloV8Metadata _metadata;
        private readonly YoloV8Parameters _parameters;

        private readonly InferenceSession _inference;
        private readonly string[] _inputNames;

        private readonly object _sync = new object();

        private bool _disposed;

        #endregion

        #region Public Properties

        public YoloV8Metadata Metadata => _metadata;

        public YoloV8Parameters Parameters => _parameters;

        #endregion

        #region Ctors

        public YoloV8(ModelSelector selector)
            : this(selector.Load(), null, null)
        { }

        public YoloV8(ModelSelector selector, SessionOptions options)
            : this(selector.Load(), null, options)
        { }

        public YoloV8(ModelSelector selector, YoloV8Metadata metadata)
            : this(selector.Load(), metadata, null)
        { }

        public YoloV8(ModelSelector selector, YoloV8Metadata metadata, SessionOptions options)
            : this(selector.Load(), metadata, options)
        { }

        private YoloV8(byte[] model, YoloV8Metadata metadata, SessionOptions options)
        {
            //if (metadata == null)
            //{
            //    Console.WriteLine("metadata is null for debug!");
            //    return;
            //}
            _inference = new InferenceSession(model, options ?? new SessionOptions());
            _inputNames = _inference.InputMetadata.Keys.ToArray();

            _metadata = metadata ?? YoloV8Metadata.Parse(_inference.ModelMetadata.CustomMetadataMap);
            _parameters = YoloV8Parameters.Default;
        }

        #endregion

        #region Run

        public TResult Run<TResult>(ImageSelector selector, PostprocessContext<TResult> postprocess)
        {
            var image = selector.Load(true);

            var originSize = image.Size();

            var timer = new SpeedTimer();

            timer.StartPreprocess();

            var input = Preprocess(image);

            var inputs = MapNamedOnnxValues(new ReadOnlySpan<Tensor<float>>(new[] { input }));

            timer.StartInference();

            var outputs = Infer(inputs);

            var list = new List<NamedOnnxValue>(outputs);
            outputs.Dispose();
            timer.StartPostprocess();
            image.Dispose();
            return postprocess(list, originSize, timer);
        }

        #endregion

        #region Private Methods

        private IDisposableReadOnlyCollection<DisposableNamedOnnxValue> Infer(IReadOnlyCollection<NamedOnnxValue> inputs)
        {
            if (_parameters.SuppressParallelInference)
            {
                lock (_sync)
                {
                    return _inference.Run(inputs);
                }
            }

            return _inference.Run(inputs);
        }

        private Tensor<float> Preprocess(Image<Rgb24> image)
        {
            var modelSize = _metadata.ImageSize;

            var dimensions = new int[] { 1, 3, modelSize.Height, modelSize.Width };
            var input = new DenseTensor<float>(dimensions);

            PreprocessHelper.ProcessToTensor(image, modelSize, _parameters.KeepOriginalAspectRatio, input, 0);

            return input;
        }

        private NamedOnnxValue[] MapNamedOnnxValues(ReadOnlySpan<Tensor<float>> inputs)
        {
            var length = inputs.Length;

            var values = new NamedOnnxValue[length];

            for (int i = 0; i < length; i++)
            {
                var name = _inputNames[i];

                var value = NamedOnnxValue.CreateFromTensor(name, inputs[i]);

                values[i] = value;
            }

            return values;
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            if (_disposed)
                return;

            _inference.Dispose();
            _disposed = true;

            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
