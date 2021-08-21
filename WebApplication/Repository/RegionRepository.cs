using System.Collections.Generic;
using WebApplication.Models;

namespace WebApplication.Repository
{
    public class RegionRepository
    {
        private readonly ApplicationDbContext applicationDb;
        public RegionRepository(ApplicationDbContext appDBContent)
        {
            this.applicationDb = appDBContent;
        }
        public IEnumerable<Region> All => applicationDb.Region;
        public void Add(string Name)
        {
            applicationDb.Region.Add
                (
                new Region
                {
                    Name = Name
                }
                );
            applicationDb.SaveChanges();
        }
    }
}
