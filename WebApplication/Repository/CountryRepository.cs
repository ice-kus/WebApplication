using System.Collections.Generic;
using WebApplication.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication.Interfaces;

namespace WebApplication.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationDbContext db;
        public CountryRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<Country> GetCountryList => db.Country.Include(i => i.region).Include(i => i.city);
        public void Create(string Name, string Code, long CityId, double Area, int Population, long RegionId)
        {
            db.Country.Add
                (
                new Country
                {
                    name = Name,
                    code = Code,
                    cityid = CityId,
                    area = Area,
                    population = Population,
                    regionid = RegionId
                }
                );
        }
        public void Update(Country Country)
        {
            db.Country.Update(Country);
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
