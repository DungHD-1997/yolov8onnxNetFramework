using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Yolov8_NetFW.Metadata
{
    public class YoloV8Class
    {
        public int Id { get; }

        public string Name { get; }

        public override string ToString()
        {
            return $"{Id}: '{Name}'";
        }
        public YoloV8Class(int id, string name)
        {
            this.Id = id ;      
            this.Name = name ;
        }
    }
}
