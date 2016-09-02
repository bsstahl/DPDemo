﻿using ShortestPath.Optimization.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortestPath
{
    class Program
    {
        const int _gridSize = 6;

        static void Main(string[] args)
        {
            Optimization.Entities.Grid grid = ConstructGrid();

            var startPoint = grid.GetLocation(0, 2);
            var endPoint = grid.GetLocation(3, 1);

            while (true)
            {
                // Construct the appropriate path engine
                // Optimization.Interfaces.IPathProvider engine = new ShortestPath.Optimization.Naive.Engine();
                Optimization.Interfaces.IPathProvider engine = new ShortestPath.Optimization.DP.Engine();

                grid.Clear();

                Console.WriteLine(grid.ToString()); // Display initial grid
                Path path = engine.FindPath(grid, startPoint, endPoint);
                Console.WriteLine(grid.ToString()); // Display final grid

                // Display results
                Console.WriteLine(path.ToString());
                Console.WriteLine("Path length: {0}", path.Length);
                Console.WriteLine("Distance from start of endpoint: {0}", endPoint.DistanceFromStart);

                Console.ReadKey();
            }
        }

        private static Optimization.Entities.Grid ConstructGrid()
        {
            var roadblocks = new List<Optimization.Entities.GridLocation>();
            roadblocks.Add(new Optimization.Entities.GridLocation(1, 1, true));
            roadblocks.Add(new Optimization.Entities.GridLocation(1, 2, true));
            roadblocks.Add(new Optimization.Entities.GridLocation(1, 3, true));
            roadblocks.Add(new Optimization.Entities.GridLocation(2, 0, true));
            roadblocks.Add(new Optimization.Entities.GridLocation(2, 1, true));

            return new Optimization.Entities.Grid(_gridSize, _gridSize, roadblocks);
        }
    }
}
