using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] GetTranspontedMatrix(double[,] sx)
        {
            var width = sx.GetLength(0);
            var height = sx.GetLength(1);
            var sy = new double[height, width];

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    sy[j, i] = sx[i, j];

            return sy;
        }

        public static double GetMatrixConvolution(double[,] s, double[,] g, int x, int y, int boundSize)
        {
            var gx = 0.0;
            for (int i = x - boundSize; i <= x + boundSize; i++)
                for (int j = y - boundSize; j <= y + boundSize; j++)
                    gx += s[i - x + boundSize, j - y + boundSize] * g[i, j];

            return gx;
        }

        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var result = new double[width, height];

            if (width == 1 && height == 1)
            {
                var gx = sx[0, 0] * g[0, 0];
                result[0, 0] = Math.Sqrt(gx * gx + gx * gx);
                return result;
            }

            var boundSize = (int)((sx.GetLength(0) - 1) / 2);
            var sy = GetTranspontedMatrix(sx);

            for (int x = boundSize; x < width - boundSize; x++)
                for (int y = boundSize; y < height - boundSize; y++)
                {
                    var gx = GetMatrixConvolution(sx, g, x, y, boundSize);
                    var gy = GetMatrixConvolution(sy, g, x, y, boundSize);

                    result[x, y] = Math.Sqrt(gx * gx + gy * gy);
                }
            return result;
        }
    }
}