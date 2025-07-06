using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FishingLake.DAL.Models;

namespace FishingLake.DAL.Repositories
{
    public class CustomerRepository
    {
        public List<User> GetAll()
        {
            using var context = new FishingManagementContext();
            return context.Users.Where(u => u.Role == 2).ToList();
        }

        public List<User> Search(string keyword)
        {
            using var context = new FishingManagementContext();
            return context.Users
                .Where(u => u.Role == 2 &&
                            (u.Name.Contains(keyword) || u.Phone.Contains(keyword)))
                .ToList();
        }

        public User? GetById(int id)
        {
            using var context = new FishingManagementContext();
            return context.Users.FirstOrDefault(u => u.Id == id && u.Role == 2);
        }

        public void Update(User user)
        {
            using var context = new FishingManagementContext();
            context.Users.Update(user);
            context.SaveChanges();
        }
    }
}
