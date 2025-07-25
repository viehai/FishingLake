using FishingLake.DAL.Models;
using System.Collections.Generic;

namespace FishingLake.DAL.Repositories
{
    public interface IBookingRepository
    {
        User? GetUserByPhone(string phone);
        void AddUser(User user);
        Pond? GetPondById(int id);
        void AddBooking(Booking booking);
        List<Booking> GetTodayBookings();
        decimal GetTodayRevenue();

        int GetTotalBookingsByUserAndOwner(string phone, int ownerId);

    }
}
