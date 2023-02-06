using System.Collections.Generic;
using System.Drawing;
using GeometryTasks;

namespace GeometryPainting
{
    public static class SegmentExtensions
    {
        public static new Dictionary<Segment, Color> SegmentsColors = new Dictionary<Segment, Color>();

        public static Color GetColor(this Segment segment)
        {
            return SegmentsColors.ContainsKey(segment) ? SegmentsColors[segment] : Color.Black;
        }

        public static void SetColor(this Segment segment, Color color)
        {
            if (SegmentsColors.ContainsKey(segment))
            {
                SegmentsColors[segment] = color;
            }
            else
            {
                SegmentsColors.Add(segment, color);
            }
        }
    }
}
