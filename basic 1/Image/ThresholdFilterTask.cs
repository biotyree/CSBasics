using System;
using System.Linq;

namespace Recognizer
{
    public static class ThresholdFilterTask
    {
        public static double GetThreshold(double[,] original, double whitePixelsFraction)
        {
            var newImage = original.Cast<double>().ToList();
            newImage.Sort();
            return newImage[newImage.Count - (int)(whitePixelsFraction * original.Length)];
        }

        public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var newImage = new double[width, height];

            var threshold = ((int)(whitePixelsFraction * original.Length) == 0 ? 0 : GetThreshold(original, whitePixelsFraction));

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    if ((int)(whitePixelsFraction * original.Length) != 0 && original[i, j] >= threshold)
                        newImage[i, j] = 1.0;
                    else
                        newImage[i, j] = 0.0;
            return newImage;
        }
    }
}