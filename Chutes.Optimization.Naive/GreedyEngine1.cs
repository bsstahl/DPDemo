using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chutes.Optimization.Naive
{
    public class GreedyEngine1 : Interfaces.IShortestPathEngine
    {
        private Entities.Gameboard _board;

        public GreedyEngine1(Entities.Gameboard board)
        {
            _board = board.Clone();
        }

        public Entities.Path GetPath() // Take all ladders and skip all chutes
        {
            var result = new Entities.Path();
            var i = 1;
            while (i <= _board.LastIndex)
            {
                var space = _board[i];
                result.Add(space);
                if (space.PathTo.HasValue && space.PathTo.Value > i)
                {
                    space = _board[space.PathTo.Value];
                    result.Add(space);
                }
                i = space.Index + 1;
            }
            return result;
        }
    }
}
