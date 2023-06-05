using System;
using System.Collections.Generic;
using System.Linq;

namespace Dungeon;

public class DungeonTask
{
    public static MoveDirection[] FindShortestPath(Map map)
    {
        var pathToExit = BfsTask.FindPaths(map, map.InitialPosition, new[] { map.Exit }).FirstOrDefault();

        if (pathToExit == null) return new MoveDirection[0];
        if (map.Chests.Any(chestPoint => pathToExit.Contains(chestPoint))) 
            return ParseToMoveDirection(pathToExit);

        var pathToChestsFromPlayer = BfsTask.FindPaths(map, map.InitialPosition, map.Chests);
        var pathToChestsFromExit = BfsTask.FindPaths(map, map.Exit, map.Chests);
        var paths = pathToChestsFromPlayer.Join(pathToChestsFromExit, pathToChest =>
                pathToChest.Value, pathToExit => pathToExit.Value, Tuple.Create).ToList();

        var shortestPath = FindShortestPath(paths);
        if(shortestPath == null) return ParseToMoveDirection(pathToExit);
        return ParseToMoveDirection(shortestPath);
    }

    public static List<Point> FindShortestPath(
		List<Tuple<SinglyLinkedList<Point>, SinglyLinkedList<Point>>> paths)
	{
        if (paths.Count == 0) return null;
        var minLength = int.MaxValue; var indexOfPath = 0;

        for (var i = 0; i < paths.Count; i++)
        {
            var (chestsPath, exitPath) = paths[i];
            if (chestsPath.Length + exitPath.Length >= minLength) continue;
            indexOfPath = i; minLength = chestsPath.Length + exitPath.Length;
        }
        var (pathToChest, pathToExit) = paths[indexOfPath];
        return pathToExit.Skip(1)
            .Aggregate(pathToChest, (currentLinkedList, point) =>
            new SinglyLinkedList<Point>(point, currentLinkedList)).ToList();
    }

    public static MoveDirection[] ParseToMoveDirection(IEnumerable<Point> path)
    {
        List<Point> list = path.Reverse().ToList();
        return list.Zip(list.Skip(1), (firstPoint, secondPoint) =>
            Walker.ConvertOffsetToDirection(new Point(secondPoint.X - firstPoint.X, secondPoint.Y - firstPoint.Y)))
            .ToArray();
    }
}