using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Model
{
    public class PC : Creature
    {
        public List<Item> Inventory { get; set; }

        public PC()
        {
            ObjectType = ObjectTypeEnum.PC;
            Inventory = new List<Item>();
        }

        public string TakeItem(List<MapObject> mapa)
        {
            var obj = mapa.FirstOrDefault(i=>i.X == X && i.Y == Y);
            if (obj is Item item)
            {
                Inventory.Add(item);
                mapa.Remove(item);

                return $"Zobral som predmet {item.Name} do svojho inventara.";
            }

            return "Nevidim tu nic co by som mohol zobrat";
        }

        public string LookAround(List<MapObject> mapa)
        {
            var obj = mapa.FirstOrDefault(i => i.X == X && i.Y == Y);

            if (obj is Item item)
            {
                return $"Vidis na zemi: {item.Name} vyzera ako {item.Description}";
            }

            return "Nic nevidis na zemi!";
        }

        public string UseItem(MapObject obj, bool[,] maze)
        {
            if (obj is Door door)
            {
                if (door.Locked)
                {
                    return door.Unlock(this);
                }

                if (door.Closed)
                {
                    return door.Open();
                }
            }

            return "Nie je tu nic na pouzitie";
        }
    }
}
