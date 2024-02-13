using Maze.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapModel.Model
{
    public class Start : MapObject
    {
        public Start()
        {
            ObjectType = ObjectTypeEnum.Start;
        }

        override public string ToString()
        {
            return $"Start{Environment.NewLine}X: {X}{Environment.NewLine}Y: {Y}";
        }
    }
}
