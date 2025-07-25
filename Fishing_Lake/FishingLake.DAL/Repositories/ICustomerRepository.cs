using FishingLake.DAL.Models;
using System.Collections.Generic;

namespace FishingLake.DAL.Repositories
{
    public interface ICustomerRepository
    {
        List<User> GetAll();
        List<Booking> GetBookingsByOwner(int ownerId);
        List<User> Search(string keyword);
        User? GetById(int id);
        void Update(User user);
    }
}
