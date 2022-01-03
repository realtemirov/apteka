using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace Dorixona
{
    public class Dori
    {
        public string _nameOfDori;
        public string _countryOfDori;
        public int _priceOfFirst;
        public int _sold;
        public string _ID;
        public int _soni;
        
        public Dori(string name, string country, int price, int sold, int count)
        {
            _nameOfDori = name;
            _countryOfDori = country;
            _priceOfFirst = price;
            _sold = sold;
            _soni = count;
            _ID = _countryOfDori + _nameOfDori;
        }
        /*
        public string NameOfDori()
        {
            return _nameOfDori;
        }
        public string CountryOfDori()
        {
            return _countryOfDori;
        }
        
        public int PriceOfFirst()
        {
            return _priceOfFirst;
        }
        public int PriceOfSold()
        {
            return _sold;
        }

        public int GetID()
        {
            return _ID;
        }*/
    }
}