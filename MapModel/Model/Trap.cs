using Maze.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Model
{
    public class Trap : MapObject, ITrap
    {
        public int TeleToX { get; set; }
        public int TeleToY { get; set; }
        public bool IsActive { get; set; }

        public Trap()
        {
            ObjectType = ObjectTypeEnum.Trap;
            IsActive = true;
        }

        public string TrapTriggered(PC pc)
        {
            if (IsActive)
            {
                pc.X = TeleToX;
                pc.Y = TeleToY;
                IsActive = false;

                return "Vstupil si na pascu";
            }

            return "Preskocil si pascu";
        }
        public override string ToString()
        {
            var el = Environment.NewLine;
            return $"Trap{el}X: {X}{el}Y: {Y}{el}{nameof(TeleToX)}: {TeleToX}{el}" +
                $"{nameof(TeleToY)}: {TeleToY}{el}{nameof(IsActive)}: {IsActive}";
        }
    }
}
