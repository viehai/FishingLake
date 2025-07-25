using Xunit;
using FishingLake.DAL.Repositories;
using FishingLake.DAL.Models;

public class HistoryRepositoryTests
{
    // Tests for HistoryRepository:
    // - GetAll returns all bookings
    // - GetAllByOwner filters by Pond.OwnerId
    // - SearchByKeywordAndOwner matches phone, name, or note

    [Fact]
    public void GetAll_ShouldReturnBookings()
    {
        var repo = new HistoryRepository();
        var all = repo.GetAll();
        Assert.NotNull(all);
    }
}