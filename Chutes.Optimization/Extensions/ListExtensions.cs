using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chutes.Optimization.Extensions
{
    public static class ListExtensions
    {
        public static KeyValuePair<int, int> AddPair(this IList<KeyValuePair<int, int>> list, int start, int end)
        {
            var newPair = new KeyValuePair<int, int>(start, end);
            list.Add(newPair);
            return newPair;
        }

        public static IEnumerable<Entities.Gamespace> Pathways(this IEnumerable<Entities.Gamespace> list)
        {
            return list.Where(s => s.PathTo.HasValue);
        }

        public static IEnumerable<Entities.Gamespace> SingleRollPredecessors(this IEnumerable<Entities.Gamespace> spaces, int index)
        {
            var withinRollDistance = spaces.Where(s => (s.Index > (index - 7)));
            var withinDistanceIndexes = withinRollDistance.Select(d => d.Index);
            var withinPath = spaces.Where(s => s.PathTo.HasValue && withinDistanceIndexes.Contains(s.PathTo.Value));
            return withinRollDistance.Union(withinPath);
        }

    }
}
