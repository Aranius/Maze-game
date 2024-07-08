using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapModel.Model
{/// <summary>
/// objekt dooor s vlastnostami zamknute nebo oevřené true/false, nemusí být vždy zamčené.
/// </summary>
    public class Door : MapObject
    {/// <summary>
    /// 
    /// </summary>
        public bool Locked { get; set; }
        public bool Closed { get; set; } = true;
        /// <summary>
        /// prostě dvě vlastnosti x a y
        /// </summary>
        /// <param name="x"></param>poloha dveří v bludišti
        /// <param name="y"></param>poloha dveří v bludišti
        /// <param name="locked"></param> pokud jsou dveře zamčené (false) musí se odmknout klíčem změn na true
        public Door(int x, int y, bool locked = false)
        {
            Locked = locked;
            X = x;
            Y = y;
            ObjectType = ObjectTypeEnum.Door;
        }
        /// <summary>
        /// metoda pro odemčení dveří
        /// </summary>
        /// <param name="pc"></param>pc musí v bludišti najít klíč a mít ho v inventáři
        /// <returns></returns> vrátí zprávu o úspěšném odemčení dveří nebo že jsou zamčené
        public string Unlock(PC pc)
        {
            var najdeny_kluc = pc.Inventory.FirstOrDefault(x => x.ItemType == ItemTypeEnum.Key);
            if (najdeny_kluc != null)
            {
                Locked = false;
                pc.Inventory.Remove(najdeny_kluc);
                return "Odomkol si uspesne dvere";
            }
            else
            {
                return "Nemas ziadny kluc!";
            }
        }
        /// <summary>
        /// konstruktor pro otevření dveří
        /// </summary>
        /// <returns></returns> zjištuje zdali byly dveře otevřeny či ne
        public string Open()
        {
            if (!Locked)
            {
                Closed = false;
                return "Otvoril si dvere";
            }
            else
            {
                return "Dvere su zamknute";
            }
        }
        /// <summary>
        /// vypiše jestli jsou dveře zamčené či nikoliv
        /// </summary>
        /// <returns></returns> vrací polohu a stav dveří(zamčené či nikoliv
        public override string ToString()
        {
            var el = Environment.NewLine;
            return $"Dvere{el}X: {X}{el}Y: {Y}{el}{nameof(Locked)}: {Locked}{el}{nameof(Closed)}: {Closed}";
        }
    }
}
