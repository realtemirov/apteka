using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.Json;
using System.IO;

namespace Dorixona
{
   
    public class Dorixona
    {
        #region Properties
        public string _namerOfDorixona { get; set; } 
        public string _ownerOfDorixona { get; set; }
        public int _bujjetOfDorixona { get; set; }
        public int _yesterdayMoney { get; set; }
        string pathOfOwner = "ownerOfDorixona.txt";
        string pathOfMoney = "moneyOfDorixona.txt";
        
        string pathOfYesMoney = "yesterdayMoney.txt";
        string path = "data.json.";
        List<Dori> _dorilar = new List<Dori>();
        string pathOfDorixona = "datasOfDorixona.json";
        #endregion

        public void Set()
        {
            if (File.Exists(pathOfOwner))
            {
                string[] data = File.ReadAllText(pathOfOwner).Split();
                _namerOfDorixona = data[0];
                _ownerOfDorixona = data[2];
            }
            if (File.Exists(pathOfMoney))
            {
                _bujjetOfDorixona = Convert.ToInt32(File.ReadAllText(pathOfMoney));
            }
            if (File.Exists(pathOfYesMoney))
            {
                 _yesterdayMoney = Convert.ToInt32(File.ReadAllText(pathOfMoney));
            }
        }

        public Dorixona()
        {
            
        }
        #region PropMethod
        public void ChangeMoney(int sum)
        {
            _bujjetOfDorixona = _bujjetOfDorixona - sum;
            File.WriteAllText(pathOfMoney, _bujjetOfDorixona.ToString());
        }

        public void AddMoney(int sum)
        {
            _bujjetOfDorixona = _bujjetOfDorixona + sum;
            File.WriteAllText(pathOfMoney, _bujjetOfDorixona.ToString());
        }


        public string GetName()
        {
            return _namerOfDorixona;
        }
        public string GetOwner()
        {
            return _ownerOfDorixona;
        }
        #endregion

        public int Add(Dori dori)
        {

            int result=1;
            List<Dori> wrtDorilar = new List<Dori>();
            if (!File.Exists(path))
            {
                var fileStream = File.Create(path);
                fileStream.Close();
            }
            if (File.ReadAllText(path) != null && File.ReadAllText(path) != "")
            {
                var data2 = File.ReadAllText(path);
                wrtDorilar = JsonConvert.DeserializeObject<List<Dori>>(data2);
                for (int i = 0; i < wrtDorilar.Count; i++)
                {
                    if (wrtDorilar[i]._ID == dori._ID)
                    {
                        result = -1;
                        wrtDorilar[i]._soni = wrtDorilar[i]._soni + dori._soni;
                        wrtDorilar[i]._priceOfFirst = dori._priceOfFirst;
                        wrtDorilar[i]._sold = dori._sold;
                        break;
                    }

                }
                if (_bujjetOfDorixona >= dori._priceOfFirst * dori._soni) { }
                else return 0;

                if (result == 1)
                {
                    wrtDorilar.Add(dori);
                    ChangeMoney(dori._soni * dori._priceOfFirst);
                }
            }
            else
            {
                if (_bujjetOfDorixona >= dori._priceOfFirst * dori._soni) { }
                else return 0;
                wrtDorilar.Add(dori);
                ChangeMoney(dori._soni * dori._priceOfFirst);
            }
            Write(wrtDorilar);
            return result;
        }

        public List<Dori> ReadJson()
        {
            List<Dori> readDorilar = new List<Dori>();
            if(File.Exists(path))
            {
                var data = File.ReadAllText(path);
                readDorilar = JsonConvert.DeserializeObject<List<Dori>>(data);
            }
            return readDorilar;
        }

        public void Update(Dori oldDori, Dori newDori)
        {
            List<Dori> readDorilar = new List<Dori>();
            if(File.Exists(path))
            {
                if (File.ReadAllText(path) != null)
                {
                    var data2 = File.ReadAllText(path);
                    readDorilar = JsonConvert.DeserializeObject<List<Dori>>(data2);
                    for (int i = 0; i < readDorilar.Count; i++)
                    {
                        if (readDorilar[i]._ID == oldDori._ID)
                        {
                            if (oldDori == newDori)
                            {
                                AddMoney((readDorilar[i]._soni-newDori._soni) *newDori._sold);
                                readDorilar[i] = newDori;
                            }
                            else
                            {
                                readDorilar[i]._sold = newDori._sold;
                            }
                            break;
                        }
                    }
                }
                Write(readDorilar);
            }
        }
        
        public void Delete(Dori deleteDori)
        {
            List<Dori> readDorilar = new List<Dori>();
            if (File.Exists(path))
            {
                if (File.ReadAllText(path) != null)
                {
                    var data2 = File.ReadAllText(path);
                    readDorilar = JsonConvert.DeserializeObject<List<Dori>>(data2);
                    for (int i = 0; i < readDorilar.Count; i++)
                    {
                        if (readDorilar[i]._ID == deleteDori._ID)
                        {
                            readDorilar[i] = null;
                        }
                    }
                }
                Write(readDorilar);
            }
        }

        public void Write(List<Dori> write)
        {
            List<Dori> writeCheck = new List<Dori>();
            foreach (var item in write)
            {
                if (item is not null)
                    if (item._soni !=0)
                        writeCheck.Add(item);
                
            }
            var data = JsonConvert.SerializeObject(writeCheck.ToArray());
            File.WriteAllText(path, data);
        }

        public void WriteYesterday()
        {
            File.WriteAllText(pathOfYesMoney, _yesterdayMoney.ToString());
            _yesterdayMoney = Convert.ToInt32(File.ReadAllText(pathOfMoney));

        }
        public void BuyDori(Dori dori,int count)
        {
            dori._soni = dori._soni - count;
            Update(dori, dori);
            Console.WriteLine("\n Sotildi ! \n");
            
        }
    }
}
