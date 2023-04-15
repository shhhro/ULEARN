using System.IO;

namespace Mazes
{
    public static class SnakeMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            int stepsDown = (height - 2) / 2;
            int horizontalPath = width - 2;
            int verticalStep = 3;

            for (int i = 0; i <= stepsDown; stepsDown--)
            {
                if (stepsDown % 2 == 0) MoveRobot(robot, horizontalPath, Direction.Left);
                else MoveRobot(robot, horizontalPath, Direction.Right);

                if (!robot.Finished) MoveRobot(robot, verticalStep, Direction.Down);
            }
        }
        static void MoveRobot(Robot robot, int path, Direction where)
        {
            for (int startPosition = 1; path > startPosition; path--)
                robot.MoveTo(where);
        }
    }
}