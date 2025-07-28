using FishingLake.DAL.Models;
using FishingLake.DAL.Repositories;
using System.Collections.Generic;

public class PondService
{
    private readonly IPondRepository _repo;

    public PondService(IPondRepository repo)
    {
        _repo = repo;
    }

    public List<Pond> GetAllPonds() => _repo.GetAll();
    public List<Pond> GetPondsByOwner(int ownerId, bool includeHidden = false) => _repo.GetByOwnerId(ownerId, includeHidden);
    public void AddPond(Pond pond) => _repo.Add(pond);
    public void UpdatePond(Pond pond) => _repo.Update(pond);
    
}