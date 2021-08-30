using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebApplication.Models;

namespace WebApplication.ViewModels
{
    public class ViewModel
    {
        public string region { get; set; }  // Регион
        public string city { get; set; }    // Столица
        public string name { get; set; }    // Название
        public string code { get; set; }    // Код
        public double area { get; set; }    // Площадь
        public int population { get; set; } // Население
        public IEnumerable<Country> countries { get; set; } // Список стран
    }
}
