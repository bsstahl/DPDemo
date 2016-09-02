using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortestPath.Optimization.Interfaces
{
    public interface IPathProvider
    {
        Entities.Path FindPath(Entities.Grid grid, Entities.GridLocation startPoint, Entities.GridLocation endPoint);

        // TODO: Reimplement PropertyChanged
        // event PropertyChangedEventHandler PropertyChanged;
        // Entities.Path Path { get; set; }
    }
}
