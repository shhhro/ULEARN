using System.Collections.Generic;

namespace yield;

public static class ExpSmoothingTask
{
    public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
    {
        var previousDataPointY = 0.0;
        var firstElementFlag = true;

        foreach (var dataPoint in data)
        {
            if (firstElementFlag)
            {
                firstElementFlag = false;
                previousDataPointY = dataPoint.OriginalY; 
            }
            else previousDataPointY += alpha * (dataPoint.OriginalY - previousDataPointY);
            yield return dataPoint.WithExpSmoothedY(previousDataPointY);
        }
    }
}