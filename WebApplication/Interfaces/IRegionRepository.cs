using System.Collections.Generic;
using WebApplication.Models;

namespace WebApplication.Interfaces
{
    public interface IRegionRepository
    {
        IEnumerable<Region> GetRegionList { get; }
        void Create(string Name);
        void Save();
    }
}
