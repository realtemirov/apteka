using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using Newtonsoft.Json;

namespace darslar
{
    public class Dorixona
    {
        List<Dori> wrtDorilar = new List<Dori>();
        List<Dori> readDorilar = new List<Dori>();

        string path = "data.json.";

        public void Add(Dori dori)
        {
            wrtDorilar.Add(dori);
            WriteJson(wrtDorilar);
        }

        public void WriteJson(List<Dori> wrtDorilar)
        {
            var data = JsonConvert.SerializeObject(wrtDorilar.ToArray());
            File.AppendAllText(path,data);
        }

        public List<Dori> ReadJson()
        {
            var data = File.ReadAllText(path);
            readDorilar = JsonConvert.DeserializeObject<List<Dori>>(data);
            return readDorilar;
        }

    }
}
