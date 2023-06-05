using System.Collections.Generic;
using System.Linq;
using Greedy.Architecture;

namespace Greedy
{
    public class GreedyPathFinder : IPathFinder
    {
        public List<Point> FindPathToCompleteGoal(State state)
        {
            var chests = new HashSet<Point>(state.Chests);
            var position = state.Position;
            var cost = 0;
            var pathFinder = new DijkstraPathFinder();
            var nextMove = new List<Point>();
            
            for (var point = 0; point < state.Goal; point++)
            {
                var calculatedPath = pathFinder
                    .GetPathsByDijkstra(state, position, chests).FirstOrDefault();
                if (calculatedPath == null) return new List<Point>();
                position = calculatedPath.End;
                cost += calculatedPath.Cost;
                if (state.Energy < cost) return new List<Point>();
                chests.Remove(calculatedPath.End);
                nextMove = nextMove.Concat(calculatedPath.Path.Skip(1)).ToList();
            }
            return nextMove;
        }
    }
}