using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Model
{
    public class Creature : MapObject
    {
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Speed { get; set; }

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

        public void Fight(Creature attacker, Creature defender)
        {
            int damage = Math.Max(1, attacker.Attack - defender.Defense);
            defender.Health -= damage;

            // Display the result, check if defender is defeated, etc.
        }

    }
}
