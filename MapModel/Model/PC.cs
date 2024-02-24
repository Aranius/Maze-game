using MapModel.Model;
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
        public int Level { get; set; }
        public int XP { get; set; }
        public int NextLevelXP { get; set; }

        public List<Item> Inventory { get; set; }
        public Dictionary<string, Item> Equipment { get; set; }
        public int Gold { get; set; }

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

        public void EarnXP(int amount)
        {
            XP += amount;
            if (XP >= NextLevelXP)
            {
                LevelUp();
            }
        }

        void LevelUp()
        {
            Level++;
            Health += 10;  // Increase Health by 10 each level
            Attack += Level;  // Increase Attack by current Level
            Defense += Level;  // Increase Defense by current Level
            Speed += Level;  // Increase Speed by current Level
            NextLevelXP = Level * 100;  // Increase XP needed for next level
        }

        public void Equip(string slot, Item item)
        {
            Equipment[slot] = item;
            ApplyStatBoosts(item.StatBoosts);
        }

        private void ApplyStatBoosts(List<Stat> statBoosts)
        {
            foreach (var stat in statBoosts)
            {
                switch (stat.Name)
                {
                    case "Attack":
                        Attack += stat.Value;
                        break;
                    case "Defense":
                        Defense += stat.Value;
                        break;
                    case "Speed":
                        Speed += stat.Value;
                        break;
                    case "Health":
                        Health += stat.Value;
                        break;
                }
            }
        }
    }
}
