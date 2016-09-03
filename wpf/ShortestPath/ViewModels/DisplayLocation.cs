using System;
using System.ComponentModel;
using System.Windows.Media;

namespace ShortestPath.ViewModels
{
    public class DisplayLocation : INotifyPropertyChanged
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

        #region DistanceFromStart

        private int? _distanceFromStart;
        public int? DistanceFromStart
        {
            get { return _distanceFromStart; }
            set
            {
                _distanceFromStart = value;
                this.NotifyPropertyChanged("DistanceFromStart");
                this.NotifyPropertyChanged("Text");
                this.NotifyPropertyChanged("BGColor");
                this.NotifyPropertyChanged("FGColor");
            }
        }

        #endregion

        #region IsRoadblock

        private bool _isRoadblock;
        public bool IsRoadblock
        {
            get { return _isRoadblock; }
            set
            {
                _isRoadblock = value;
                this.NotifyPropertyChanged("IsRoadBlock");
                this.NotifyPropertyChanged("Text");
                this.NotifyPropertyChanged("BGColor");
                this.NotifyPropertyChanged("FGColor");
            }
        }

        #endregion

        //public GridLocation(int x, int y) : base(x, y, false) { }

        //public GridLocation(int x, int y, bool isRoadblock):base(x,y,isRoadblock) { }

        public int X { get; set; }

        public int Y { get; set; }

        #region IsStartPoint

        private bool _isStartPoint = false;
        public bool IsStartPoint
        {
            get { return _isStartPoint; }
            set
            {
                _isStartPoint = value;
                this.NotifyPropertyChanged("IsStartPoint");
                this.NotifyPropertyChanged("BGColor");
                this.NotifyPropertyChanged("FGColor");
            }
        }

        #endregion IsStartPoint

        #region IsEndPoint

        private bool _isEndPoint = false;
        public bool IsEndPoint
        {
            get { return _isEndPoint; }
            set
            {
                _isEndPoint = value;
                this.NotifyPropertyChanged("IsEndPoint");
                this.NotifyPropertyChanged("BGColor");
                this.NotifyPropertyChanged("FGColor");
            }
        }

        #endregion IsEndPoint

        #region Text

        public string Text
        {
            get
            {
                string text = "???";

                if (this.IsRoadblock)
                    text = "XXX";
                else if (this.DistanceFromStart.HasValue)
                    text = this.DistanceFromStart.Value.ToString("000");

                return text;
            }
        }

        #endregion Text

        #region BGColor

        public Brush BGColor
        {
            get
            {
                if (this.IsStartPoint)
                    return new SolidColorBrush(Colors.Red);
                else if (this.IsEndPoint)
                    return new SolidColorBrush(Colors.Maroon);
                else if (this.IsRoadblock)
                    return new SolidColorBrush(Colors.Black);
                else if (this.DistanceFromStart != null)
                    return new SolidColorBrush(Colors.Teal);
                else
                    return new SolidColorBrush(Colors.Silver);
            }
        }

        #endregion BGColor

        #region FGColor

        public Brush FGColor
        {
            get
            {
                if (this.IsStartPoint)
                    return new SolidColorBrush(Colors.White);
                else if (this.IsEndPoint)
                    return new SolidColorBrush(Colors.White);
                else if (this.IsRoadblock)
                    return new SolidColorBrush(Colors.White);
                else if (this.DistanceFromStart != null)
                    return new SolidColorBrush(Colors.Yellow);
                else
                    return new SolidColorBrush(Colors.Black);
            }
        }

        #endregion FGColor
    }
}
