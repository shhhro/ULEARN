using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
    public static class ThresholdFilterTask
    {
        public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
        {
            int lengthX = original.GetLength(0);
            int lengthY = original.GetLength(1);

            double countOfWhitePixels = (int)(original.Length * whitePixelsFraction);
            var threshold = CalculateThreshold(countOfWhitePixels, original);
            var imageWithThresholdFilter = new double[lengthX, lengthY];

            for (int x = 0; x < lengthX; x++)
                for (var y = 0; y < lengthY; y++)
                    imageWithThresholdFilter[x, y] = original[x, y] >= threshold ? 1.0 : 0.0;

            return imageWithThresholdFilter;
        }
        public static double CalculateThreshold(double countOfWhitePixels, double[,] original)
        {
            if (countOfWhitePixels > 0)
            {
                List<double> sortPixels = new List<double>();
                foreach (double pixel in original)
                    sortPixels.Add(pixel);
                sortPixels.Sort();
                return sortPixels[sortPixels.Count - (int)countOfWhitePixels];
            }
            else
                return int.MaxValue;
        }
    }
}

