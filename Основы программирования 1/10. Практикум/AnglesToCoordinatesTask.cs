using System;
using System.Drawing;
using NUnit.Framework;

namespace Manipulation
{
    public static class AnglesToCoordinatesTask
    {
        const float UpperArm = Manipulator.UpperArm;
        const float Forearm = Manipulator.Forearm;
        const float Palm = Manipulator.Palm;
        public static PointF[] GetJointPositions(double shoulder, double elbow, double wrist)
        {
            float firstPosition; float secondPosition;
            double angleBetweenUpperArmAndBody = shoulder;
            double angleBetweenUpperArmAndForearm = elbow + shoulder - Math.PI;
            double angleBetweenForearmAndPalm = shoulder + elbow + wrist - 2 * Math.PI;

            firstPosition = GetCoordinatePosition(UpperArm, (float)Math.Cos(angleBetweenUpperArmAndBody));
            secondPosition = GetCoordinatePosition(UpperArm, (float)Math.Sin(angleBetweenUpperArmAndBody));
            PointF elbowPos = new PointF(firstPosition, secondPosition);

            firstPosition += GetCoordinatePosition(Forearm, (float)Math.Cos(angleBetweenUpperArmAndForearm));
            secondPosition += GetCoordinatePosition(Forearm, (float)Math.Sin(angleBetweenUpperArmAndForearm));
            PointF wristPos = new PointF(firstPosition, secondPosition);

            firstPosition += GetCoordinatePosition(Palm, (float)Math.Cos(angleBetweenForearmAndPalm));
            secondPosition += GetCoordinatePosition(Palm, (float)Math.Sin(angleBetweenForearmAndPalm));
            PointF palmEndPos = new PointF(firstPosition, secondPosition);

            return new PointF[]
            {
                elbowPos,
                wristPos,
                palmEndPos
            };
        }

        public static float GetCoordinatePosition(float constForCompute, float angle)
        {
            return constForCompute * angle;
        }
    }

    [TestFixture]
    public class AnglesToCoordinatesTask_Tests
    {
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]
        public void TestGetJointPositions(double shoulder, double elbow, double wrist, double palmEndX, double palmEndY)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
            Assert.AreEqual(palmEndX, joints[2].X, 1e-5, "palm endX");
            Assert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
        }
    }
}