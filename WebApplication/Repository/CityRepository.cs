using System.Collections.Generic;
using WebApplication.Interfaces;
using WebApplication.Models;

namespace WebApplication.Repository
{
    public class CityRepository : ICityRepository
    {
        private readonly ApplicationDbContext db;
        public CityRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<City> GetCityList => db.City;
        public void Create(string Name)
        {
            db.City.Add
                (
                new City
                {
                    name = Name
                }
                );
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
