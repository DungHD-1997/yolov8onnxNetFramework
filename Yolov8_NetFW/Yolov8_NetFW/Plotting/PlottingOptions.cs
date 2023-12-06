using SixLabors.Fonts;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Yolov8_NetFW.Plotting
{
    public abstract class PlottingOptions
    {
        public FontFamily FontFamily { get; set; }

        public float FontSize { get; set; }

        public PlottingOptions()
        {
            FontFamily = GetDefaultFontFamily();
            FontSize = 12F;
        }

        private static FontFamily GetDefaultFontFamily()
        {
            //if (OperatingSystem.IsWindows())
            //    return SystemFonts.Get("Arial");

            //if (OperatingSystem.IsAndroid)
            //    return SystemFonts.Get("Robot");

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return SystemFonts.Get("Arial");
            }

            return SystemFonts.Families.First();
        }
    }
}
