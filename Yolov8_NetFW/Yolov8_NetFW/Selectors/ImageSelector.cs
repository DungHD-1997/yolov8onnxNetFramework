using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yolov8_NetFW.Selectors
{
    public class ImageSelector : ImageSelector<Rgb24>
    {
        public ImageSelector(Image image)
        : base(image) { }

        public ImageSelector(string path)
            : base(path) { }

        public ImageSelector(byte[] data)
            : base(data) { }

        public ImageSelector(Stream stream)
            : base(stream) { }

        public static implicit operator ImageSelector(Image image) => new ImageSelector(image);

        public static implicit operator ImageSelector(string path) => new ImageSelector(path);

        public static implicit operator ImageSelector(byte[] data) => new ImageSelector(data);

        public static implicit operator ImageSelector(Stream stream) => new ImageSelector(stream);
    }
}
