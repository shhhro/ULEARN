using System;

namespace Rectangles
{
	public static class RectanglesTask
	{
		public static bool SquaresAreIntersected(Rectangle r1, Rectangle r2)
		{
			var intersectedTopAndBottom = r1.Bottom >= r2.Top && r1.Top <= r2.Bottom;
			var intersectedLeftAndRight = r1.Left <= r2.Right && r1.Right >= r2.Left;

			if (intersectedTopAndBottom && intersectedLeftAndRight) return true;
			else return false;
		}
		public static int IntersectionSquare(Rectangle r1, Rectangle r2)
		{
			if (SquaresAreIntersected(r1, r2))
			{
				int width = Math.Min(r1.Right, r2.Right) - Math.Max(r1.Left, r2.Left);
				int height = Math.Min(r1.Bottom, r2.Bottom) - Math.Max(r1.Top, r2.Top);
				return width * height;
			}
			else return 0;
		}
		public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
		{
            if (SquaresAreIntersected(r1, r2))
			{
                if (CheckInnerFirstRectangle(r1, r2)) return 0;
                if (CheckInnerSecondRectangle(r1, r2)) return 1;
            }
			return -1;
		}	
		static bool CheckInnerFirstRectangle(Rectangle r1, Rectangle r2)
		{
			return r1.Left >= r2.Left && r1.Right <= r2.Right && r1.Top >= r2.Top && r1.Bottom <= r2.Bottom;
        }
		static bool CheckInnerSecondRectangle(Rectangle r1, Rectangle r2)
		{
			return r2.Left >= r1.Left && r2.Right <= r1.Right && r2.Top >= r1.Top && r2.Bottom <= r1.Bottom;
        }
	}
}