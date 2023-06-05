using System;
using System.Collections.Generic;

namespace Rivals;

public class RivalsTask
{
    private static List<Point> neighboorPoints = new List<Point>()
    {
        new Point(0, 1),
        new Point(1, 0),
        new Point(0, -1),
        new Point(-1, 0)
    };

    public static IEnumerable<OwnedLocation> AssignOwners(Map map)
    {
        var pointQueue = new Queue<Tuple<Point, int, int>>();
        var visitedPoints = new HashSet<Point>();
        for (var playerIndex = 0; playerIndex < map.Players.Length; playerIndex++)
            pointQueue.Enqueue(Tuple.Create(new Point(map.Players[playerIndex].X, 
                map.Players[playerIndex].Y), playerIndex, 0));

        while (pointQueue.Count > 0)
        {
            var (point, player, distance) = pointQueue.Dequeue();
            if (CheckNextPoint(map, visitedPoints, point)) continue;
            visitedPoints.Add(point);
            yield return new OwnedLocation(player, new Point(point.X, point.Y), distance);
            foreach (var neighbor in neighboorPoints)
                pointQueue.Enqueue(Tuple.Create(CreateNewPoint(point, neighbor), player, distance + 1));
        }
    }

    public static bool CheckNextPoint(Map map, HashSet<Point> visited, Point point) => 
        !map.InBounds(point) || map.Maze[point.X, point.Y] == MapCell.Wall || visited.Contains(point);

    public static Point CreateNewPoint(Point point, Point neighbor) => 
        new Point(point.X + neighbor.X, point.Y + neighbor.Y);
}