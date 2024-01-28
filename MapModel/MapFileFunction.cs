using Maze.Model;
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

        public static List<MapObject> LoadMapObjects(string nazov_suboru)
        {
            var obsah = File.ReadAllText(nazov_suboru);

            return JsonConvert.DeserializeObject<List<MapObject>>(obsah, serializerSettings);
        }

        public static void SaveMapObjects(List<MapObject> mapObjects, string nazov_suboru)
        {
            var obsah = JsonConvert.SerializeObject(mapObjects, serializerSettings);

            File.WriteAllText(nazov_suboru, obsah);
        }

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
