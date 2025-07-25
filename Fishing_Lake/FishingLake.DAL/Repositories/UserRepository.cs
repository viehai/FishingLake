
using FishingLake.DAL;
using FishingLake.DAL.Models;
using FishingLake.DAL.Repositories;
using System.Linq;

namespace Fishing_Lake.DAL.Repositories
{
    public class UserRepository: IUserRepository
    {
        private FishingManagementContext? _context;

        public User? GetOne(string phone, string password)
        {
            _context = new();
            return _context.Users.FirstOrDefault(x => x.Phone == phone && x.PasswordHash == password);
        }

        public bool IsPhoneTaken(string phone)
        {
            _context = new();
            return _context.Users.Any(u => u.Phone == phone);
        }

        public void Add(User user)
        {
            _context = new();
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
