using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yolov8_NetFW.Plotting
{
    internal class HumanSkeleton : ISkeleton
    {
        private readonly string[] _colors = new string[] {

            "FF8000",
            "FF9933",
            "FFB266",
            "E6E600",
            "FF99FF",
            "99CCFF",
            "FF66FF",
            "FF33FF",
            "66B2FF",
            "3399FF",
            "FF9999",
            "FF6666",
            "FF3333",
            "99FF99",
            "66FF66",
            "33FF33",
            "00FF00",
            "0000FF",
            "FF0000",
            "FFFFFF",
        };

        private readonly SkeletonConnection[] _connections = new SkeletonConnection[] { 

            new SkeletonConnection(15, 13),
            new SkeletonConnection(13, 11),
            new SkeletonConnection(16, 14),
            new SkeletonConnection(14, 12),
            new SkeletonConnection(11, 12),
            new SkeletonConnection(5, 11),
            new SkeletonConnection(6, 12),
            new SkeletonConnection(5, 6),
            new SkeletonConnection(5, 7),
            new SkeletonConnection(6, 8),
            new SkeletonConnection(7, 9),
            new SkeletonConnection(8, 10),
            new SkeletonConnection(1, 2),
            new SkeletonConnection(0, 1),
            new SkeletonConnection(0, 2),
            new SkeletonConnection(1, 3),
            new SkeletonConnection(2, 4),
            new SkeletonConnection(3, 5),
            new SkeletonConnection(4, 6)
        };

        private readonly int[] _keypointColorMap = new int[] {
        
            16, 16, 16, 16, 16, 0, 0, 0, 0, 0, 0, 9, 9, 9, 9, 9, 9
        };

        private readonly int[] _lineColorMap = new int[] {
        
            9, 9, 9, 9, 7, 7, 7, 0, 0, 0, 0, 0, 16, 16, 16, 16, 16, 16, 16
        };

        public IReadOnlyList<SkeletonConnection> Connections => _connections;

        public Color GetKeypointColor(int index)
        {
            index = _keypointColorMap[index % _keypointColorMap.Length];

            var hex = _colors[index];

            return Color.ParseHex(hex);
        }

        public Color GetLineColor(int index)
        {
            index = _lineColorMap[index % _lineColorMap.Length];

            var hex = _colors[index];

            return Color.ParseHex(hex);
        }
    }
}
