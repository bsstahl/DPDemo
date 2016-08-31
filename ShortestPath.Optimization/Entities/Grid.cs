using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortestPath.Optimization.Entities
{
  public class Grid : INotifyPropertyChanged
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

    #region Width
    private int _width = 0;
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
    private int _height = 0;
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
    private GridLocation _endPoint = null;
    public GridLocation EndPoint
    {
      get { return _endPoint; }
      set
      {
        _endPoint = value;
        this.NotifyPropertyChanged("EndPoint");
      }
    }
    #endregion EndPoint

    private GridLocation[,] _gridLocations;

    /// <summary>
    /// Gets or Sets the distance value from the GridLocation
    /// object at the specified x,y location in the Grid
    /// </summary>
    public GridLocation this[int x, int y]
    {
      get
      {
        if (!LocationIsValid(x, y))
          throw new InvalidOperationException();

        return _gridLocations[x, y];
      }
      set
      {
        if (!LocationIsValid(x, y))
          throw new InvalidOperationException();

        _gridLocations[x, y] = value;

        this.NotifyPropertyChanged("Item[]");
      }
    }

    #region GridLocation
    public GridLocation this[string index]
    {
      get 
      {
        int x, y;
        this.SplitIndex(index, out x, out y);
        return _gridLocations[x, y]; 
      }
      set
      {
        int x, y;
        this.SplitIndex(index, out x, out y);
        _gridLocations[x, y] = value;
        this.NotifyPropertyChanged("GridLocation");
      }
    }
    #endregion GridLocation

    private void SplitIndex(string index, out int x, out int y)
    {
      var parts = index.Split('-');
      if (parts.Length != 2)
        throw new ArgumentException("The provided index is not valid.");

      x = int.Parse(parts[0]);
      y = int.Parse(parts[1]);
    }

    public Grid(int gridWidth, int gridHeight, IEnumerable<Entities.GridLocation> roadblocks, Entities.GridLocation startPoint, Entities.GridLocation endPoint)
    {
      this.Width = gridWidth;
      this.Height = gridHeight;

      this.StartPoint = startPoint;
      this.EndPoint = endPoint;

      _gridLocations = new GridLocation[gridHeight, gridWidth];
      for (int x = 0; x < gridWidth; x++)
      {
        for (int y = 0; y < gridHeight; y++)
        {
          if (x == startPoint.X && y == startPoint.Y)
          {
            _gridLocations[x, y] = startPoint;
          }
          else if (x == endPoint.X && y == endPoint.Y)
          {
            _gridLocations[x, y] = endPoint;
          }
          else
          {
            var isRoadblock = roadblocks.Any(l => l.X == x && l.Y == y);
            _gridLocations[x, y] = new GridLocation(x, y, isRoadblock);
          }
        }
      }
    }

    public IEnumerable<Entities.GridLocation> LocationsAtDistance(int distance)
    {
      List<Entities.GridLocation> locs = new List<GridLocation>();
      for (int x = 0; x < this.Width; x++)
      {
        for (int y = 0; y < this.Height; y++)
        {
          GridLocation l = _gridLocations[x, y];
          if (l.DistanceFromStart.HasValue && l.DistanceFromStart == distance)
          {
            locs.Add(l);
          }
        }
      }
      return locs;
    }

    public bool LocationIsValid(int x, int y)
    {
      return (x < this.Width && x >= 0 && y < this.Height && y >= 0);
    }

    public bool LocationIsRoadblocked(int x, int y)
    {
      if (!LocationIsValid(x, y))
        throw new InvalidOperationException();

      var location = _gridLocations[x, y];
      System.Diagnostics.Debug.Assert(location != null);
      return location.IsRoadblock;
    }

    public bool LocationIsTraversable(int x, int y)
    {
      return (LocationIsValid(x, y) && !LocationIsRoadblocked(x, y));
    }

    public GridLocation TraversableLocationAtOffset(GridLocation startingPoint, int xOffset, int yOffset)
    {
      // Note: Offset of 0,0 is considered not-traversable because
      // you can't move from where you are to there. It is not a valid movement.
      GridLocation result = null;

      if (xOffset != 0 || yOffset != 0)
      {
        var xNew = startingPoint.X + xOffset;
        var yNew = startingPoint.Y + yOffset;
        if (LocationIsValid(xNew, yNew))
        {
          var newLocation = this[xNew, yNew];
          if (!newLocation.IsRoadblock)
            result = newLocation;
        }
      }

      return result;
    }

    public override string ToString()
    {
      var sb = new StringBuilder();

      sb.AppendLine(string.Format(" {0} ", new String('-', this.Width * 5))); // Header line
      sb.AppendLine(string.Format("|{0}|", new String(' ', this.Width * 5))); // Spacer line


      for (int y = this.Height - 1; y >= 0; y--)
      {
        sb.Append(string.Format("|"));
        for (int x = 0; x < this.Width; x++)
        {
          var loc = _gridLocations[x, y];
          if (loc.IsRoadblock)
            sb.Append(" XXX ");
          else if (loc.DistanceFromStart.HasValue)
            sb.Append(loc.DistanceFromStart.Value.ToString(" 000 "));
          else
            sb.Append(" ??? ");
        }
        sb.AppendLine(string.Format("|"));
        sb.AppendLine(string.Format("|{0}|", new String(' ', this.Width * 5))); // Spacer line
      }

      sb.AppendLine(string.Format(" {0} ", new String('-', this.Width * 5))); // Footer line

      return sb.ToString();
    }
  }
}
