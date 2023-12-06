using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yolov8_NetFW.Plotting
{
    public class SegmentationPlottingOptions : DetectionPlottingOptions
    {
        public static new SegmentationPlottingOptions Default { get; } = new SegmentationPlottingOptions();

        public float MaskConfidence { get; set; }

        public float ContoursThickness { get; set; }

        public SegmentationPlottingOptions()
        {
            MaskConfidence = .5F;
            ContoursThickness = 1f;
        }
    }
}
