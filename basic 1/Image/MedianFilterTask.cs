using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace Recognizer
{
	internal static class MedianFilterTask
	{

		public static double GetMedianFilterValue(List<double> valueList)
		{
			valueList.Sort();
			if (valueList.Count % 2 == 0)
				return (valueList[valueList.Count / 2] + valueList[valueList.Count / 2 - 1]) / 2;
			else
				return valueList[(valueList.Count - 1) / 2];
		}

		public static List<double> GetMedianFilterWindow(double[,] original, int posI, int posJ)
		{
			var filterWindow = new List<double>();
			for (int i = posI - 1; i <= posI + 1; i++)
				for (int j = posJ - 1; j <= posJ + 1; j++)
					try
					{
						filterWindow.Add(original[i, j]);
					}
					catch
					{
					
					}

			return filterWindow;
		}

		public static double[,] MedianFilter(double[,] original)
		{
			var width = original.GetLength(0);
			var height = original.GetLength(1);
            var newImage = new double[width, height];

            for (int i = 0; i < width; i++)
				for (int j = 0; j < height; j++)
					newImage[i, j] = GetMedianFilterValue(GetMedianFilterWindow(original, i, j));

            return newImage;
		}
	}
}