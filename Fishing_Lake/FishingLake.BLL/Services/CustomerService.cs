using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FishingLake.DAL.Models;
using FishingLake.DAL.Repositories;

namespace FishingLake.BLL.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _repo;

        public CustomerService(ICustomerRepository repo)
        {
            _repo = repo;
        }

        public List<User> GetCustomersByOwner(int ownerId, string keyword = "")
        {
            var bookings = _repo.GetBookingsByOwner(ownerId);
            var grouped = bookings.GroupBy(b => b.User.Phone).Select(g =>
            {
                var user = g.First().User;
                int total = g.Count();
                return new User
                {
                    Id = user.Id,
                    Name = user.Name,
                    Phone = user.Phone,
                    Role = user.Role,
                    TotalBookings = total,
                    IsVip = total >= 5
                };
            });
            if (!string.IsNullOrWhiteSpace(keyword))
                grouped = grouped.Where(u => u.Name.Contains(keyword) || u.Phone.Contains(keyword));

            return grouped.OrderByDescending(u => u.IsVip).ThenByDescending(u => u.TotalBookings).ToList();
        }

        public User? GetById(int id) => _repo.GetById(id);
    }

}
