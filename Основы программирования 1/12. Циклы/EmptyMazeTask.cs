namespace Mazes
{
	public static class EmptyMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
			MoveRobotByX(robot, width);
			MoveRobotByY(robot, height);
		}
		static void MoveRobotByX(Robot robot, int width)
		{
			for (int wall = 1; width - 2 > wall; width--) robot.MoveTo(Direction.Right);
        }
		static void MoveRobotByY(Robot robot, int height)
		{
            for (int wall = 1; height - 2 > wall; height--) robot.MoveTo(Direction.Down);
        }
	}
}