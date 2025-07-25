using FishingLake.DAL.Models;
using System.Collections.Generic;

namespace FishingLake.DAL.Repositories
{
    public interface IHistoryRepository
    {
        List<Booking> GetAll();
        List<Booking> GetByOwner(int ownerId);
        List<Booking> SearchByKeywordAndOwner(string keyword, int ownerId);
    }
}
