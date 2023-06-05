using System.Collections.Generic;
using Greedy.Architecture;
using System.Linq;

namespace Greedy
{
    public class NotGreedyPathFinder : IPathFinder
    {
        public readonly Dictionary<Point, Dictionary<Point, PathWithCost>> Paths;
        public List<Point> BestPath;
        public int MaxChests;

        public NotGreedyPathFinder()
        {
            Paths = new Dictionary<Point, Dictionary<Point, PathWithCost>>();
            BestPath = new List<Point>();
        }

        public List<Point> FindPathToCompleteGoal(State state)
        {
            var chests = new List<Point>(state.Chests) { state.Position };
            var current = new List<Point>();
            var finder = new DijkstraPathFinder();

            foreach (var point in chests)
            {
                var dijkstraPaths = finder.GetPathsByDijkstra(state, point, state.Chests);
                if (Paths != null && !Paths.ContainsKey(point)) 
                    Paths.Add(point, new Dictionary<Point, PathWithCost>());
                foreach (var path in dijkstraPaths)
                {
                    if (path.Start.Equals(path.End)) continue;
                    if (Paths != null) Paths[path.Start][path.End] = path;
                }
            }
            var pathsFromPosition = Paths[state.Position];

            FindBestFromAll(pathsFromPosition, state);

            return UpdateCurrentPath(current, BestPath);
        }

        private void FindBestFromAll(Dictionary<Point, PathWithCost> pathsFromPosition, State state)
        {
            foreach (var path in pathsFromPosition)
            {
                var points = new HashSet<Point>();
                foreach (var tuple in Paths[path.Key])
                    points.Add(tuple.Key);
                FindBestPath(state.Energy - path.Value.Cost,
                path.Key, points, 1, new List<Point> { state.Position, path.Key });
            }
        }

        private void FindBestPath(int currentEnergy, Point currentPosition, IEnumerable<Point> leftChests,
            int takenChests, List<Point> points)
        {
            var chests = new HashSet<Point>(leftChests);
            chests.Remove(currentPosition);
            foreach (var point in chests)
            {
                if (Paths[currentPosition][point].Cost > currentEnergy) continue;
                var newPath = new List<Point>(points) { point };
                FindBestPath(currentEnergy - Paths[currentPosition][point].Cost,
                    point, chests, takenChests + 1, newPath);
            }
            if (takenChests <= MaxChests) return;
            MaxChests = takenChests;
            BestPath = points;
        }

        public List<Point> UpdateCurrentPath(List<Point> current, List<Point> goodPath)
        {
            for (var path = 0; path < goodPath.Count - 1; path++)
            {
                var nextMove = Paths[goodPath[path]][goodPath[path + 1]]
                    .Path.Skip(1);
                current = current.Concat(nextMove).ToList();
            }
            return current;
        }
    }
}