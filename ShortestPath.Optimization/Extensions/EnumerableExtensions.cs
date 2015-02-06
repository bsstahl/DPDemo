using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortestPath.Optimization.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<Entities.GridLocation> SurroundingLocation(this IEnumerable<Entities.GridLocation> locations, Entities.GridLocation centerPoint)
        {
            var minX = centerPoint.X - 1;
            var maxX = centerPoint.X + 1;
            var minY = centerPoint.Y - 1;
            var maxY = centerPoint.Y + 1;
            return locations.Where(l => l.X >= minX && l.X <= maxX && l.Y >= minY && l.Y <= maxY);
        }
    }
}
