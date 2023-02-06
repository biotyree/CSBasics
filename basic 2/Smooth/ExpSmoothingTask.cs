using NUnit.Framework.Constraints;
using System.Collections.Generic;

namespace yield
{
	public static class ExpSmoothingTask
	{
		public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
		{
			var isFirstRun = true;
			var prevPoint = new DataPoint(0, 0);

			foreach (var point in data)
			{
				if (isFirstRun)
				{
					prevPoint = point.WithExpSmoothedY(point.OriginalY);
					yield return prevPoint;
                    isFirstRun = false;
                }
				else
				{
					var currentPoint = point.WithExpSmoothedY(alpha * point.OriginalY + (1 - alpha) * prevPoint.ExpSmoothedY);
                    yield return currentPoint;
                    prevPoint = currentPoint;
                }
			}
		}
	}
}