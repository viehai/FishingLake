using Xunit;
using Moq;
using System.Collections.Generic;
using FishingLake.DAL.Models;
using FishingLake.DAL.Repositories;

public class PondServiceTests
{
    // Tests for PondService:
    // - Get all ponds
    // - SavePond adds if Id=0, updates otherwise

    [Fact]
    public void GetAllPonds_ShouldCallRepository()
    {
        var repo = new Mock<IPondRepository>();
        repo.Setup(r => r.GetAll()).Returns(new List<Pond>());

        var service = new PondService(repo.Object);
        var ponds = service.GetAllPonds();

        Assert.NotNull(ponds);
        repo.Verify(r => r.GetAll(), Times.Once);
    }
}