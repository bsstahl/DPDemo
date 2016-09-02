using ShortestPath.Optimization.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortestPath.ViewModels
{
    public class Grid : Optimization.Entities.Grid, INotifyPropertyChanged
    {
        public Grid(int gridWidth, int gridHeight, IEnumerable<Optimization.Entities.GridLocation> roadblocks, GridLocation startPoint, GridLocation endPoint) : base(gridWidth, gridHeight, roadblocks)
        {
            this.StartPoint = startPoint;
            this.EndPoint = endPoint;
        }

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

        public new int Width
        {
            get { return base.Width; }
            set
            {
                base.Width = value;
                this.NotifyPropertyChanged("Width");
            }
        }

        #endregion Width

        #region Height

        public new int Height
        {
            get { return base.Height; }
            set
            {
                base.Height = value;
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

        public Optimization.Entities.GridLocation this[string index]
        {
            get
            {
                int x, y;
                this.SplitIndex(index, out x, out y);
                return base[x, y];
            }
            //set
            //{
            //    int x, y;
            //    this.SplitIndex(index, out x, out y);
            //    base[x, y] = value;

            //    // TODO: Move NotifyPropertyChanged
            //    // this.NotifyPropertyChanged("GridLocation");
            //}
        }

        #endregion GridLocation


    }
}
