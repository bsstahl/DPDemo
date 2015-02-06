using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortestPath.Optimization.Entities
{
    public class Path
    {
        LinkedList<GridLocation> _list = new LinkedList<GridLocation>();
        LinkedListNode<GridLocation> _currentNode = null;

        public int Length
        {
            get
            {
                return _list.Count() - 1;
            }
        }

        public GridLocation Add(GridLocation newLocation)
        {
            if (_currentNode == null)
                _currentNode = _list.AddFirst(newLocation);
            else
                _currentNode = _list.AddAfter(_currentNode, newLocation);
            return newLocation;
        }

        public Path Reverse()
        {
            var result = new Path();
            foreach (var item in _list.Reverse())
            {
                result.Add(item);
            }
            return result;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var item in _list)
            {
                sb.AppendLine(String.Format("{0},{1}", item.X, item.Y));
            }
            return sb.ToString();
        }
    }
}
