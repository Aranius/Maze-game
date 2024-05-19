using MapModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    public static class Extensions
    {
        public static bool IsEquiped(this Item item, PC pc)
        {
            var equip = item as Equipment;
            if (equip == null)
            {
                return false;
            }

            pc.Equipment.TryGetValue(equip.EquipemntType.ToString(), out var equippedItem);
            return equippedItem == equip;
        }

        public static bool IsEquiped(this Item item, NPC npc)
        {
            return npc.Inventory.Any(x=>x.Name == item.Name);
        }
    }
}
