using System.Linq;
using FishingLake.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FishingLake.DAL.Repositories
{
    public class BookingRepository
    {
        private readonly FishingManagementContext _context = new();

        public User? GetUserByPhone(string phone)
        {
            return _context.Users.FirstOrDefault(u => u.Phone == phone);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public Pond? GetPondById(int id)
        {
            return _context.Pond.FirstOrDefault(p => p.Id == id);
        }

        public void AddBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }
    }
}
