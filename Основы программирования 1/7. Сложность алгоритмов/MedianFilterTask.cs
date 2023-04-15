using System;
using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
	internal static class MedianFilterTask
	{
		public static double[,] MedianFilter(double[,] original)
		{
			var lengthX = original.GetLength(0);
			var lengthY = original.GetLength(1);

			var filteredImage = new double[lengthX, lengthY];
			for(int x = 0; x < lengthX; x++)
				for(int y = 0; y < lengthY; y++)
					filteredImage[x, y] = CalculateMedian(GetWindowOfPixel(original, x, y, lengthX, lengthY));

			return filteredImage;
		}

		public static List<double> GetWindowOfPixel(double[,] original, int x, int y, int lengthX, int lengthY)
		{
			var window = new List<double>();

            int fromX = x == 0 ? 0 : x - 1;
            int toX = x == lengthX - 1 ? lengthX - 1 : x + 1;
            int fromY = y == 0 ? 0 : y - 1;
            int toY = y == lengthY - 1 ? lengthY - 1 : y + 1;

			for (int firstX = fromX; firstX <= toX; firstX++)
				for(int firstY = fromY; firstY <=  toY; firstY++)
				{

					window.Add(original[firstX, firstY]);
				}
            return window;
		}

		public static double CalculateMedian(List<double> window)
		{
            window.Sort();

			if (window.Count % 2 == 1)
				return window[window.Count / 2];
			else
				return (window[window.Count / 2] + window[window.Count / 2 - 1]) / 2;
		}
	}
}