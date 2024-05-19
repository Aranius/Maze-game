using MapModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.NPCs
{
    internal class Owen : NPC
    {
        public Owen()
        {
            Health = 100;
            Attack = 10;
            Defense = 5;
            Speed = 5;

            CurrentDialogue = new Dialogue
            {
                Text = "Hello, I'm Owen. I'm the blacksmith in this town. I can make you a weapon if you bring me the materials.",
                Responses = new List<Dialogue>
                {
                    new Dialogue
                    {
                        Text = "What materials do you need?",
                        Responses = new List<Dialogue>
                        {
                            new Dialogue
                            {
                                Text = "I need 5 iron ore and 2 wood.",
                                OnChosen = (player) =>
                                {
                                    Console.WriteLine("I need 5 iron ore and 2 wood.");
                                }
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
