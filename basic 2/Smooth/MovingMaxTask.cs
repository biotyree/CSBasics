using System;
using System.Collections.Generic;
using System.Linq;

namespace yield
{

	public static class MovingMaxTask
	{
		public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
		{
            var window = new Queue<DataPoint>();
            var maxsDeque = new LinkedList<double>();

            foreach (var point in data)
            {
                window.Enqueue(point);
                while(maxsDeque.Count > 0 && maxsDeque.Last.Value < point.OriginalY)
                    maxsDeque.RemoveLast();
                maxsDeque.AddLast(point.OriginalY);
                
                if (window.Count > windowWidth)
                    maxsDeque.Remove(window.Dequeue().OriginalY);

                yield return point.WithMaxY(maxsDeque.First.Value);
            }
        }
	}
}