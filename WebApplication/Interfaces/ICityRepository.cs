using System.Collections.Generic;
using WebApplication.Models;

namespace WebApplication.Interfaces
{
    public interface ICityRepository
    {
        IEnumerable<City> GetCityList { get; }
        void Create(string Name);
        void Save();
    }
}
