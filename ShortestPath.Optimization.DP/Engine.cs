using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShortestPath.Optimization.Extensions;
using System.ComponentModel;

namespace ShortestPath.Optimization.DP
{
    public class Engine : ShortestPath.Optimization.Interfaces.IPathProvider, INotifyPropertyChanged
    {
      #region INotifyPropertyChanged Implementation
      public event PropertyChangedEventHandler PropertyChanged;
      public void NotifyPropertyChanged(string propertyName)
      {
        var handler = PropertyChanged;
        if (handler != null)
        {
          handler(this, new PropertyChangedEventArgs(propertyName));
        }
      }
      #endregion INotifyPropertyChanged Implementation

      #region Path
      private Entities.Path _path = null;
      public Entities.Path Path
      {
        get { return _path; }
        set
        {
          _path = value;
          this.NotifyPropertyChanged("Path");
        }
      }
      #endregion Path

        public void FindPath(Entities.Grid grid)
        {
          Entities.Path path = null;
          var startingLocation = grid[grid.StartPoint.X, grid.StartPoint.Y];
          var endingLocation = grid[grid.EndPoint.X, grid.EndPoint.Y];
          ProcessLocation(0, grid, startingLocation, endingLocation);
          path = RetraceSteps(grid, grid.EndPoint);
          this.Path = path;
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
