using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapModel.Model
{
    /// <summary>
    /// Kreatury jsou objekty, které se mohou pohybovat po mapě a útočit na hráče. Základní vlastnosti jsou zdraví, útok, obrana a rychlost. 
    /// </summary>
    public class Creature : MapObject
    {
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Speed { get; set; }

        /// <summary>
        /// Hodnoty utoku, obrany, rychlosti a zdravi jsou inicializovany na zaklade vlozenych parametru. 
        /// </summary> Boj končí ve chvíli, kdy jeden PC nebo creature zemře.
        /// <param name="attacker"></param> sohrn hodnot utoku proti PC
        /// <param name="defender"></param> Vyhodnocuje obranu proti utoku PC a kolik zdravi mu zbyva a zda je mrtva
        public void ResolveCombat(Creature attacker, Creature defender)
        {
            Creature first = attacker.Speed > defender.Speed ? attacker : defender;
            Creature second = attacker.Speed > defender.Speed ? defender : attacker;

            Fight(first, second);
            if (second.Health > 0)
            {
                Fight(second, first);
            }
        }

        /// <summary> Boj, kde se odečte útok od obrany a odečte se zdraví. </summary>
        public void Fight(Creature attacker, Creature defender)
        {
            int damage = Math.Max(1, attacker.Attack - defender.Defense);
            defender.Health -= damage;

            // Display the result, check if defender is defeated, etc.
        }

    }
}
