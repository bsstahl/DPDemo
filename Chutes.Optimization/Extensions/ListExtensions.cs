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
    }
}
