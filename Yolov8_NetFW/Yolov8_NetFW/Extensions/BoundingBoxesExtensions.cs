using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yolov8_NetFW.Data;

namespace Yolov8_NetFW.Extensions
{
    internal static class BoundingBoxesExtensions
    {
        public static string Summary(this IEnumerable<IBoundingBox> boxes)
        {
            var sort = boxes.Select(x => x.Class)
                            .GroupBy(x => x.Id)
                            .OrderBy(x => x.Key)
                            .Select(x => $"{x.Count()} {x.First().Name}");

            return string.Join(", ", sort);
        }
    }
}
