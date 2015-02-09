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

        public Entities.Path GetPathWithFewestSpacesTraversed(int startingIndex)
        {
            // Take all ladders and skip all chutes
            if (startingIndex > _board.LastIndex)
                throw new InvalidOperationException();

            var result = new Entities.Path();

            var i = startingIndex;
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

        public Entities.Path GetPathWithFewestRollsNeeded(int startingIndex)
        {
            // Roll all 6s unless it moves you past the end
            if (startingIndex > _board.LastIndex)
                throw new InvalidOperationException();

            var result = new Entities.Path();

            var i = startingIndex;
            var done = false;
            while (!done)
            {
                var space = _board[i];
                result.Add(space);
                if (space.PathTo.HasValue)
                {
                    space = _board[space.PathTo.Value];
                    result.Add(space);
                }

                if (i == _board.LastIndex)
                    done = true;
                else
                {
                    i = space.Index + 6;
                    if (i > _board.LastIndex)
                        i = _board.LastIndex;
                }
            }

            return result;
        }
    }
}
