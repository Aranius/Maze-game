using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetCoreAudio;

namespace MapModel.Model
{
    /// <summary>
    ///  Funkce, ktera zjisti, zda je mozne prejit na dalsi level. Musi byt splneny vsechny podminky. 
    /// </summary>
    public class Finish : MapObject
    {
        /// <summary>
        ///  Vlastnosti funkce, ktera zjisti, zda je mozne prejit na dalsi level. Musi byt splneny vsechny podminky. 
        /// </summary>
        public List<Item>  FullfillmentConditionList { get; set; }
        /// <summary>
        /// Zjistuje, zda PC ma v inventari vsechny predmety, dulezite pro splneni levelu. 
        /// </summary>
        public Finish() 
        { 
            FullfillmentConditionList = new List<Item>();
            ObjectType = ObjectTypeEnum.Finish;
        }
        /// <summary>
        /// Zjisti, zda je PC na spravnem miste na mape pro postup do dalsiho levelu.
        /// </summary>
        /// <param name="pc"></param> PC zda je na pozici Finish
        /// <returns></returns> Vraci true, pokud je PC na pozici Finish
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
/// <summary>
/// Zvuk, ktery se prehraje, pokud je level splnen.
/// </summary>
        public void FinishAchieved()
        {
            Player player = new Player();
            player.Play(@"Sounds/finish_sound.mp3");
        }
        /// <summary>
        /// Vypise kam dojit a jake predmety jsou podminkou pro prechod na dalsi level.
        /// </summary>
        /// <returns></returns> Vypise text s informacemi co musis splnit,  Finish
        public override string ToString()
        {
            var el = Environment.NewLine;
            var condition = FullfillmentConditionList.Any() ? FullfillmentConditionList.Aggregate("", (x, y) => x + y.Name + ", ") : "None";
            return $"Finish{el}X: {X}{el}Y: {Y}{el}{nameof(FullfillmentConditionList)}: {condition}";
        }
    }
}
