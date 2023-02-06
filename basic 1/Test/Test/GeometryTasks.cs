using System;
using System.Runtime.CompilerServices;

namespace GeometryTasks
{
    public class Vector
    {
        public double X;
        public double Y;

        public double GetLength()
        {
            return Geometry.GetLength(this);
        }

        public Vector Add(Vector vector)
        {
            return Geometry.Add(this, vector);
        }

        public bool Belongs(Segment segment)
        {
            return Geometry.IsVectorInSegment(this, segment);
        }
    }

    public class Geometry
    {
        public static double GetLength(Vector vector)
        {
            return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }

        public static Vector Add(Vector begin, Vector end)
        {
            return new Vector { X = begin.X + end.X, Y = begin.Y + end.Y };
        }

        public static double GetLength(Segment segment)
        {
            return GetLength(new Vector { X = segment.End.X - segment.Begin.X, Y = segment.End.Y - segment.Begin.Y });
        }

        public static bool IsVectorInSegment(Vector vector, Segment segment)
        {
            var beginToVectorLength = GetLength(new Segment { Begin = segment.Begin, End = vector });
            var endToVectorLength = GetLength(new Segment { Begin = segment.End, End = vector });
            return Math.Abs(GetLength(segment) - (beginToVectorLength + endToVectorLength)) < 10e-9 ? true : false;
        }
    }

    public class Segment
    {
        public Vector Begin;
        public Vector End;

        public double GetLength()
        {
            return Geometry.GetLength(this);
        }

        public bool Contains(Vector vector)
        {
            return Geometry.IsVectorInSegment(vector, this);
        }
    }
}