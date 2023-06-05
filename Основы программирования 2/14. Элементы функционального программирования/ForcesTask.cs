namespace func_rocket;

public class ForcesTask
{
	/// <summary>
	/// Создает делегат, возвращающий по ракете вектор силы тяги двигателей этой ракеты.
	/// Сила тяги направлена вдоль ракеты и равна по модулю forceValue.
	/// </summary>
	public static RocketForce GetThrustForce(double forceValue) => 
		rcket => new Vector(forceValue, 0).Rotate(rcket.Direction);

	/// <summary>
	/// Преобразует делегат силы гравитации, в делегат силы, действующей на ракету
	/// </summary>
	public static RocketForce ConvertGravityToForce(Gravity gravity, Vector spaceSize) =>
		rcket => new Vector(gravity(spaceSize, rcket.Location).X, gravity(spaceSize, rcket.Location).Y);

	/// <summary>
	/// Суммирует все переданные силы, действующие на ракету, и возвращает суммарную силу.
	/// </summary>
	public static RocketForce Sum(params RocketForce[] forces) => rcket => {	
		var sum = new Vector(0, 0);
		for (int i = 0; i < forces.Length; i++)
			sum += forces[i](rcket);
		return sum; };
}