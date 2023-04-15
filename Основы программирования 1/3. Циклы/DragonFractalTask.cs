using Fractals;
using System;
using System.Drawing;

namespace Fractals
{
    internal static class DragonFractalTask
    {
        public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
        {
            double x = 1.0;
            double y = 0.0;
            DrawFractal(pixels, iterationsCount, x, y, seed);
        }
        static double CalculateNewX(double angle, double x, double y)
        {
            return (x * Math.Cos(angle) - y * Math.Sin(angle)) / Math.Sqrt(2);
        }
        static double CalculateNewY(double angle, double x, double y)
        {
            return (x * Math.Sin(angle) + y * Math.Cos(angle)) / Math.Sqrt(2);
        }
        static void DrawFractal(Pixels pixels, int iterationsCount, double x, double y, int seed)
        {
            var firstAngle = Math.PI * 45 / 180;
            var secondAngle = Math.PI * 135 / 180;

            var random = new Random(seed);

            for (int i = 0; i < iterationsCount; i++)
            {
                double angle, newX, newY;

                if (random.Next(2) == 0) angle = firstAngle;
                else angle = secondAngle;

                newX = CalculateNewX(angle, x, y);
                newY = CalculateNewY(angle, x, y);
                if (angle == secondAngle) newX += 1;
                x = newX;
                y = newY;
                pixels.SetPixel(x, y);
            }
        }
    }

}
/*for (int i = 0; i < iterationsCount; i++)
{
    double angle;

    if (random.Next(2) == 0) angle = firstAngle;
    else angle = secondAngle;

    newX = CalculateNewX(angle, x, y);
    if (angle == secondAngle) newX += 1;
    newY = CalculateNewY(angle, x, y);
    x = newX;
    y = newY;
    pixels.SetPixel(x, y);
}*/
/*for (int i = 0; i < iterationsCount; i++)
            {
                double newX;
                double newY;
                if (random.Next(2) == 0)
                {
                    newX = CalculateNewX(firstAngle, x, y);
                    newY = CalculateNewY(firstAngle, x, y);
                }
                else
                {
                    newX = CalculateNewX(secondAngle, x, y) + 1;
                    newY = CalculateNewY(secondAngle, x, y);
                }
                x = newX;
                y = newY;
                pixels.SetPixel(x, y);
            }*/