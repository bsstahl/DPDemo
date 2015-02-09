using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chutes.Optimization.Interfaces
{
    public interface IShortestPathEngine
    {
        Entities.Path GetPathWithFewestSpacesTraversed(int startingIndex);

        Entities.Path GetPathWithFewestRollsNeeded(int startingIndex);
    }
}
