using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapModel.Model
{
    /// <summary>
    /// Basic quest system for tracking objectives and completion
    /// </summary>
    public class Quest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public bool IsActive { get; set; } = false;
        public List<QuestObjective> Objectives { get; set; } = new List<QuestObjective>();
        public Action<PC>? OnCompleted { get; set; }
        public int RewardGold { get; set; } = 0;
        public int RewardXP { get; set; } = 0;

        public Quest()
        {
            Objectives = new List<QuestObjective>();
        }

        public Quest(string name, string description)
        {
            Name = name;
            Description = description;
            Objectives = new List<QuestObjective>();
        }

        /// <summary>
        /// Check if all objectives are completed and complete the quest if so
        /// </summary>
        public bool CheckCompletion(PC player)
        {
            if (IsCompleted || !IsActive) return IsCompleted;

            bool allObjectivesComplete = Objectives.All(obj => obj.IsCompleted);
            
            if (allObjectivesComplete)
            {
                IsCompleted = true;
                OnCompleted?.Invoke(player);
                
                if (RewardGold > 0)
                {
                    player.Gold += RewardGold;
                    Console.WriteLine($"Quest completed! You earned {RewardGold} gold.");
                }
                
                if (RewardXP > 0)
                {
                    player.EarnXP(RewardXP);
                    Console.WriteLine($"Quest completed! You earned {RewardXP} XP.");
                }
            }
            
            return IsCompleted;
        }

        public void Start()
        {
            IsActive = true;
            Console.WriteLine($"Quest started: {Name}");
            Console.WriteLine($"Description: {Description}");
        }

        public override string ToString()
        {
            var status = IsCompleted ? "[COMPLETED]" : IsActive ? "[ACTIVE]" : "[INACTIVE]";
            return $"{status} {Name}: {Description}";
        }
    }

    /// <summary>
    /// Individual quest objective
    /// </summary>
    public class QuestObjective
    {
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public QuestObjectiveType Type { get; set; }
        public string TargetItemName { get; set; } = string.Empty;
        public int TargetCount { get; set; } = 1;
        public int CurrentCount { get; set; } = 0;

        public QuestObjective(string description, QuestObjectiveType type)
        {
            Description = description;
            Type = type;
        }

        /// <summary>
        /// Update progress on this objective
        /// </summary>
        public bool UpdateProgress(PC player, string? itemName = null)
        {
            switch (Type)
            {
                case QuestObjectiveType.CollectItem:
                    if (!string.IsNullOrEmpty(itemName) && itemName == TargetItemName)
                    {
                        CurrentCount = player.Inventory.Count(item => item.Name == TargetItemName);
                        IsCompleted = CurrentCount >= TargetCount;
                        if (IsCompleted)
                        {
                            Console.WriteLine($"Objective completed: {Description}");
                        }
                    }
                    break;
                case QuestObjectiveType.TalkToNPC:
                    if (!string.IsNullOrEmpty(itemName) && itemName == TargetItemName)
                    {
                        IsCompleted = true;
                        Console.WriteLine($"Objective completed: {Description}");
                    }
                    break;
            }
            
            return IsCompleted;
        }

        public override string ToString()
        {
            var status = IsCompleted ? "[✓]" : "[ ]";
            if (Type == QuestObjectiveType.CollectItem && TargetCount > 1)
            {
                return $"{status} {Description} ({CurrentCount}/{TargetCount})";
            }
            return $"{status} {Description}";
        }
    }

    public enum QuestObjectiveType
    {
        CollectItem,
        TalkToNPC,
        ReachLocation,
        DefeatEnemy
    }
}