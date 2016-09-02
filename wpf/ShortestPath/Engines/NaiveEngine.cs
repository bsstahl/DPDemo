using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using ShortestPath.ViewModels;

namespace ShortestPath.Engines
{
    public class NaiveEngine: Optimization.Naive.Engine, INotifyPropertyChanged
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

        // TODO: Reimplement if needed
        //public new Optimization.Entities.Path Path
        //{
        //    get { return base.Path; }
        //    set
        //    {
        //        base.Path = value;
        //        this.NotifyPropertyChanged("Path");
        //    }
        //}

        #endregion Path

        //public Optimization.Entities.Path FindPath(Grid grid, GridLocation startPoint, GridLocation endPoint)
        //{
        //    return base.FindPath(grid, startPoint, endPoint);
        //}

        private static bool IsCurrentLocationTheEndPoint(Optimization.Entities.GridLocation currentLocation, Optimization.Entities.GridLocation endPoint)
        {
            return (currentLocation.X == endPoint.X) && (currentLocation.Y == endPoint.Y);
        }

    }
}
