using System;
using System.Drawing;
using System.Runtime.Remoting.Lifetime;

namespace RoutePlanning
{
    public static class PathFinderTask
    {

        public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
        {
            var minimalPathLength = double.MaxValue;
            var position = 1;
            var countOfCheckpoints = checkpoints.Length;

            var path = new int[countOfCheckpoints];
            var bestOrder = FillArrayWithSerialNumbers(new int[countOfCheckpoints]);

            if (countOfCheckpoints == 1) return bestOrder;

            MakeTrivialPermutation(checkpoints, bestOrder, minimalPathLength, path, position);

            return bestOrder;
        }

        private static double MakeTrivialPermutation(Point[] checkpoints, int[] bestOrder, double minimalPathLength, int[] path, int position)
        {
            var distance = checkpoints.GetPathLength(path);
            var nextPosition = position + 1;

            if (position == path.Length && distance < minimalPathLength)
            {
                minimalPathLength = distance;
                CloneArray(path, bestOrder); 
                return minimalPathLength;
            }

            for (int i = 0; i < path.Length; i++)
            {
                int index = Array.IndexOf(path, i, 0, position);
                if (index == -1)
                {
                    path[position] = i;
                    minimalPathLength = MakeTrivialPermutation(checkpoints, bestOrder, minimalPathLength, path, nextPosition);
                }
            }
            return minimalPathLength;
        }
        public static int[] FillArrayWithSerialNumbers(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = i;
            return array;
        }
        public static int[] CloneArray(int[] original, int[] cloned)
        {
            for (int i = 0; i < original.Length; i++)
                cloned[i] = original[i];
            return original;
        }
    }
}
