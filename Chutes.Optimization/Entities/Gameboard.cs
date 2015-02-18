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
            for (int i = 1; i <= size; i++)
            {
                var newSpace = new Gamespace() { Index = i };
                if (pathways.Any(p => p.Key == i))
                    newSpace.PathTo = pathways.Single(p => p.Key == i).Value;
                _spaces.Add(newSpace);
            }
        }

        public Gamespace this[int index]
        {
            get { return _spaces.Single(s => s.Index == index); }
        }

        public int LastIndex 
        {
            get { return _spaces.Count; }
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
            for (var i = _spaces.Count; i > 0; i--)
            {
                var s = this[i];
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

        public Gamespace PathFrom(int spaceIndex)
        {
            return _spaces.SingleOrDefault(s => s.PathTo.HasValue && s.PathTo.Value == spaceIndex);
        }

        public IEnumerable<Gamespace> SpacesAtDistance(int distance)
        {
            return _spaces.Where(s => s.DistanceFromStart == distance);
        }

        public IEnumerable<Gamespace> NeighborsAtDistance(int distance, int currentIndex)
        {
            return this.SpacesAtDistance(distance).Where(s => (s.Index == (currentIndex - 1)) || (s.PathTo.HasValue && s.PathTo.Value == currentIndex));
        }
    }
}
