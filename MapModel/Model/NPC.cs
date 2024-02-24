using MapModel.Model;
using NetCoreAudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Model
{
    public class NPC : Creature
    {
        public Dialogue CurrentDialogue { get; set; }

        public void TalkToPlayer(Player player)
        {
            Console.WriteLine(CurrentDialogue.Text);

            for (int i = 0; i < CurrentDialogue.Responses.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {CurrentDialogue.Responses[i].Text}");
            }

            int playerChoice = Convert.ToInt32(Console.ReadLine()) - 1;

            if (playerChoice >= 0 && playerChoice < CurrentDialogue.Responses.Count)
            {
                CurrentDialogue = CurrentDialogue.Responses[playerChoice];
                CurrentDialogue.OnChosen?.Invoke(player);
            }
        }
    }
}
