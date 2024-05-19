using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapModel.Model
{
    public class Equipment : Item
    {
        public EquipmentTypeEnum EquipemntType { get; set; }

        public enum EquipmentTypeEnum
        {
            Weapon,
            Shield,
            Armor, 
            Helmet,
            Boots,
            Gloves,
            Ring,
            Amulet
        }

        public Equipment()
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
}
