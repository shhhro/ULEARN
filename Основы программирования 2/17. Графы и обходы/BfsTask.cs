using System.Collections.Generic;
using System.Linq;

namespace Dungeon;

public class BfsTask
{
	public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point[] chests)
	{
		var searchQueue = new Queue<SinglyLinkedList<Point>>();
		var checkedPoints = new HashSet<Point>() { start };
		var chestsHashSet = chests.ToHashSet();
		searchQueue.Enqueue(new SinglyLinkedList<Point>(start));

		while (searchQueue.Count != 0)
		{
			var path = searchQueue.Dequeue();
            if (chestsHashSet.Contains(path.Value)) yield return path;
            if (!map.InBounds(path.Value) || map.Dungeon[path.Value.X, path.Value.Y] == MapCell.Wall) continue;
            foreach (var direction in Walker.PossibleDirections)
			{
				var nextPoint = new Point(path.Value.X + direction.X, path.Value.Y + direction.Y);
                if (!checkedPoints.Contains(nextPoint))
                {
                    searchQueue.Enqueue(new SinglyLinkedList<Point>(nextPoint, path));
                    checkedPoints.Add(nextPoint);
                }
			}
		}
	}
}
