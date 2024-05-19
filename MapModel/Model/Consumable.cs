using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapModel.Model
{
    public class Consumable : Item
    {
        public ConsumableTypeEnum ConsumableType { get; set; }

        public enum ConsumableTypeEnum
        {
            HealthPotion,
            SpeedPotion,
            AttackPotion,
            DefensePotion
        }

        public Consumable()
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
