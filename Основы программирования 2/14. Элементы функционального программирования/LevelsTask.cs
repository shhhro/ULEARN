using System;
using System.Collections.Generic;

namespace func_rocket;

public class LevelsTask
{
	static readonly Physics physics = new();
	static readonly Rocket rocket = new(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI);
	static readonly Vector target = new(600, 200);
    static readonly Vector anomaly = (rocket.Location + target) / 2;

    public static IEnumerable<Level> CreateLevels()
	{
		yield return new Level("Zero", rocket, target, 
			(size, rl) => Vector.Zero, physics);
		yield return new Level("Heavy", rocket, target, 
			(size, rl) => new Vector(0, 0.9), physics);
		yield return new Level("Up", rocket, new Vector(700, 500), 
			CreateGravityUp(), physics);
		yield return new Level("WhiteHole", rocket, target, 
			CreateGravityWhiteHole(), physics);
		yield return new Level("BlackHole", rocket, target,
			CreateGravityBlackHole(), physics);
		yield return new Level("BlackAndWhite", rocket, target,
			CreateGravityBlackAndWhite(), physics);
    }

	public static Gravity CreateGravityUp() => (size, v) => 
		new Vector(0, -300 / (size.Y - v.Y + 300.0));

	public static Gravity CreateGravityWhiteHole() => (size, rl) => {
			var d = (target - rl).Length;
			return (target - rl).Normalize() * (-140 * d / (d * d + 1)); };

	public static Gravity CreateGravityBlackHole() => (size, rl) => {
			var d = (anomaly - rl).Length;
			return (anomaly - rl).Normalize() * (300 * d / (d * d + 1)); };

	public static Gravity CreateGravityBlackAndWhite() => (size, rl) => 
		(CreateGravityWhiteHole()(size, rl) + CreateGravityBlackHole()(size, rl)) / 2;
}