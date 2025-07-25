using Xunit;
using FishingLake.DAL.Repositories;
using FishingLake.DAL.Models;

public class PondRepositoryTests
{
    // Tests for PondRepository:
    // - GetAll returns all ponds
    // - GetByOwnerId filters by OwnerId
    // - Add adds pond
    // - Update modifies existing pond
    // - GetById returns correct pond

    [Fact]
    public void GetAll_ShouldReturnList()
    {
        var repo = new PondRepository();
        var ponds = repo.GetAll();
        Assert.NotNull(ponds);
    }
}