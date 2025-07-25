using Xunit;
using Moq;
using System.Collections.Generic;
using FishingLake.DAL.Models;
using FishingLake.DAL.Repositories;
using FishingLake.BLL.Services;

public class CustomerServiceTests
{
    // Tests for CustomerService:
    // - Get list of customers with Role = 2
    // - Sort by VIP status (≥5 bookings) and booking count descending

    [Fact]
    public void GetCustomers_ShouldOrderByVipAndBookingCount()
    {
        var repo = new Mock<ICustomerRepository>();
        repo.Setup(r => r.GetAll()).Returns(new List<User>
        {
            new User { Name = "A", TotalBookings = 2, IsVip = false },
            new User { Name = "B", TotalBookings = 10, IsVip = true }
        });

        var service = new CustomerService(repo.Object);
        var customers = service.GetCustomers();

        Assert.Equal("B", customers[0].Name);
    }
}