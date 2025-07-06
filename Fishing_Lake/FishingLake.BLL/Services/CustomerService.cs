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
        private readonly CustomerRepository _repo;

        public CustomerService()
        {
            _repo = new CustomerRepository();
        }

        public List<User> GetCustomers(string keyword = "")
        {
            var customers = string.IsNullOrWhiteSpace(keyword)
                ? _repo.GetAll()
                : _repo.Search(keyword);

            return customers
                .OrderByDescending(c => c.IsVip)
                .ThenByDescending(c => c.TotalBookings)
                .ToList();
        }

        public User? GetById(int id) => _repo.GetById(id);

        
    }

}
