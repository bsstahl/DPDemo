using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chutes.Optimization;
using Chutes.Optimization.Entities;
using Chutes.Optimization.Extensions;

namespace Chutes
{
    class Program
    {
        static void Main(string[] args)
        {
            var pathways = new List<System.Collections.Generic.KeyValuePair<int, int>>();
            pathways.AddPair(1, 13);
            pathways.AddPair(9, 20);
            pathways.AddPair(12, 7);
            pathways.AddPair(23, 6);

            var board = new Gameboard(25, pathways);

        }
    }
}
