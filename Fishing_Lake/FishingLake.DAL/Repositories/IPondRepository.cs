using FishingLake.DAL.Models;
using System.Collections.Generic;

namespace FishingLake.DAL.Repositories
{
    public interface IPondRepository
    {
        List<Pond> GetAll();
        void Add(Pond pond);
        void Update(Pond pond);
        
        List<Pond> GetByOwnerId(int ownerId, bool includeHidden = false);
    }
}
