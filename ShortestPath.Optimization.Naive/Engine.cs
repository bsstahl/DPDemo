using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortestPath.Optimization.Naive
{
    public class Engine : ShortestPath.Optimization.Interfaces.IPathProvider, INotifyPropertyChanged
    {
        Random _random = new Random();

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
          var result = new ShortestPath.Optimization.Entities.Path();
            var currentLocation = result.Add(grid[grid.StartPoint.X, grid.StartPoint.Y]);
            currentLocation.IsStartPoint = true;
            currentLocation.DistanceFromStart = 0;

            while (!IsCurrentLocationTheEndPoint(currentLocation, grid.EndPoint))
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
                  if (IsCurrentLocationTheEndPoint(newLocation, grid.EndPoint))
                  {
                    newLocation.IsEndPoint = true;
                  }
                  currentLocation = result.Add(newLocation);
                }
              }
          }
          this.Path = result;
        }

        private static bool IsCurrentLocationTheEndPoint(Entities.GridLocation currentLocation, Entities.GridLocation endPoint)
        {
            return (currentLocation.X == endPoint.X) && (currentLocation.Y == endPoint.Y);
        }
    }
}
