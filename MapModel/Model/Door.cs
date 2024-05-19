using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapModel.Model
{
    public class Door : MapObject
    {
        public bool Locked { get; set; }
        public bool Closed { get; set; } = true;

        public Door(int x, int y, bool locked = false)
        {
            Locked = locked;
            X = x;
            Y = y;
            ObjectType = ObjectTypeEnum.Door;
        }

        public string Unlock(PC pc)
        {
            var najdeny_kluc = pc.Inventory.FirstOrDefault(x => x.ItemType == ItemTypeEnum.Key);
            if (najdeny_kluc != null)
            {
                Locked = false;
                pc.Inventory.Remove(najdeny_kluc);
                return "Odomkol si uspesne dvere";
            }
            else
            {
                return "Nemas ziadny kluc!";
            }
        }

        public string Open()
        {
            if (!Locked)
            {
                Closed = false;
                return "Otvoril si dvere";
            }
            else
            {
                return "Dvere su zamknute";
            }
        }

        public override string ToString()
        {
            var el = Environment.NewLine;
            return $"Dvere{el}X: {X}{el}Y: {Y}{el}{nameof(Locked)}: {Locked}{el}{nameof(Closed)}: {Closed}";
        }
    }
}
