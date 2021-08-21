using System.Collections.Generic;
using WebApplication.Models;

namespace WebApplication.Repository
{
    public class CityRepository
    {
        private readonly ApplicationDbContext applicationDb;
        public CityRepository(ApplicationDbContext applicationDb)
        {
            this.applicationDb = applicationDb;
        }
        public IEnumerable<City> All => applicationDb.City;
        public void Add(string Name)
        {
            applicationDb.City.Add
                (
                new City
                {
                    Name = Name
                }
                );
            applicationDb.SaveChanges();
        }
    }
}
