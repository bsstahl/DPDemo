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
        static Optimization.Entities.Grid grid;
        static Optimization.Interfaces.IPathProvider engine;

        static void Main(string[] args)
        {
            grid = ConstructGrid();
            Console.WriteLine(grid.ToString()); // Display initial grid

            //var startPoint = new Optimization.Entities.GridLocation(0, 2);
            //var endPoint = new Optimization.Entities.GridLocation(3, 1);

            // Construct the appropriate path engine
            // Optimization.Interfaces.IPathProvider engine = new ShortestPath.Optimization.Naive.Engine();
            engine = new ShortestPath.Optimization.DP.Engine();

            engine.PropertyChanged += engine_PropertyChanged;

            engine.FindPath(grid);

        }

        static void engine_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Path")
            {
                Console.WriteLine(grid.ToString()); // Display final grid

                Console.WriteLine("Path length: {0}", engine.Path.Length);
                Console.WriteLine(engine.Path.ToString());
            }
        }

        private static Optimization.Entities.Grid ConstructGrid()
        {
            var startPoint = new Optimization.Entities.GridLocation(0, 2);
            var endPoint = new Optimization.Entities.GridLocation(3, 1);

            var roadblocks = new List<Optimization.Entities.GridLocation>();
            roadblocks.Add(new Optimization.Entities.GridLocation(1, 1, true));
            roadblocks.Add(new Optimization.Entities.GridLocation(1, 2, true));
            roadblocks.Add(new Optimization.Entities.GridLocation(1, 3, true));
            roadblocks.Add(new Optimization.Entities.GridLocation(2, 0, true));
            roadblocks.Add(new Optimization.Entities.GridLocation(2, 1, true));

            return new Optimization.Entities.Grid(_gridSize, _gridSize, roadblocks, startPoint, endPoint);
        }
    }
}
