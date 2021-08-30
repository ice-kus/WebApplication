using System.Collections.Generic;
using WebApplication.Models;

namespace WebApplication.Interfaces
{
    public interface ICountryRepository
    {
        IEnumerable<Country> GetCountryList { get; }
        void Create(string Name, string Code, long CityId, double Area, int Population, long RegionId);
        void Update(Country Country);
        void Save();
    }
}
