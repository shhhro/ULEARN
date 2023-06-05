using System;

namespace func_rocket;

public class ControlTask
{
    private static double angle;
	public static Turn ControlRocket(Rocket rocket, Vector target)
	{
        Vector distance = new(target.X - rocket.Location.X, target.Y - rocket.Location.Y);
        var firstAngle = distance.Angle - rocket.Direction;
        var secondAngle = distance.Angle - rocket.Velocity.Angle;

        if (Math.Abs(firstAngle) < 0.5) angle = (firstAngle + secondAngle) / 2;
        else angle = firstAngle;

        if (angle > 0) return Turn.Right;
        if (angle < 0) return Turn.Left;
        else return Turn.None;
    }
}