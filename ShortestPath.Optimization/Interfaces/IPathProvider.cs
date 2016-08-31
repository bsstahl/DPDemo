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
      void FindPath(Entities.Grid grid);
      event PropertyChangedEventHandler PropertyChanged;
      Entities.Path Path { get; set; }
    }
}
