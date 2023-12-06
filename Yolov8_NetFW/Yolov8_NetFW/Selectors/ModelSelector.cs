using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yolov8_NetFW.Selectors
{
    public class ModelSelector
    {
        private readonly Func<byte[]> _factory;

        public ModelSelector(string path)
        {
            _factory = () => File.ReadAllBytes(path);
        }

        public ModelSelector(byte[] data)
        {
            _factory = () => data;
        }

        public ModelSelector(Stream stream)
        {
            //_factory = () =>
            //{
            //    using var memory = new MemoryStream();
            //    stream.CopyTo(memory);

            //    return memory.ToArray();
            //};
            _factory = () =>
            {
                using (var memory = new MemoryStream())
                {
                    stream.CopyTo(memory);
                    return memory.ToArray();
                }
            };
        }

        internal byte[] Load() => _factory();

        public static implicit operator ModelSelector(string path) => new ModelSelector(path);
        public static implicit operator ModelSelector(byte[] data) => new ModelSelector(data);
        public static implicit operator ModelSelector(Stream stream) => new ModelSelector(stream);
    }
}
