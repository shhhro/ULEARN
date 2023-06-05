using System.Collections.Generic;
using System.Linq;
using Greedy.Architecture;

namespace Greedy;

public class DijkstraPathFinder
{
    public static readonly HashSet<Point> directions = new()
    { new (0, 1), new (1, 0), new (-1, 0), new (0, -1) };
    public static readonly Point falsePoint = new(-1, -1);

    public IEnumerable<PathWithCost> GetPathsByDijkstra(State state, Point start, IEnumerable<Point> targets)
    {
        var visited = new HashSet<Point>();
        var notVisited = new HashSet<Point>() { start };
        var track = new Dictionary<Point, DijkstraData>();
        track[start] = new DijkstraData { Weight = 0, Previous = falsePoint };
        var chests = targets.ToHashSet();

        foreach (var calculatedPath in GetPathsDijkstraAlgorythm(state, chests, notVisited, track, visited))
            yield return calculatedPath;
    }

    public static IEnumerable<PathWithCost> GetPathsDijkstraAlgorythm(State state, HashSet<Point> chests, 
        HashSet<Point> notVisited, Dictionary<Point, DijkstraData> track, HashSet<Point> visited)
    {
        while (chests.Count != 0)
        {
            var toOpen = FindBestChoice(falsePoint, notVisited, track);
            if (toOpen.Equals(falsePoint)) yield break;

            if (chests.Contains(toOpen))
            {
                yield return GetPathWithCost(track, toOpen);
                chests.Remove(toOpen);
            }

            foreach (var nextPoint in GetNeighbours(toOpen, state))
            {
                var weight = track[toOpen].Weight + state.CellCost[nextPoint.X, nextPoint.Y];

                if (!track.ContainsKey(nextPoint) || track[nextPoint].Weight > weight)
                    track[nextPoint] = new DijkstraData { Previous = toOpen, Weight = weight };

                if (!visited.Contains(nextPoint)) notVisited.Add(nextPoint);
            }
            notVisited.Remove(toOpen);
            visited.Add(toOpen);
        }
    }

    public static PathWithCost GetPathWithCost(Dictionary<Point, DijkstraData> track, Point end)
    {
        var weight = track[end].Weight;
        var path = new List<Point>();
        while (!end.Equals(falsePoint))
        {
            path.Add(end);
            end = track[end].Previous;
        }
        path.Reverse();
        return new PathWithCost(weight, path.ToArray());
    }

    public static IEnumerable<Point> GetNeighbours(Point toOpen, State state) => 
        directions
        .Where(direction => state.InsideMap(toOpen + direction) && !state.IsWallAt(toOpen + direction))
        .Select(dir => toOpen + dir);

    public class DijkstraData
    {
        public int Weight { get; set; }
        public Point Previous { get; set; }
    }
    public static Point FindBestChoice(Point toOpen, HashSet<Point> notVisited, Dictionary<Point, DijkstraData> track)
    {
        var bestWeight = int.MaxValue;
        foreach (var currentPoint in notVisited
            .Where(point => track
            .ContainsKey(point) && track[point].Weight < bestWeight))
        {
            bestWeight = track[currentPoint].Weight;
            toOpen = currentPoint;
        }
        return toOpen;
    }
}