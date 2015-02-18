using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chutes.Optimization.Extensions;

namespace Chutes.Optimization.DP
{
    public class Engine : Interfaces.IShortestPathEngine
    {
        Entities.Gameboard _board;

        public Engine(Entities.Gameboard board)
        {
            _board = board.Clone();
        }

        public Entities.Path GetPath()
        {
            Console.WriteLine(_board.ToString());
            DetermineDistance(1, 1);
            Console.WriteLine(_board.ToString());
            return RetracePath();
        }

        private void DetermineDistance(int spaceIndex, int distanceFromStart)
        {
            var space = _board[spaceIndex];
            if (!space.DistanceFromStart.HasValue ||
                space.DistanceFromStart.Value > distanceFromStart)
            {
                space.DistanceFromStart = distanceFromStart;
                if (space.PathTo.HasValue)
                    DetermineDistance(space.PathTo.Value, distanceFromStart + 1);
                if (space.Index < _board.LastIndex)
                    DetermineDistance(space.Index + 1, distanceFromStart + 1);
            }
        }

        private Entities.Path RetracePath()
        {
            var result = new Entities.Path();
            var spc = _board[_board.LastIndex];
            result.Add(spc);
            for (int i = spc.DistanceFromStart.Value - 1; i > 0; i--)
            {
                spc = _board.NeighborsAtDistance(i, spc.Index).First();
                result.Add(spc);
            }
            return result.Reverse();
        }
    }
}
