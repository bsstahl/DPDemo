using ShortestPath.Optimization.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortestPath.ViewModels
{
    public class DisplayGrid : INotifyPropertyChanged
    {
        public DisplayGrid(Grid grid, GridLocation startPoint, GridLocation endPoint)
        {
            this.StartPoint = startPoint;
            this.EndPoint = endPoint;

            for (int x = 0; x < grid.Width; x++)
                for (int y = 0; y < grid.Height; y++)
                    _list.Add(new DisplayLocation()
                    { 
                        X = x,
                        Y = y,
                        DistanceFromStart = grid[x,y].DistanceFromStart,
                        IsEndPoint = (x == endPoint.X && y == endPoint.Y),
                        IsStartPoint = (x == startPoint.X && y == startPoint.Y),
                        IsRoadblock = grid[x,y].IsRoadblock
                    });
        }

        private List<DisplayLocation> _list = new List<DisplayLocation>();

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

        #region Width

        private int _width;
        public int Width
        {
            get { return _width; }
            set
            {
                _width = value;
                this.NotifyPropertyChanged("Width");
            }
        }

        #endregion Width

        #region Height

        private int _height;
        public int Height
        {
            get { return _height; }
            set
            {
                _height = value;
                this.NotifyPropertyChanged("Height");
            }
        }

        #endregion Height

        #region StartPoint

        private GridLocation _startPoint = null;
        public GridLocation StartPoint
        {
            get { return _startPoint; }
            set
            {
                _startPoint = value;
                this.NotifyPropertyChanged("StartPoint");
            }
        }

        #endregion StartPoint

        #region EndPoint

        private Optimization.Entities.GridLocation _endPoint = null;
        public Optimization.Entities.GridLocation EndPoint
        {
            get { return _endPoint; }
            set
            {
                _endPoint = value;
                this.NotifyPropertyChanged("EndPoint");
            }
        }

        #endregion EndPoint

        private void SplitIndex(string index, out int x, out int y)
        {
            var parts = index.Split('-');
            if (parts.Length != 2)
                throw new ArgumentException("The provided index is not valid.");

            x = int.Parse(parts[0]);
            y = int.Parse(parts[1]);
        }

        #region GridLocation

        public DisplayLocation this[string index]
        {
            get
            {
                int x, y;
                this.SplitIndex(index, out x, out y);
                return _list.Single(l => l.X == x && l.Y == y);
            }
            //set
            //{
            //    int x, y;
            //    this.SplitIndex(index, out x, out y);
            //    this.NotifyPropertyChanged("GridLocation");
            //    base[x, y] = value;
            //}
        }

        #endregion GridLocation


    }
}
