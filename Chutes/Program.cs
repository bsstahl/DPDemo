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
            pathways.AddPair(1, 38);
            pathways.AddPair(4, 14);
            pathways.AddPair(9, 31);
            pathways.AddPair(16, 6);
            pathways.AddPair(21,42);
            pathways.AddPair(28,84);
            pathways.AddPair(36,44);
            pathways.AddPair(47,26);
            pathways.AddPair(49,11);
            pathways.AddPair(51,67);
            pathways.AddPair(56,53);
            pathways.AddPair(62,19);
            pathways.AddPair(64,60);
            pathways.AddPair(71,91);
            pathways.AddPair(80,100);
            pathways.AddPair(87,24);
            pathways.AddPair(93,73);
            pathways.AddPair(95,75);
            pathways.AddPair(98,78);

            var board = new Gameboard(100, pathways);
            Chutes.Optimization.Interfaces.IShortestPathEngine engine = new Chutes.Optimization.Naive.GreedyEngine1(board);
            // Chutes.Optimization.Interfaces.IShortestPathEngine engine = new Chutes.Optimization.DP.Engine(board);

            var path = engine.GetPath();
            Console.WriteLine("Fewest spaces traversed:");
            Console.WriteLine(path.ToString());
            Console.WriteLine("Path length: {0}", path.Length);

            //Console.WriteLine("");

            //var fewestRollsPath = engine.GetPathWithFewestRollsNeeded();
            //Console.WriteLine("Fewest rolls:");
            //Console.WriteLine(fewestRollsPath.ToString());
            //Console.WriteLine("Rolls: {0}", fewestRollsPath.Length);


        }
    }
}
