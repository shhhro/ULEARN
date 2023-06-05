using System.Collections.Generic;

namespace yield
{
    public static class MovingAverageTask
    {
        public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
        {
            var result = 0.0;
            Queue<double> pointQueue = new();
            foreach (var point in data)
            {
                if (pointQueue.Count >= windowWidth) result -= pointQueue.Dequeue();
                pointQueue.Enqueue(point.OriginalY);
                result += point.OriginalY; 
                yield return point.WithAvgSmoothedY(result / pointQueue.Count);
            }
        }
    }
}