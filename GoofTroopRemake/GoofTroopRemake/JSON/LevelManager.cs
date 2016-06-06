using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.IO;

namespace GoofTroopRemake.JSON
{
    public class LevelManager
    {
        
        public IList<Level> levels { get; set; }

        public LevelManager() {
            string domain = AppDomain.CurrentDomain.BaseDirectory;
            string path = domain.Substring(0, domain.IndexOf("bin")) + @"JSON\";
            levels = new List<Level>();

            Level level1 = JsonConvert.DeserializeObject<Level>(
                File.ReadAllText(path + @"level1.json"));
            level1.SetRectangles();

            Level level2 = JsonConvert.DeserializeObject<Level>(
                File.ReadAllText(path + @"level2.json"));
            level2.SetRectangles();

            Level level3 = JsonConvert.DeserializeObject<Level>(
                File.ReadAllText(path + @"level3.json"));
            level3.SetRectangles();

            levels.Add(level1);
            levels.Add(level2);
            levels.Add(level3);
        }
    }
}
