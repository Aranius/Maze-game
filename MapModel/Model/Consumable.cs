using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapModel.Model
{/// <summary>
/// Consumable class dedi (inheritens) vlastnosti predmetu. 
/// </summary>
    public class Consumable : Item
    {/// <summary>
    /// vlastnoti Consumable class zvyšuji hodnoty hráčových vlastnosti jednorázově nebo dočasně
    /// </summary>
        public ConsumableTypeEnum ConsumableType { get; set; }
        /// <summary>
        /// typy lektvarů ve hře davají + nebo - k postavě
        /// </summary>
        public enum ConsumableTypeEnum
        {
            HealthPotion,
            SpeedPotion,
            AttackPotion,
            DefensePotion
        }
        /// <summary>
        /// konstruktor Consumable class dedí vlastnosti od Item class jako zakladni vlastnosti predmetu
        /// </summary>
        public Consumable()
        {
            ObjectType = ObjectTypeEnum.Item;
            StatBoosts = new List<Stat>();
        }
        /// <summary>
        /// vypisuje vlastnosti Consumable class
        /// </summary> 
        /// <returns></returns> jmeno a popis predmetu
        public override string ToString()
        {
            var el = Environment.NewLine;
            return $"Item{el}X: {X}{el}Y: {Y}{el}{nameof(Name)}: {Name}{el}{nameof(Description)}: {Description}{el}" +
                $"{nameof(ItemType)}: {ItemType}";
        }
    }
}
