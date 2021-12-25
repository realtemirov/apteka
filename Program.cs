using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace darslar
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "data.json";
            if(!File.Exists(path))
            {
                var fileStream = File.Create(path);
                fileStream.Close();
            }
            

            Dorixona dorixona = new Dorixona();
            Console.WriteLine("1 create");
            Console.WriteLine("2 read");
            Console.WriteLine("3 update");
            Console.WriteLine("4 delete");
            int n = Convert.ToInt32(Console.ReadLine());
            switch (n)
            {
                case 1:
                    {
                        Console.WriteLine("Create");

                        Console.Write("Nomi ");
                        string nomi = Console.ReadLine();
                        Console.Write("Davlati ");
                        string davlati = Console.ReadLine();
                        Console.Write("Narxi ");
                        string narxi = Console.ReadLine();
                        Console.Write("Soni ");
                        string count = Console.ReadLine();
                        
                        Dori dori = new Dori(nomi, davlati, narxi, count);
                        dorixona.Add(dori);

                    } break;

                case 2:
                    {
                        Console.WriteLine("Create");
                        var readdorilar = dorixona.ReadJson();
                        for (int i = 0; i < readdorilar.Count; i++)
                        {
                            Console.WriteLine($"{(i+1)}-{readdorilar[i]._nomi} {readdorilar[i]._narxi} {readdorilar[i]._soni} {readdorilar[i]._davlati}");
                        }
                    }
                    break;

                case 3:
                    {
                        Update();
                    }
                    break;

                case 4:
                    {
                        Delete();
                    }
                    break;

                default:
                    Console.WriteLine("No No");
                    break;
            }


            /*string[] arg = {""};
            Main(arg);*/
        }


        public static void Update()
        {
            Console.WriteLine("Update");
        }

        public static void Delete()
        {
            Console.WriteLine("Delete");
        }
    }

    
	

}