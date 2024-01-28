using Maze.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Interfaces
{
    public interface ITrap
    {
        public string TrapTriggered(PC pc);
    }
}
