using System.Collections.Generic;
using WebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Repository
{
    public class CountryRepository
    {
        private readonly ApplicationDbContext applicationDb;
        public CountryRepository(ApplicationDbContext applicationDb)
        {
            this.applicationDb = applicationDb;
        }
        public IEnumerable<Country> All => applicationDb.Country.Include(i => i.Region).Include(i => i.City);
        public void Add(string Name, string Code, long CityId, double Area, int Population, long RegionId)
        {
            applicationDb.Country.Add
                (
                new Country
                {
                    Name = Name,
                    Code = Code,
                    CityId = CityId,
                    Area = Area,
                    Population = Population,
                    RegionId = RegionId
                }
                );
            applicationDb.SaveChanges();
        }
        public void Update(Country Country)
        {
            applicationDb.Country.Update(Country);
            applicationDb.SaveChanges();
        }
    }
}
