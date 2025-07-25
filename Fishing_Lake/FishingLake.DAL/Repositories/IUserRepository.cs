using FishingLake.DAL.Models;

namespace FishingLake.DAL.Repositories
{
    public interface IUserRepository
    {
        User? GetOne(string phone, string password);
        bool IsPhoneTaken(string phone);
        void Add(User user);
    }
}
