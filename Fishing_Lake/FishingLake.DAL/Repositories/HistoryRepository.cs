using FishingLake.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FishingLake.DAL.Repositories
{
    public class HistoryRepository: IHistoryRepository
    {
        public List<Booking> GetAll()
        {
            using var context = new FishingManagementContext();
            return context.Bookings
                .Where(b => b.Pond != null && b.User != null)
                .Include(b => b.User)
                .Include(b => b.Pond)
                .ToList();
        }

        public List<Booking> GetByOwner(int ownerId)
        {
            using var context = new FishingManagementContext();
            return context.Bookings
                .Include(b => b.User)
                .Include(b => b.Pond)
                .Where(b => b.Pond.OwnerId == ownerId)
                .ToList();
        }

        public List<Booking> SearchByKeywordAndOwner(string keyword, int ownerId)
        {
            using var context = new FishingManagementContext();
            return context.Bookings
                .Include(b => b.User)
                .Include(b => b.Pond)
                .Where(b => b.Pond.OwnerId == ownerId &&
                            (b.User.Name.Contains(keyword) || b.User.Phone.Contains(keyword)))
                .ToList();
        }
    }
}
