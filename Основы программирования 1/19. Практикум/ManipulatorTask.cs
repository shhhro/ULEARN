using System;
using System.IO;
using NUnit.Framework;

namespace Manipulation
{
    public static class ManipulatorTask
    {
        /// <summary>
        /// Возвращает массив углов (shoulder, elbow, wrist),
        /// необходимых для приведения эффектора манипулятора в точку x и y 
        /// с углом между последним суставом и горизонталью, равному alpha (в радианах)
        /// См. чертеж manipulator.png!
        /// </summary>
        public static double[] MoveManipulatorTo(double x, double y, double alpha)
        {
            var wristX = FindCoordinate(x, Math.Cos(Math.PI - alpha));
            var wristY = FindCoordinate(y, Math.Sin(Math.PI - alpha));
            var wristPosition = Math.Sqrt(wristX * wristX + wristY * wristY);

            double elbowAngle = TriangleTask.GetABAngle(Manipulator.UpperArm, Manipulator.Forearm, wristPosition);
            double shoulderAngle = TriangleTask.GetABAngle(Manipulator.UpperArm, wristPosition, Manipulator.Forearm) + Math.Atan2(wristY, wristX);
            double wristAngle = -alpha - shoulderAngle - elbowAngle;

            if (elbowAngle == double.NaN || shoulderAngle == double.NaN || wristAngle == double.NaN)
                return new[] { double.NaN, double.NaN, double.NaN };
            return new[] { shoulderAngle, elbowAngle, wristAngle };
        }
        public static double FindCoordinate(double coordinate, double function)
        {
            return coordinate + function * Manipulator.Palm;
        }
    }

    [TestFixture]
    public class ManipulatorTask_Tests
    {
        [Test]
        public void TestMoveManipulatorTo()
        {
        }
    }
}