using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chutes.Optimization.Entities
{
    public class Gamespace
    {
        public int Index { get; set; }

        public int? PathTo { get; set; }

        public int? DistanceFromStart { get; set; }
    }
}
