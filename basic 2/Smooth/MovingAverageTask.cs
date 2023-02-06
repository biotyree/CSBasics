using System.Collections.Generic;

namespace yield
{
	public static class MovingAverageTask
	{
		public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
		{
			var window = new Queue<DataPoint>();
			var sum = 0.0;
			foreach (var point in data)
			{
				window.Enqueue(point);
				sum += point.OriginalY;

				if (window.Count > windowWidth)
					sum -= window.Dequeue().OriginalY;

				yield return point.WithAvgSmoothedY(sum / window.Count);
			}
		}
	}
}