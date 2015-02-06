using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortestPath.Optimization.Naive
{
    public class Engine : ShortestPath.Optimization.Interfaces.IPathProvider
    {
        Random _random = new Random();

        public Entities.Path FindPath(Entities.Grid grid, Entities.GridLocation startPoint, Entities.GridLocation endPoint)
        {
            var result = new ShortestPath.Optimization.Entities.Path();
            var currentLocation = result.Add(grid[startPoint.X, startPoint.Y]);
            currentLocation.DistanceFromStart = 0;

            while (!IsCurrentLocationTheEndPoint(currentLocation, endPoint))
            {
                var moveX = _random.Next(3) - 1;
                var moveY = _random.Next(3) - 1;
                if (moveX != 0 || moveY != 0)
                {
                    var newX = currentLocation.X + moveX;
                    var newY = currentLocation.Y + moveY;

                    if (grid.LocationIsTraversable(newX, newY))
                    {
                        var newLocation = grid[newX, newY];
                        newLocation.DistanceFromStart = currentLocation.DistanceFromStart + 1;
                        currentLocation = result.Add(newLocation);
                    }
                }
            } 

            return result;
        }

        private static bool IsCurrentLocationTheEndPoint(Entities.GridLocation currentLocation, Entities.GridLocation endPoint)
        {
            return (currentLocation.X == endPoint.X) && (currentLocation.Y == endPoint.Y);
        }
    }
}
