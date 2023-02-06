using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RoutePlanning
{
    public static class PathFinderTask
    {
        public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
        {
            var bestOrders = new Dictionary<int[], double>();

            MakePermutations(new int[checkpoints.Length], checkpoints, 1, bestOrders);

            return bestOrders.FirstOrDefault(x => x.Value == bestOrders.Values.Min()).Key;
        }

        private static void MakePermutations(int[] current, Point[] checkpoints, int position, Dictionary<int[], double> best)
        {
            var bestWeight = best.Count > 0 ? best.Values.Min() : double.MaxValue;
            var currentWeight = IsRouteBest(current, checkpoints, position, bestWeight);

            if (currentWeight >= 0)
            {
                current = (int[])current.Clone();
                if (IsDetourOver(current, position, currentWeight, best))
                    return;
                for (var i = 0; i < current.Length; i++)
                {
                    var index = Array.IndexOf(current, i, 0, position);
                    if (index != -1)
                        continue;
                    current[position] = i;
                    MakePermutations(current, checkpoints, position + 1, best);
                }
            }
        }

        private static double IsRouteBest(int[] current, Point[] checkpoints, int position, double best)
        {
            var currentWeight = 0.0;
            for (var i = 0; i < position - 1 && i < checkpoints.Length - 1; i++)
                currentWeight += checkpoints[current[i]].DistanceTo(checkpoints[current[i + 1]]);

            return currentWeight > best ? -1.0 : currentWeight;
        }

        private static bool IsDetourOver(int[] current, int position, double currentWeight, Dictionary<int[], double> best)
        {
            if (position == current.Length)
            {
                if (best.ContainsKey(current))
                    best[current] = currentWeight;
                else
                    best.Add(current, currentWeight);

                return true;
            }
            return false;
        }
    }
}