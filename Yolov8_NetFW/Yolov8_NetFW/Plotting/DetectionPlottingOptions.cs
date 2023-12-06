using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yolov8_NetFW.Plotting
{
    public class DetectionPlottingOptions : PlottingOptions
    {
        public static DetectionPlottingOptions Default { get; } = new DetectionPlottingOptions();

        public float TextHorizontalPadding { get; set; }

        public float BoxBorderThickness { get; set; }

        public ColorPalette ColorPalette { get; set; }

        public DetectionPlottingOptions()
        {
            TextHorizontalPadding = 5F;
            BoxBorderThickness = 1F;
            ColorPalette = ColorPalette.Default;
        }
    }
}
