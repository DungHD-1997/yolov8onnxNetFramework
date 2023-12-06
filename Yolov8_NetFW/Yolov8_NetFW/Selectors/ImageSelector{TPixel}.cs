using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp.Processing;

namespace Yolov8_NetFW.Selectors
{
    public class ImageSelector<TPixel> where TPixel : unmanaged, IPixel<TPixel>
    {
        private readonly Func<Image<TPixel>> _factory;

        public ImageSelector(Image image)
        {
            _factory = image.CloneAs<TPixel>;
        }

        public ImageSelector(string path)
        {
            _factory = () => Image.Load<TPixel>(path);
        }

        public ImageSelector(byte[] data)
        {
            _factory = () => Image.Load<TPixel>(data);
        }

        public ImageSelector(Stream stream)
        {
            _factory = () => Image.Load<TPixel>(stream);
        }

        internal Image<TPixel> Load(bool autoOrient)
        {
            var image = _factory();

            if (autoOrient)
                image.Mutate(x => x.AutoOrient());

            return image;
        }

        public static implicit operator ImageSelector<TPixel>(Image image) => new ImageSelector<TPixel>(image);
        public static implicit operator ImageSelector<TPixel>(string path) => new ImageSelector<TPixel>(path);
        public static implicit operator ImageSelector<TPixel>(byte[] data) => new ImageSelector<TPixel>(data);
        public static implicit operator ImageSelector<TPixel>(Stream stream) => new ImageSelector<TPixel>(stream);
    }
}
