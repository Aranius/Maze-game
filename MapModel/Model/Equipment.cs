using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapModel.Model
{/// <summary>
/// výstroj a výzbroj vybranná(equipnutá) pc zvyšuje určité parametry pc   
/// </summary>
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
        /// <summary>
        /// konstruktor výstroje    a výzbroje  
        /// </summary>
        public Equipment()
        {
            ObjectType = ObjectTypeEnum.Item;
            StatBoosts = new List<Stat>();
        }
        /// <summary>
        /// vypíše všechny hodnoty výstroje a výzbroje
        /// </summary>
        /// <returns></returns> vrací jmeno a popis predmetu ve hře a jeho vlastnosti
        public override string ToString()
        {
            var el = Environment.NewLine;
            return $"Item{el}X: {X}{el}Y: {Y}{el}{nameof(Name)}: {Name}{el}{nameof(Description)}: {Description}{el}" +
                $"{nameof(ItemType)}: {ItemType}";
        }
    }
}
