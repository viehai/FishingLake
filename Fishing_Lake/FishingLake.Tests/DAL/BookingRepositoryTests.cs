using Xunit;
using FishingLake.DAL.Repositories;
using FishingLake.DAL.Models;

public class BookingRepositoryTests
{
    // Tests for BookingRepository:
    // - GetUserByPhone returns correct user
    // - AddUser inserts and persists user
    // - GetPondById returns correct pond
    // - AddBooking inserts booking
    // - GetTodayBookings returns bookings from today with User and Pond included

    [Fact]
    public void GetUserByPhone_ShouldReturnNull_WhenUserNotFound()    {
        var repo = new BookingRepository();
        var result = repo.GetUserByPhone("0000000000");
        Assert.Null(result);
    }

    [Fact]
    public void AddBooking_ShouldIncreaseBookingCount()
    {
        var repo = new BookingRepository();
        var booking = new Booking { PondId = 1, UserId = 1, BookingDate = DateOnly.FromDateTime(System.DateTime.Today) };
        repo.AddBooking(booking);
        var today = repo.GetTodayBookings();
        Assert.Contains(today, b => b.UserId == 1 && b.PondId == 1);
    }
}