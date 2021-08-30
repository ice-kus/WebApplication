using System.Collections.Generic;
using WebApplication.Interfaces;
using WebApplication.Models;

namespace WebApplication.Repository
{
    public class RegionRepository : IRegionRepository
    {
        private readonly ApplicationDbContext db;
        public RegionRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<Region> GetRegionList => db.Region;
        public void Create(string Name)
        {
            db.Region.Add
                (
                new Region
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
