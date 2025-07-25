using Xunit;
using Moq;
using System.Collections.Generic;
using FishingLake.DAL.Models;
using FishingLake.DAL.Repositories;
using FishingLake.BLL.Services;

public class HistoryServiceTests
{
    // Tests for HistoryService:
    // - GetAll bookings by owner
    // - Filter bookings by keyword (phone, name, note) and ownerId

    [Fact]
    public void GetBookings_ShouldSearchByKeyword_WhenKeywordProvided()
    {
        var repo = new Mock<IHistoryRepository>();
        repo.Setup(r => r.SearchByKeywordAndOwner("vip", 1)).Returns(new List<Booking>());

        var service = new HistoryService(repo.Object);
        var result = service.GetBookings("vip", 1);

        Assert.NotNull(result);
        repo.Verify(r => r.SearchByKeywordAndOwner("vip", 1), Times.Once);
    }
}