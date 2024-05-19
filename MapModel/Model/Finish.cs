using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetCoreAudio;

namespace MapModel.Model
{
    public class Finish : MapObject
    {    
        public List<Item>  FullfillmentConditionList { get; set; }

        public Finish() 
        { 
            FullfillmentConditionList = new List<Item>();
            ObjectType = ObjectTypeEnum.Finish;
        }

        public bool CanAdvanceToNextLevel(PC pc)
        {
            if (pc.X == X & pc.Y == Y)
            {
                if (!FullfillmentConditionList.Any()) return true;
                
                if (FullfillmentConditionList.All(x=> pc.Inventory.Exists(y=>y.Name == x.Name))) 
                    return true;
            }
            return false;
        }

        public void FinishAchieved()
        {
            Player player = new Player();
            player.Play(@"Sounds\finish_sound.mp3");
        }

        public override string ToString()
        {
            var el = Environment.NewLine;
            var condition = FullfillmentConditionList.Any() ? FullfillmentConditionList.Aggregate("", (x, y) => x + y.Name + ", ") : "None";
            return $"Finish{el}X: {X}{el}Y: {Y}{el}{nameof(FullfillmentConditionList)}: {condition}";
        }
    }
}
