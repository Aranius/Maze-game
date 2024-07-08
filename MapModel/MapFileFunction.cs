using MapModel.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapModel
{
    public class MapFileFunction
    {
        static JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects
        };

        /// <summary>
        /// Vraci 2D bludisko z nacitaneho suboru v bool hodnotach. Pole obsahuje false na poziciach, kde je stena. 
        /// </summary>
        /// <param name="nazov_suboru"></param> Soubor se nacita zlist mapobjects.json.
        /// <returns></returns> Vraci 2D bludisko v bool hodnotach.
        public static bool[,] LoadMaze(string nazov_suboru)
        {
            TextReader subor = new StreamReader(nazov_suboru);

            var riadky = subor.ReadToEnd().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            bool[,] bludisko = new bool[riadky.Length, riadky[0].Length];

            for (int i = 0; i < riadky.Length; i++)
            {
                for (int j = 0; j < riadky[i].Length; j++)
                {
                    bludisko[i, j] = riadky[i][j] == '1';
                }
            }

            subor.Close();

            return bludisko;
        }
        /// <summary>
        /// Nacita objekty z mapobjects.json a vraci je jako List<MapObject>.
        /// </summary>
        /// <param name="nazov_suboru"></param>soubor se nacita z mapobjects.json
        /// <returns></returns> vrati List<MapObject>
        /// <exception cref="Exception"></exception> Pokud se nepodari nacist soubor.
        public static List<MapObject> LoadMapObjects(string nazov_suboru)
        {
            var obsah = File.ReadAllText(nazov_suboru);

            var result = JsonConvert.DeserializeObject<List<MapObject>>(obsah, serializerSettings);

            if (result != null)
            {
                return result;
            }
            throw new Exception("Chyba pri nacitani suboru");
        }
        /// <summary>
        /// Ulozi objekty do souboru mapobjects.json. => soubor se ulozi  do List<MapObject> nazev suboru je typu string. funkce je staticka. nevraci zadnou hodnotu. do souboru se ulozi inventar, pozice PC na mape, vybava PC a aktualne vybavene predmety (stav predmetu).
        /// </summary>
        /// <param name="mapObjects"></param> Uklada aktualni stav mapobjektu (pasti, dvere, predmety, NPC, PC, Finish) do souboru mapobjects.json.
        /// <param name="nazov_suboru"></param> Nazev souboru, do ktereho se ukladaji objekty.
        public static void SaveMapObjects(List<MapObject> mapObjects, string nazov_suboru)
        {
            var obsah = JsonConvert.SerializeObject(mapObjects, serializerSettings);

            File.WriteAllText(nazov_suboru, obsah);
        }
        /// <summary>
        /// Ulozi bludisko do suboru. Bludisko je 2D pole bool hodnot, kde false znamena stenu. Funkcia je staticka a nevracia ziadnu hodnotu.
        /// </summary>
        /// <param name="bludisko"></param> SaveMaze uklada aktualni stav bludiska do souboru.
        /// <param name="nazov"></param> Nazov suboru, do ktoreho sa ma ulozit bludisko.
        public static void SaveMaze(bool[,] bludisko, string nazov)
        {
            TextWriter subor = new StreamWriter(nazov);

            for (int i = 0; i < bludisko.GetLength(0); i++)
            {
                for (int j = 0; j < bludisko.GetLength(1); j++)
                {
                    subor.Write(bludisko[i, j] ? "1" : "0");
                }
                subor.WriteLine();
            }

            subor.Close();
        }
    }
}
