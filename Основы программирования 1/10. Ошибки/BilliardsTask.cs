using System;

namespace Billiards
{ 
    public static class BilliardsTask
    {
        /// <param name="directionRadians">Угол направления движения шара</param>
        /// <param name="wallInclinationRadians">Угол</param>
        /// <returns></returns>
        public static double BounceWall(double directionRadians, double wallInclinationRadians)
        {
            double directionAngle = (directionRadians * 180) / Math.PI;
            double wallInclinationAngle = (wallInclinationRadians * 180) / Math.PI;
            double BounceAngle = wallInclinationAngle + (wallInclinationAngle - directionAngle);
            return (BounceAngle * Math.PI) / 180;
        }
    }
}