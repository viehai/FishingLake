using Xunit;
using Fishing_Lake.DAL.Repositories;
using FishingLake.DAL.Models;

public class UserRepositoryTests
{
    // Tests for UserRepository:
    // - IsPhoneTaken returns true/false correctly
    // - Add inserts user
    // - GetOne returns user by phone + password

    [Fact]
    public void IsPhoneTaken_ShouldReturnFalse_WhenPhoneNotExist()
    {
        var repo = new UserRepository();
        bool exists = repo.IsPhoneTaken("0000000000");
        Assert.False(exists);
    }
}