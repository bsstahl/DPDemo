using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortestPath.Optimization.Entities
{
    public class Grid
    {
        public int Width { get; set; }
        public int Height { get; set; }

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

                return GetLocation(x, y);
            }
        }

        private List<GridLocation> _list = new List<GridLocation>();

        public Grid(int gridWidth, int gridHeight, IEnumerable<Entities.GridLocation> roadblocks)
        {
            this.Width = gridWidth;
            this.Height = gridHeight;

            _list.Clear();
            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    var isRoadblock = roadblocks.Any(l => l.X == x && l.Y == y);
                    _list.Add(new GridLocation(x, y, isRoadblock));
                }
            }
        }

        public void Clear()
        {
            for (int x = 0; x < this.Width; x++)
            {
                for (int y = 0; y < this.Height; y++)
                {
                    var location = GetLocation(x, y);
                    location.DistanceFromStart = null;
                }
            }
        }

        public IEnumerable<Entities.GridLocation> LocationsAtDistance(int distance)
        {
            return _list.Where(l => l.DistanceFromStart.HasValue && l.DistanceFromStart == distance);
        }

        public bool LocationIsValid(int x, int y)
        {
            return (x < this.Width && x >= 0 && y < this.Height && y >= 0);
        }

        public bool LocationIsRoadblocked(int x, int y)
        {
            if (!LocationIsValid(x, y))
                throw new InvalidOperationException();

            var location = GetLocation(x, y);
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
                    var loc = GetLocation(x, y);
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

        public GridLocation GetLocation(int x, int y)
        {
            if (!this.LocationIsValid(x, y))
                return null;
            else
                return _list.Where(l => (l.X == x) && (l.Y == y)).SingleOrDefault();
        }
    }
}
