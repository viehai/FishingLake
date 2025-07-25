using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using FishingLake.DAL.Models;

namespace FishingLake.DAL.Repositories
{
    public class BookingRepository : IBookingRepository
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
            var today = DateOnly.FromDateTime(DateTime.Today);
            return _context.Bookings
                .Include(b => b.Pond)
                .Include(b => b.User)
                .Where(b => b.BookingDate == today)
                .ToList();
        }

        public decimal GetTodayRevenue()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            return _context.Bookings
                .Where(b => b.BookingDate == today && b.PaymentTime != null)
                .Sum(b => (decimal?)b.Price) ?? 0;
        }

        public int GetTotalBookingsByUserAndOwner(string phone, int ownerId)
        {
            return _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Pond)
                .Where(b => b.User != null &&
                            b.Pond != null &&
                            b.User.Phone == phone &&
                            b.Pond.OwnerId == ownerId)
                .Count();
        }
    }
}
