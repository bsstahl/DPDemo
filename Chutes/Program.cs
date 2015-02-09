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
            Chutes.Optimization.Interfaces.IShortestPathEngine engine = new Chutes.Optimization.Naive.GreedyEngine1(board);

            var fewestSpacesPath = engine.GetPathWithFewestSpacesTraversed(0);
            Console.WriteLine("Fewest spaces traversed:");
            Console.WriteLine(fewestSpacesPath.ToString());
            Console.WriteLine("Path length: {0}", fewestSpacesPath.Length);

            Console.WriteLine("");

            var fewestRollsPath = engine.GetPathWithFewestRollsNeeded(0);
            Console.WriteLine("Fewest rolls:");
            Console.WriteLine(fewestRollsPath.ToString());
            Console.WriteLine("Rolls: {0}", fewestRollsPath.RollCount);
        }
    }
}
