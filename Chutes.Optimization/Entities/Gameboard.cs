using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chutes.Optimization.Entities
{
    public class Gameboard
    {
        List<Gamespace> _spaces = new List<Gamespace>();

        public Gameboard(int size, List<KeyValuePair<int, int>> pathways)
        {
            _spaces.Clear();
            for (int i = 0; i < size; i++)
            {
                var newSpace = new Gamespace() { Index = i };
                if (pathways.Any(p => p.Key == i))
                    newSpace.PathTo = pathways.Single(p => p.Key == i).Value;
                _spaces.Add(newSpace);
            }
        }
    }
}
