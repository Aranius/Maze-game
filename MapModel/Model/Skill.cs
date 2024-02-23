using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapModel.Model
{
    public class Skill
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public int MaxLevel { get; set; }
        public int Usage { get; set; } // New property
        public Dictionary<Skill, int> Requirements { get; set; }

        public Skill(string name, string description, int maxLevel)
        {
            Name = name;
            Description = description;
            Level = 0;
            MaxLevel = maxLevel;
            Usage = 0; // Initialize usage at 0
            Requirements = new Dictionary<Skill, int>();
        }

        public bool CanLevelUp()
        {
            foreach (var requirement in Requirements)
            {
                if (requirement.Key.Level < requirement.Value)
                {
                    return false;
                }
            }
            return Level < MaxLevel && Usage >= Level * 10; // Check usage threshold
        }

        public void LevelUp()
        {
            if (CanLevelUp())
            {
                Level++;
                Usage = 0; // Reset usage after leveling up
            }
        }

        public void Use()
        {
            Usage++; // Increment usage each time the skill is used
        }
    }

}
