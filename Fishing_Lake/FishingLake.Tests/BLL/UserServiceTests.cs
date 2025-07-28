using Xunit;
using Moq;
using Fishing_Lake.BLL.Services;
using FishingLake.DAL.Repositories;
using FishingLake.DAL.Models;

public class UserServiceTests
{
    // Tests for UserService:
    // - Register fails if phone is taken
    // - Register success if phone is not taken
    // - Authenticate returns correct user or null

    [Fact]
    public void Register_ShouldReturnError_WhenPhoneIsTaken()
    {
        var repo = new Mock<IUserRepository>();
        repo.Setup(r => r.IsPhoneTaken("0988888888")).Returns(true);

        var service = new UserService(repo.Object);
        var result = service.Register("Test", "0988888888", "pass123");

        Assert.Equal("Phone number has been registered.", result);
    }

    [Fact]
    public void Authenticate_ShouldReturnNull_WhenCredentialsInvalid()
    {
        var repo = new Mock<IUserRepository>();
        repo.Setup(r => r.GetOne("0911", "1234")).Returns((User)null);

        var service = new UserService(repo.Object);
        var result = service.Authenticate("0911", "1234");

        Assert.Null(result);
    }
}