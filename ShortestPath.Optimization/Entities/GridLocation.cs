using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ShortestPath.Optimization.Entities
{
  public class GridLocation : INotifyPropertyChanged
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

    public int X { get; set; }
    public int Y { get; set; }

    int? _distanceFromStart = null;
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

    bool _isRoadBlock = false;
    public bool IsRoadblock 
    {
      get { return _isRoadBlock; }
      set
      {
        _isRoadBlock = value;
        this.NotifyPropertyChanged("IsRoadBlock");
        this.NotifyPropertyChanged("Text");
        this.NotifyPropertyChanged("BGColor");
        this.NotifyPropertyChanged("FGColor");
      }
    }

    public GridLocation(int x, int y) : this(x, y, false) { }

    public GridLocation(int x, int y, bool isRoadblock)
    {
      X = x;
      Y = y;
      IsRoadblock = isRoadblock;
      DistanceFromStart = null;
    }

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
        {
          text = "XXX";
        }
        else if (this.DistanceFromStart.HasValue)
        {
          text = this.DistanceFromStart.Value.ToString("000");
        }
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
        {
          return new SolidColorBrush(Colors.Red);
        }
        else if (this.IsEndPoint)
        {
          return new SolidColorBrush(Colors.Maroon);
        }
        else if (this.IsRoadblock)
        {
          return new SolidColorBrush(Colors.Black);
        }
        else if (this.DistanceFromStart != null)
        {
          return new SolidColorBrush(Colors.Teal);
        }
        else
        {
          return new SolidColorBrush(Colors.Silver);
        }
      }
    }
    #endregion BGColor

    #region FGColor
    public Brush FGColor
    {
      get 
      {
        if (this.IsStartPoint)
        {
          return new SolidColorBrush(Colors.White);
        }
        else if (this.IsEndPoint)
        {
          return new SolidColorBrush(Colors.White);
        }
        else if (this.IsRoadblock)
        {
          return new SolidColorBrush(Colors.White);
        }
        else if (this.DistanceFromStart != null)
        {
          return new SolidColorBrush(Colors.Yellow);
        }
        else
        {
          return new SolidColorBrush(Colors.Black);
        }
      }
    }
    #endregion FGColor
  }
}
