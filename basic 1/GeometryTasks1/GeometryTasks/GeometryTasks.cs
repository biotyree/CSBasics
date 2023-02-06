using System;

namespace GeometryTasks
{
    public class Vector
    {
        public double X;
        public double Y;
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
            return GetLength(Add(segment.Begin, segment.End));
        }

        public static bool IsVectorInSegment(Vector vector, Segment segment)
        {
            return GetLength(segment) == GetLength(Add(segment.Begin,vector)) + GetLength(Add(segment.End, vector)) ? true : false;
        }
    }

    public class Segment
    {
        public Vector Begin;
        public Vector End;
    }
}