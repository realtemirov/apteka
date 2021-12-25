using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace darslar
{
    public class Dori
    {
        public string _nomi;
        public string _davlati;
        public string _narxi;
        public string _soni;
        
        public Dori(string nomi, string davlati, string narxi, string soni)
        {
            _nomi = nomi;
            _davlati = davlati;
            _narxi = narxi;
            _soni = soni;
        }
    }
}
