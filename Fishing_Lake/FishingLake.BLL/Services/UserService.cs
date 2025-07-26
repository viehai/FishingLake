
using Fishing_Lake.DAL.Repositories;
using FishingLake.DAL.Models;
using FishingLake.DAL.Repositories;

namespace Fishing_Lake.BLL.Services
{
    public class UserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public string? Register(string name, string phone, string password)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(password))
                return "Please enter complete information.";

            if (_repo.IsPhoneTaken(phone))
                return "Phone number has been registered.";

            if (!IsValidPhoneNumber(phone))
                return "Invalid phone number. Please enter correct format (eg: 098xxxxxxx).";

            var user = new User { Name = name, Phone = phone, PasswordHash = password, Role = 1, TotalBookings = 0 };
            _repo.Add(user);
            return null;
        }

        public User? Authenticate(string phone, string password)
        {
            if (string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(password))
                return null;
            return _repo.GetOne(phone, password);
        }

        private bool IsValidPhoneNumber(string phone)
        {
            var regex = new System.Text.RegularExpressions.Regex("^(03|05|07|08|09)\\d{8}$");
            return regex.IsMatch(phone);
        }
    }
}
