using Xunit;
using Moq;
using System;
using FishingLake.Services;
using FishingLake.DAL.Models;
using FishingLake.DAL.Repositories;
using System.Collections.Generic;

public class BookingServiceTests
{
    // Tests for BookingService:
    // - Create booking with new/existing user
    // - Handle full pond capacity
    // - VIP discount when user has ≥5 bookings
    // - Booking result message formatting
    // - GetTodayRevenue, GetTotalBookingsToday, GetActiveCustomerCountToday (by ownerId)

    [Fact]
    public void BookPondForCustomer_ShouldReturnVipMessage_WhenUserIsVip()
    {
        var repo = new Mock<IBookingRepository>();
        var pond = new Pond { Id = 1, OwnerId = 10, Capacity = 3 };
        var user = new User { Id = 5, Name = "Khách VIP", Phone = "0909123456" };

        repo.Setup(r => r.GetUserByPhone(user.Phone)).Returns(user);
        repo.Setup(r => r.GetPondById(pond.Id)).Returns(pond);
        repo.Setup(r => r.GetTotalBookingsByUserAndOwner(user.Phone, pond.OwnerId)).Returns(5);

        var service = new BookingService(repo.Object);
        var result = service.BookPondForCustomer(pond.Id, user.Name, user.Phone, DateTime.Today, 100000, "", DateTime.Now);

        Assert.Contains("VIP", result);
        Assert.Contains("80,000", result);
    }
}