using System.Collections.Generic;

namespace yield
{
    public static class MovingMaxTask
    {
        public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
        {
            var comparisonList = new LinkedList<double>();
            var pointsCountWindow = new Queue<double>();
            foreach (var point in data)
            {
                pointsCountWindow.Enqueue(point.OriginalY);
                if (pointsCountWindow.Count > windowWidth && pointsCountWindow.Dequeue() >= comparisonList.First.Value) 
                    comparisonList.RemoveFirst();
                while (comparisonList.Count != 0 && point.OriginalY > comparisonList.Last.Value)
                    comparisonList.RemoveLast();
                comparisonList.AddLast(point.OriginalY);
                yield return point.WithMaxY(comparisonList.First.Value);
            }
        }
    }
}