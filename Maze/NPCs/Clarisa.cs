using MapModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.NPCs
{
    internal class Clarisa : NPC
    {
        public Clarisa()
        {
            Health = 100;
            Attack = 10;
            Defense = 5;
            Speed = 5;

            CurrentDialogue = new Dialogue
            {
                Text = "Hello, I'm Clarisa. I'm the healer in this town. I can heal you if you bring me the materials.",
                Responses = new List<Dialogue>
                {
                    new Dialogue
                    {
                       Text = "Would you like to buy a health potion for 10 gold?",
                        OnChosen = player =>
                        {
                            if (player.Gold >= 10)
                            {
                                player.Gold -= 10;
                                var healthPotion = new Item
                                {
                                    Name = "Health Potion",
                                    Description = "Heals 20 health.",
                                    OnUse = (p) =>
                                    {
                                        p.Health += 20;
                                        Console.WriteLine("You drink the health potion and heal 20 health.");
                                    }
                                };
                                player.Inventory.Add(healthPotion);
                                Console.WriteLine("You bought a health potion!");
                            }
                            else
                            {
                                Console.WriteLine("You don't have enough gold.");
                            }
                        }
                    },
                    new Dialogue
                    {
                        Text = "Goodbye.",
                        OnChosen = (player) =>
                        {
                            Console.WriteLine("Goodbye.");
                        }
                    }
                }
            };
        }

    }
}
