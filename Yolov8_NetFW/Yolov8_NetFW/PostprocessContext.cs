using Microsoft.ML.OnnxRuntime;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yolov8_NetFW.Timing;

namespace Yolov8_NetFW
{
    public delegate TResult PostprocessContext<TResult>(IReadOnlyList<NamedOnnxValue> outputs,
                                                        Size imageSize,
                                                        SpeedTimer timer);
}
