using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GeometryTasks;

namespace GeometryPainting
{
    public static class SegmentExtensions
    {
        private static Dictionary<Segment, Color> ColorsOfSegments = new Dictionary<Segment, Color>();
        public static void SetColor(this Segment segment, Color color)
        {
            if (ColorsOfSegments.ContainsKey(segment)) ColorsOfSegments[segment] = color;
            else ColorsOfSegments.Add(segment, color);
        }
        public static Color GetColor(this Segment segment)
        {
            if(ColorsOfSegments.ContainsKey(segment)) return ColorsOfSegments[segment];
            return Color.Black;
        }
    }
}
