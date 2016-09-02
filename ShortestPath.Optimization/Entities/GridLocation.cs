using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ShortestPath.Optimization.Entities
{
    public class GridLocation
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int? DistanceFromStart { get; set; }
        public bool IsRoadblock { get; set; }

        public GridLocation(int x, int y) : this(x, y, false) { }

        public GridLocation(int x, int y, bool isRoadblock)
        {
            X = x;
            Y = y;
            IsRoadblock = isRoadblock;
            DistanceFromStart = null;
        }

    }
}
