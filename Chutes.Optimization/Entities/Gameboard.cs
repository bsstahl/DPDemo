using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chutes.Optimization.Extensions;

namespace Chutes.Optimization.Entities
{
    public class Gameboard
    {
        List<Gamespace> _spaces = new List<Gamespace>();

        public Gameboard(int size, IEnumerable<KeyValuePair<int, int>> pathways)
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

        public Gamespace this[int index]
        {
            get { return _spaces[index]; }
        }

        public int LastIndex 
        {
            get { return _spaces.Count - 1; }
        }

        public Gameboard Clone()
        {
            var pathways = _spaces.Pathways()
                .Select(s => new KeyValuePair<int, int>(s.Index, s.PathTo.Value))
                .AsEnumerable();

            return new Gameboard(_spaces.Count(), pathways);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(" ");
            for (var i = _spaces.Count - 1; i >= 0; i--)
            {
                var s = _spaces[i];
                if (s.DistanceFromStart.HasValue)
                    sb.Append(s.DistanceFromStart.Value.ToString("00"));
                else
                    sb.Append("??");
                sb.Append(" ");
            }
            sb.AppendLine("");
            foreach (var item in _spaces.Pathways())
            {
                sb.Append(String.Format("{0:00}->{1:00} ", item.Index, item.PathTo.Value));
            }

            sb.AppendLine("");

            return sb.ToString();
        }
    }
}
