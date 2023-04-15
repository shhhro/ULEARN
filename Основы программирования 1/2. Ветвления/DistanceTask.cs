using System;

namespace DistanceTask
{
    public static class DistanceTask
    {
        public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
        {
            double abLength = CalculateDistance(ax, ay, bx, by);
            double ac = CalculateDistance(ax, ay, x, y);
            double bc = CalculateDistance(bx, by, x, y);

            if ((ax == x && ay == y) || (bx == x && by == y)) return 0;
            if (abLength == 0) return ac;

            if (CheckObtuseAngle(bc, abLength, ac)) return bc;
            if (CheckObtuseAngle(ac, abLength, bc)) return ac;

            double perimeter = (ac + bc + abLength) / 2;
            var distancetosegment = 2 * Math.Sqrt(perimeter * (perimeter - abLength) * (perimeter - bc) * (perimeter - ac)) / abLength;
            return distancetosegment;
        }

        public static double CalculateDistance(double x1, double y1, double x2, double y2)
        {
            var distance = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
            return distance;
        }

        public static bool CheckObtuseAngle(double a, double b, double x)
        {
            return (a * a + b * b - x * x) / (2 * a * b) < 0;
        }
    }
}
