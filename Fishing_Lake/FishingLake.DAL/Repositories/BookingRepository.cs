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

        

        public Pond? GetPondById(int id)
        {
            return _context.Pond.FirstOrDefault(p => p.Id == id);
        }

        public void AddBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }

        public List<Booking> GetTodayBookings()
        {
            using var context = new FishingManagementContext();
            var today = DateOnly.FromDateTime(DateTime.Today);
            return context.Bookings
                          .Where(b => b.BookingDate == today)
                          .ToList();
        }

        public decimal GetTodayRevenue()
        {
            using var context = new FishingManagementContext();
            var today = DateOnly.FromDateTime(DateTime.Today);
            return context.Bookings
                          .Where(b => b.BookingDate == today && b.PaymentTime != null)
                          .Sum(b => (decimal?)b.Price) ?? 0;
        }
    }
}

