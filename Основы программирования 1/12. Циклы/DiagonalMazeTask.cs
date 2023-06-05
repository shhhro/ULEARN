using System;
using System.IO;

namespace Mazes
{
	public static class DiagonalMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            int finish = height - 1;
            for (int i = 1; i < finish; i++)
                if (width >= height) GetRobotToFinish(robot, width, height, Direction.Down, Direction.Right);
                else GetRobotToFinish(robot, height, width, Direction.Right, Direction.Down);
        }
        public static void GetRobotToFinish(Robot robot, int firstSide, int secondSide, Direction firstDirection, Direction secondDirection)
        {
            var longSteps = CalculateRobotSteps(firstSide, secondSide);
            var oneStep = 1;
            if(!robot.Finished) MoveRobot(robot, longSteps, secondDirection);
            if(!robot.Finished) MoveRobot(robot, oneStep, firstDirection);
        }
        public static void MoveRobot(Robot robot, double path, Direction direction)
        {
            for (int zeroPosition = 0; zeroPosition < path; zeroPosition++)
                robot.MoveTo(direction);
        }
        public static double CalculateRobotSteps(double firstSide, double secondSide)
        {
            var stepsDirection = Math.Round((firstSide - 2) / (secondSide - 2));
            return stepsDirection;
        }
    }
}