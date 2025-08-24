using NetCoreAudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapModel.Model
{
    public class NPC : Creature
    {
        public Dialogue CurrentDialogue { get; set; }

        public List<Item> Inventory { get; set; }

        public NPC()
        {
            Inventory = new List<Item>();
        }

        public void TalkToPlayer(PC player)
        {
            if (CurrentDialogue == null)
            {
                Console.WriteLine("This NPC has nothing to say.");
                return;
            }

            Console.WriteLine(CurrentDialogue.Text);

            if (CurrentDialogue.Responses?.Count > 0)
            {
                Console.WriteLine("\nChoose your response:");
                for (int i = 0; i < CurrentDialogue.Responses.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {CurrentDialogue.Responses[i].Text}");
                }

                Console.Write("\nYour choice (number): ");
                string? input = Console.ReadLine();
                
                if (int.TryParse(input, out int playerChoice))
                {
                    playerChoice--; // Convert to 0-based index
                    
                    if (playerChoice >= 0 && playerChoice < CurrentDialogue.Responses.Count)
                    {
                        var selectedResponse = CurrentDialogue.Responses[playerChoice];
                        selectedResponse.OnChosen?.Invoke(player);
                        
                        // If the selected response has further responses, continue the dialogue
                        if (selectedResponse.Responses?.Count > 0)
                        {
                            CurrentDialogue = selectedResponse;
                            Console.WriteLine("\nPress any key to continue the conversation...");
                            Console.ReadKey();
                            TalkToPlayer(player); // Recursive call for multi-level dialogue
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice.");
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid number.");
                }
            }
            else
            {
                Console.WriteLine("\n[The NPC nods and returns to their work]");
            }
        }
    }
}
