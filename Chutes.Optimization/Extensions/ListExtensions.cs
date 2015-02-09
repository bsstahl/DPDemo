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

    }
}
