using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] original, double[,] sx)
        {
            var lengthX = original.GetLength(0);
            var lengthY = original.GetLength(1);

            var shift = sx.GetLength(0) / 2;
            var imageWithSobelFilter = new double[lengthX, lengthY];
            for (int x = shift; x < lengthX - shift; x++)
                for (int y = shift; y < lengthY - shift; y++)
                {
                    double[,] sy = GetTranspositionMatrix(sx);
                    double[,] partOfImage = GetPartOfImageOnCoordinate(original, x, y, sx.GetLength(0), shift);
                    
                    double gx = CalculateConvolution(partOfImage, sx);
                    double gy = CalculateConvolution(partOfImage, sy);

                    imageWithSobelFilter[x, y] = Math.Sqrt(gx * gx + gy * gy);
                }
            return imageWithSobelFilter;
        }

        public static double[,] GetPartOfImageOnCoordinate(double[,] g, int x, int y, int length, int shift)
        {
            double[,] partOfImage = new double[length, length];

            for (int i = -shift; i <= shift; i++)
                for (int j = -shift; j <= shift; j++)
                    partOfImage[i + shift, j + shift] = g[x + j, y + i];

            return partOfImage;
        }

        public static double[,] GetTranspositionMatrix(double[,] matrix)
        {
            int length = matrix.GetLength(0);
            double[,] transpositionMatrix = new double[length, length];

            for (int i = 0; i < length; i++)
                for (int j = 0; j < length; j++)
                    transpositionMatrix[j, i] = matrix[i, j];

            return transpositionMatrix;
        }

        public static double CalculateConvolution(double[,] partOfImage, double[,] matrix)
        {
            int length = partOfImage.GetLength(0);
            double convolution = 0;

            for (int i = 0; i < length; i++)
                for (int j = 0; j < length; j++)
                    convolution += partOfImage[i, j] * matrix[i, j];

            return convolution;
        }
    }
}