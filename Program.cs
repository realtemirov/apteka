using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using System.IO;


namespace Dorixona
{
    class Program
    {
        

        static void Main(string[] args)
        {

            

            #region Start

            string pathOfOwner = "ownerOfDorixona.txt";
            string pathOfMoney = "moneyOfDorixona.txt";
            string pathOfYesMoney = "yesterdayMoney.txt";
            if (!File.Exists(pathOfOwner) || !File.Exists(pathOfMoney) || !File.Exists(pathOfYesMoney))
            {

                var fileStreamOwn = File.Create(pathOfOwner);
                fileStreamOwn.Close();
                var fileStreamMoney = File.Create(pathOfMoney);
                fileStreamMoney.Close();
                var fileStreamYesMoney = File.Create(pathOfYesMoney);
                fileStreamYesMoney.Close();
                

                
                Console.WriteLine("Assalomu alaykum, yangi dorixona ochganingiz bilan tabriklaymiz !");
                Console.WriteLine("Noqulayliklarni keltirib chiqarmaslik uchun o'z ma'lumotlaringizni kiriting: ");

                Console.Write("Dorixona nomi: ");
                string name = Console.ReadLine();
                Console.Write("Dorixona egasi: ");
                string person = Console.ReadLine();

                Console.Write("Qancha pul mablag'i bilan boshlayabsiz ? ");
                int money = Select();
                using (StreamWriter wrt = new StreamWriter(pathOfYesMoney))
                {
                    wrt.Write("0");
                }
                using (StreamWriter wrt = new StreamWriter(pathOfOwner))
                {
                    wrt.WriteLine(name);
                    wrt.WriteLine(person);
                }

                using (StreamWriter wrt = new StreamWriter(pathOfMoney))
                {
                    wrt.WriteLine(money);
                }

                /*
                    string pathOfDorixona = "datasOfDorixona.json";
                    var fileStreamOwn = File.Create(pathOfDorixona);
                    fileStream.Close();
                
                    List<Dorixona> dorixona1 = new List<Dori>();
                
                    dorixona1.Add(dorixona);
                    var data = JsonSerializer.Serialize<List<Dorixona>>(dorixona1);
                    File.WriteAllText(pathOfDorixona, data);
                */
            }

            #endregion
            Dorixona dorixona = new Dorixona();
            dorixona.Set();
            int n = 0;
        menu: n = Menu();
            switch (n)
            {
                case 1:
                    {
                        int m = 0;
                    toDoriCRUD: m = DoriCRUD();

                        switch (m)
                        {
                            case 1:
                                {
                                    int res = dorixona.Add(AddDori());
                                    if (res == 1)
                                        Console.WriteLine("\nDori bazaga qo'shildi !\n");
                                    else if (res == -1) Console.WriteLine("\nDori bazada yangilandi !\n");
                                    else if (res == 0)
                                        Console.WriteLine("Mablag'ingiz yetmaydi !");
                                    goto toDoriCRUD;
                                }
                                break;
                            case 2:
                                {
                                    View(dorixona);
                                    goto toDoriCRUD;
                                }
                                break;

                            case 3:
                                {
                                    Update(dorixona);
                                    goto toDoriCRUD;
                                }
                                break;

                            case 4:
                                {
                                    Delete(dorixona);
                                    goto toDoriCRUD;
                                }
                                break;

                            case 5:
                                {
                                    goto menu;
                                }
                                break;

                            default:
                                {
                                    Console.WriteLine("Noto'g'ri bo'limni tanladingiz !");
                                    goto toDoriCRUD;
                                }
                                break;
                        }
                    }
                    break;

                case 2:
                    {
                        Search(dorixona);
                        goto menu;
                    }
                    break;

                case 3:
                    {
                        
                        if(BuySelect(dorixona)) { }
                        goto menu;
                    }
                    break;
                case 4:
                    {
                        TodayMoney(dorixona);
                        goto menu;

                    }
                    break;

                case 5:
                    {
                        View(dorixona);
                        goto menu;
                        //todo
                    }
                    break;

                case 6:
                    {
                        Bujjet(dorixona);
                        goto menu;
                    }
                    break;

                case 7:
                    {
                        Console.WriteLine();
                        Console.WriteLine("****************Dorixona****************");
                        Console.WriteLine("*                                      *");
                        Console.WriteLine("*       Nomi: " + dorixona.GetName());
                        Console.WriteLine("*       Egasi: " + dorixona.GetOwner());
                        Console.WriteLine("*                                      *");
                        Console.WriteLine("****************************************");
                        Console.WriteLine();
                        goto menu;
                    }
                    break;

                case 8:
                    {
                        return;
                    }
                    break;

                case 9:
                    {
                        Console.WriteLine("Ishonchingiz komilmi?\n[1]-Yo'q\t\t[99]-Ha");
                        int res = Select();
                        if (res == 99) Reset();
                        else goto menu;
                    }
                    break;

                default:
                    {
                        Console.WriteLine("Noto'g'ri bo'limni tanladingiz !");
                        goto menu;

                    }
                    break;
            }

        }

        public static int Menu()
        {
            Console.WriteLine();
            Console.WriteLine("****************Dorixona****************");
            Console.WriteLine("*                                      *");
            Console.WriteLine("*    [1] - Dorilar(CRUD)               *");
            Console.WriteLine("*    [2] - Qidirish                    *");
            Console.WriteLine("*    [3] - Dori sotish                 *");
            Console.WriteLine("*    [4] - Bugungi savdo               *");
            Console.WriteLine("*    [5] - Omborxona                   *");
            Console.WriteLine("*    [6] - Bujjet                      *");
            Console.WriteLine("*    [7] - Haqida                      *");
            Console.WriteLine("*    [8] - Chiqish                     *");
            Console.WriteLine("*    [9] - Reset                       *");
            Console.WriteLine("*                                      *");
            Console.WriteLine("****************************************");
            Console.WriteLine();
            Console.Write("Bo'limlardan birini tanlang: ");

            return Select();
        }

        public static int Select()
        {
            int select = 0;

        input: while (select == 0)
            {

                try
                {
                    select = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Noto'g'ri format !");
                    goto input;
                }
            }
            return select;
        }

        public static Dori AddDori()
        {
        addDori:
            Console.Write("Nomi: ");
            string nameOfNewDori = Console.ReadLine();
            if (nameOfNewDori.Length < 15)
            {
                while (nameOfNewDori.Length != 15)
                {
                    nameOfNewDori = nameOfNewDori + " ";
                }
            }
            nameOfNewDori = nameOfNewDori.ToUpper();
            Console.Write("Qaysi davlayda ishlab chiqarilgan: ");
            string countryOfNewDori = Console.ReadLine();
            countryOfNewDori = countryOfNewDori.ToUpper();
            Console.Write("Sotib olish narxi: ");
            int priceOfNewDori = Select();

            Console.Write("Sotish narxi: ");
            int priceOfSold = Select();

            Console.Write("Dorilar soni: ");
            int countOfNewDori = Select();

            Dori dori = new Dori(nameOfNewDori, countryOfNewDori, priceOfNewDori, priceOfSold, countOfNewDori);
            if (AddAI(dori)) return dori;
            else
            {
                Console.WriteLine("Dorini qaytadan kiriting: ");
                dori = null;
                goto addDori;
            }

            return dori;
        }
        public static int DoriCRUD()
        {
            Console.WriteLine();
            Console.WriteLine("****************Dorilar(CRUD)****************");
            Console.WriteLine("*                                           *");
            Console.WriteLine("*    [1] - Bazaga dori qo'shish             *");
            Console.WriteLine("*    [2] - Dorilar ro'yxati                 *");
            Console.WriteLine("*    [3] - Dori ma'lumotlarni o'zgartirish  *");
            Console.WriteLine("*    [4] - Dorini o'chirish                 *");
            Console.WriteLine("*    [5] - Menu                             *");
            Console.WriteLine("*                                           *");
            Console.WriteLine("*********************************************");
            Console.WriteLine();
            Console.Write("Bo'limlardan birini tanlang: ");
            int forDori = 0;
            forDori = Select();
            return forDori;
        }
        public static void View(Dorixona dorixona)
        {
            
            var readDorilar = dorixona.ReadJson();
            Console.WriteLine();
            Console.WriteLine("*********************Dorilar ro'yxati*********************");
            Console.WriteLine("*                                                        *");

            if (readDorilar == null || readDorilar.Count == 0)
                Console.WriteLine("*               Dorilar hali mavjud emas                 *");
            else if (readDorilar.Count > 0 && readDorilar != null)
            {
                Console.WriteLine("* N: Nomi             soni Input Output  Davlati   Kodi  *");
                for (int i = 0; i < readDorilar.Count; i++)
                {
                    if (readDorilar[i] != null)
                    {
                        Console.WriteLine($"* {(i + 1)}  {readDorilar[i]._nameOfDori} " +
                    $" {readDorilar[i]._soni}   {readDorilar[i]._priceOfFirst}    {readDorilar[i]._sold} " +
                    $"   {readDorilar[i]._countryOfDori}      {readDorilar[i]._ID}  ");
                    }
                }
            }

            Console.WriteLine("*                                                        *");
            Console.WriteLine("**********************************************************");
            Console.WriteLine();
        }

        public static void ViewOneDori(List<Dori> dorilarnomi, List<Dori> dorilarboshi)
        {
            Console.WriteLine("* N: Nomi             soni Input Output  Davlati   Kodi  *");
            if (dorilarnomi.Count != 0)
            {
                for (int i = 0; i < dorilarnomi.Count; i++)
                {
                    Console.WriteLine($"* {(i + 1)}  {dorilarnomi[i]._nameOfDori} " +
                $" {dorilarnomi[i]._soni}   {dorilarnomi[i]._priceOfFirst}    {dorilarnomi[i]._sold} " +
                $"   {dorilarnomi[i]._countryOfDori}      {dorilarnomi[i]._ID}  ");
                }
            }
            if (dorilarnomi.Count == 0 && dorilarboshi.Count != 0)
            {
                Console.WriteLine("\nAytgan doringiz yo'q o'xshashlari bor !\n");
                for (int i = 0; i < dorilarboshi.Count; i++)
                {
                    Console.WriteLine($"* {(i + 1)}  {dorilarboshi[i]._nameOfDori} " +
                $" {dorilarboshi[i]._soni}   {dorilarboshi[i]._priceOfFirst}    {dorilarboshi[i]._sold} " +
                $"   {dorilarboshi[i]._countryOfDori}      {dorilarboshi[i]._ID}  ");
                }
            }
            if (dorilarnomi.Count == 0 && dorilarboshi.Count == 0)
            {
                Console.WriteLine("*               Bunday dori yo'q                *");
            }
            Console.WriteLine("*                                                        *");
            Console.WriteLine("**********************************************************");
            Console.WriteLine();
        }

        public static void Update(Dorixona dorixona)
        {
           
            var readdorilar = dorixona.ReadJson();
            if (readdorilar.Count != 0)
            {
                View(dorixona);
                Console.WriteLine("Siz maxsulotning sotilish narxini o'zgartirishingiz mumkin faqat !");
                Console.WriteLine("Dori nomi va davlatini to'g'ri kiriting !");
                Console.Write("O'zgaradigan dori raqamini tanlang: ");
                int numberChangeDori = Select();
                for (int i = 0; i < readdorilar.Count; i++)
                {
                    if (numberChangeDori == (i + 1))
                    {
                        dorixona.Update(readdorilar[i], AddDori());
                        break;
                    }
                }
            }
            else Console.WriteLine("Hali dorilar mavjud emas");
        }
        public static void Delete(Dorixona dorixona)
        {
            var readdorilar = dorixona.ReadJson();
            if (readdorilar.Count != 0)
            {
                View(dorixona);
                Console.Write("O'chirmoqchi bo'lgan dori raqamini kiriting: ");
                int numberDeleteDori = Select();
                for (int i = 0; i < readdorilar.Count; i++)
                {
                    if (numberDeleteDori == (i + 1))
                    {
                        dorixona.Delete(readdorilar[i]);
                        break;
                    }
                }
            }
            else Console.WriteLine("Hali dorilar mavjud emas");
        }
        public static void Reset()
        {
            if (File.Exists("moneyOfDorixona.txt"))
                File.Delete("moneyOfDorixona.txt");
            if (File.Exists("ownerOfDorixona.txt"))
                File.Delete("ownerOfDorixona.txt");
            if (File.Exists("data.json"))
                File.Delete("data.json");
            Console.WriteLine("\n\nSalomat bo'ling\n\n");
        }
        public static void Search(Dorixona dorixona)
        {
            Console.Write("Dori nomini kiriting: ");
            string searchDori = Console.ReadLine();
            searchDori = searchDori.ToUpper();
            List<Dori> searchDorilar1 = new List<Dori>();
            List<Dori> searchDorilar2 = new List<Dori>();

            var readdorilar = dorixona.ReadJson();
            if (readdorilar != null && readdorilar.Count != 0)
            {
                for (int i = 0; i < readdorilar.Count; i++)
                {
                    if (readdorilar[i]._nameOfDori == searchDori)
                    {
                        searchDorilar1.Add(readdorilar[i]);
                    }
                    if (readdorilar[i]._nameOfDori.StartsWith(searchDori[0]))
                    {
                        searchDorilar2.Add(readdorilar[i]);
                    }
                }
                ViewOneDori(searchDorilar1, searchDorilar2);
                Console.WriteLine("[1]Xarid qilish\n[2]-Dorilar (CRUD)\n[3]-Menu");
                int n = Select();
                if (n == 1) BuySelect(dorixona);
                if (n == 2) DoriCRUD();

            }
            else Console.WriteLine("Hali dorilar mavjud emas");

        }
        public static bool AddAI(Dori dori)
        {
            bool res = true;
            if (dori._sold <= dori._priceOfFirst * 1.05)
            {
                Console.WriteLine("Siz bundan kam savdo ko'rishingiz mumkin !" +
                     "\nBunga ro'zimisiz?" +
                    "\n[1]-Ha \t\t [2]-Yo'q");
                int num = Select();
                if (num == 1) res = true;
                if (num == 2) res = false;
            }
            if (dori._sold >= dori._priceOfFirst * 1.9)
            {
                Console.WriteLine("Siz qimmatroq sotayabsiz, Xaridorlarni yo'qotishingiz mumkin !" +
                    "\nBunga ro'zimisiz?" +
                    "\n[1]-Ha \t\t [2]-Yo'q");

                int num = Select();
                if (num == 1) res = true;
                if (num == 2) res = false;
            }

            return res;
        }

        public static int BuyDori(Dori dori)
        {
            int res = -1;
            Console.Write("Nechta: ");
            int num = Select();
            if (num <= dori._soni)
                res = num;
            return res;
        }

        public static bool  BuySelect(Dorixona dorixona)
        {
            View(dorixona);
            bool res = true;
            var readDorilar = dorixona.ReadJson();
            if (readDorilar == null || readDorilar.Count == 0)
            {
                res = false;
            }
            else if (readDorilar.Count > 0 && readDorilar != null)
            {
            buy: Console.Write("Tanglang : ");
                  int num = Select();
                for (int i = 0; i < readDorilar.Count; i++)
                {
                    if ((i + 1) == num)
                    {
                        int result = BuyDori(readDorilar[i]);
                        if (result > 0)
                            dorixona.BuyDori(readDorilar[i], result);
                        else
                        {
                            Console.WriteLine("Buncha dori chiqmaydi");
                            goto buy;
                        }
                        
                        break;
                    }
                }
            }
            return res;
        }

        public static void Bujjet(Dorixona dorixona)
        {
            Console.WriteLine();
            Console.WriteLine("****************Bujjet****************");
            Console.WriteLine("*                                    *");
            Console.WriteLine("*    " + dorixona._bujjetOfDorixona);
            Console.WriteLine("*                                    *");
            Console.WriteLine("**************************************");
            Console.WriteLine();
        }

        public static void TodayMoney(Dorixona dorixona)
        {
            int todaySum = dorixona._bujjetOfDorixona - dorixona._yesterdayMoney;
            Console.WriteLine();
            Console.WriteLine("************Bugungi savdo************");
            
            Console.WriteLine("*                                   *");
            Console.WriteLine("*      " + todaySum);
            Console.WriteLine("*                                   *");
            Console.WriteLine("*************************************");
            Console.WriteLine();
            Console.WriteLine("Dastur yopilgach, kassa ham yopiladi !");
            Console.WriteLine("[1]-Kassani yopish\n[2]-Menu");
            int n = Select();
            if(n == 1)
            {
                dorixona.WriteYesterday();
                Console.WriteLine("Kassa yopildi, 0 dan hisoblaydi");
            }
            if(n == 2)
            {
                Menu();
            }

        }
    }
}
