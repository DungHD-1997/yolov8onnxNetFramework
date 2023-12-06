using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yolov8_NetFW.Data;

namespace Yolov8_NetFW.Plotting
{
    public static class PlottingAsyncOperationExtensions
    {
        #region Pose

        public static async Task<Image> PlotImageAsync(this IPoseResult result, Image originImage)
        {
            return await Task.Run(() => result.PlotImage(originImage));
        }

        public static async Task<Image> PlotImageAsync(this IPoseResult result, Image originImage, PosePlottingOptions options)
        {
            return await Task.Run(() => result.PlotImage(originImage, options));
        }

        #endregion

        #region Detection

        public static async Task<Image> PlotImageAsync(this IDetectionResult result, Image originImage)
        {
            return await Task.Run(() => result.PlotImage(originImage));
        }

        public static async Task<Image> PlotImageAsync(this IDetectionResult result, Image originImage, DetectionPlottingOptions options)
        {
            return await Task.Run(() => result.PlotImage(originImage, options));
        }

        #endregion

        #region Segmentation

        public static async Task<Image> PlotImageAsync(this ISegmentationResult result, Image originImage)
        {
            return await Task.Run(() => result.PlotImage(originImage));
        }

        public static async Task<Image> PlotImageAsync(this ISegmentationResult result, Image originImage, SegmentationPlottingOptions options)
        {
            return await Task.Run(() => result.PlotImage(originImage, options));
        }

        #endregion

        #region Classification

        public static async Task<Image> PlotImageAsync(this IClassificationResult result, Image originImage)
        {
            return await Task.Run(() => result.PlotImage(originImage));
        }

        public static async Task<Image> PlotImageAsync(this IClassificationResult result, Image originImage, ClassificationPlottingOptions options)
        {
            return await Task.Run(() => result.PlotImage(originImage, options));
        }

        #endregion
    }
}
