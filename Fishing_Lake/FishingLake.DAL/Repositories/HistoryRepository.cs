using FishingLake.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FishingLake.DAL.Repositories
{
    public class HistoryRepository
    {
        public List<Booking> GetAll()
        {
            using var context = new FishingManagementContext();
            return context.Bookings
                .Include(b => b.User)
                .Include(b => b.Pond)
                .Where(b => b.User.Role == 2)
                .ToList();
        }

        public List<Booking> Search(string keyword)
        {
            using var context = new FishingManagementContext();
            return context.Bookings
                .Include(b => b.User)
                .Include(b => b.Pond)
                .Where(b => b.User.Role == 2 &&
                           (b.User.Name.Contains(keyword) || b.User.Phone.Contains(keyword)))
                .ToList();
        }

        public List<Booking> GetByUserId(int userId)
        {
            using var context = new FishingManagementContext();
            return context.Bookings
                .Include(b => b.User)
                .Include(b => b.Pond)
                .Where(b => b.UserId == userId)
                .ToList();
        }
    }
}
