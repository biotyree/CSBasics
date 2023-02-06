using System;

namespace ReadOnlyVectorTask
{

    public class ReadOnlyVector
    {
        public readonly double X;
        public readonly double Y;

        public ReadOnlyVector() { }

        public ReadOnlyVector(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double Add(ReadOnlyVector other)
        {
            return Math.Sqrt((this.X + other.X) * (this.X + other.X) + (this.Y + other.Y) * (this.Y + other.Y));
        }

        public ReadOnlyVector WithX(double Y)
        {
            return new ReadOnlyVector(this.X, Y);
        }

        public ReadOnlyVector WithY(double X)
        {
            return new ReadOnlyVector(X, this.Y);
        }
    }
}