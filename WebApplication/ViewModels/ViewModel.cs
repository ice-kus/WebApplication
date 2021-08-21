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
        public string Region { get; set; }  // Регион
        public string City { get; set; }    // Столица
        public string Name { get; set; }    // Название
        public string Code { get; set; }    // Код
        public double Area { get; set; }    // Площадь
        public int Population { get; set; } // Население
        public IEnumerable<Country> Countries { get; set; } // Список стран
    }
}
