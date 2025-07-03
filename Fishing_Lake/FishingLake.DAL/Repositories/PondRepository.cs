using FishingLake.DAL.Models;
using FishingLake.DAL;

public class PondRepository
{
    private readonly FishingManagementContext _context;

    public PondRepository()
    {
        _context = new FishingManagementContext();
    }

    public void Add(Pond pond)
    {
        _context.Pond.Add(pond);
        _context.SaveChanges();
    }
}
