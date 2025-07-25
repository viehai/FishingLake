using Xunit;
using FishingLake.DAL.Repositories;
using FishingLake.DAL.Models;

public class CustomerRepositoryTests
{
    // Tests for CustomerRepository:
    // - GetAll returns users with Role = 2

    [Fact]
    public void GetAll_ShouldReturnOnlyCustomers()
    {
        var repo = new CustomerRepository();
        var list = repo.GetAll();
        Assert.All(list, u => Assert.Equal(2, u.Role));
    }
}