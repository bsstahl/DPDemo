using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShortestPath.Optimization.Entities;
using ShortestPath.Optimization.Extensions;
using ShortestPath.Optimization.Interfaces;

namespace ShortestPath.Optimization.DP
{
    public class Engine : IPathProvider
    {
        public Entities.Path FindPath(Entities.Grid grid, Entities.GridLocation startPoint, Entities.GridLocation endPoint)
        {
            var startingLocation = grid[startPoint.X, startPoint.Y];
            var endingLocation = grid[endPoint.X, endPoint.Y];
            ProcessLocation(0, grid, startingLocation, endingLocation);
            return RetraceSteps(grid, endPoint);
        }

        private Entities.Path RetraceSteps(Entities.Grid grid, Entities.GridLocation endPoint)
        {
            var resultPath = new Entities.Path();

            var currentLocation = grid[endPoint.X, endPoint.Y];
            resultPath.Add(currentLocation);

            var pathLength = currentLocation.DistanceFromStart.Value;
            for (int i = pathLength - 1; i >= 0; i--)
            {
                currentLocation = grid.LocationsAtDistance(i).SurroundingLocation(currentLocation).First();
                resultPath.Add(currentLocation);
            }

            return resultPath.Reverse();
        }

        private void ProcessLocation(int currentDistance, Entities.Grid grid, Entities.GridLocation startPoint, Entities.GridLocation endPoint)
        {
            var currentPoint = startPoint;

            if (!currentPoint.DistanceFromStart.HasValue ||
                currentPoint.DistanceFromStart.Value > currentDistance)
            {
                currentPoint.DistanceFromStart = currentDistance;

                for (int xDelta = -1; xDelta < 2; xDelta++)
                    for (int yDelta = -1; yDelta < 2; yDelta++)
                    {
                        var newLocation = grid.TraversableLocationAtOffset(currentPoint, xDelta, yDelta);
                        if (newLocation != null)
                            ProcessLocation(currentDistance + 1, grid, newLocation, endPoint);
                    }
            }
        }

    }
}
