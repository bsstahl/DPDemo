using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chutes.Optimization.Entities
{
    public class Path
    {
        LinkedList<Gamespace> _path = new LinkedList<Gamespace>();

        public Path()
        {
            _path.Clear();
        }

        public Gamespace Add(Gamespace space)
        {
            if (_path.Any())
                _path.AddAfter(_path.Last, space);
            else
                _path.AddFirst(space);
            return space;
        }

        public int Length 
        {
            get { return _path.Count; }
        }

        public int RollCount
        {
            get { return _path.Where(s => !s.PathTo.HasValue).Count(); }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var item in _path)
                sb.Append(item.Index.ToString("00") + " ");
            return sb.ToString();
        }

        public IEnumerable<Gamespace> Reverse()
        {
            throw new NotImplementedException();
        }

    }
}
