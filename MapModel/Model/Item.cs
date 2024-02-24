using MapModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Model
{
    public class Item : MapObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Stat> StatBoosts { get; set; }
        public ItemTypeEnum ItemType { get; set; }
        public Action<PC> OnUse { get; set; }

        public Item()
        {
            ObjectType = ObjectTypeEnum.Item;
            StatBoosts = new List<Stat>();
        }

        public override string ToString()
        {
            var el = Environment.NewLine;
            return $"Item{el}X: {X}{el}Y: {Y}{el}{nameof(Name)}: {Name}{el}{nameof(Description)}: {Description}{el}" +
                $"{nameof(ItemType)}: {ItemType}";
        }
    }

    public enum ItemTypeEnum
    {
        Key,
        Weapon,
        Shield,
        Armor,
        Potion,
        Treasure,
    }
}
