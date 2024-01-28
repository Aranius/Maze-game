using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Model
{
    public abstract class MapObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public ObjectTypeEnum ObjectType { get; set; }
    }
}

public enum ObjectTypeEnum
{
    PC = 0,
    NPC = 1,
    Door = 2,
    Trap = 3,
    Item = 4,
}
