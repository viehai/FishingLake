using FishingLake.DAL.Models;
using System.Collections.Generic;

public class PondService
{
    private readonly PondRepository _repo = new();

    public List<Pond> GetAllPonds()
    {
        return _repo.GetAll();
    }
    public List<Pond> GetPondsByOwner(int ownerId, bool includeHidden = false)
    {
        return _repo.GetByOwnerId(ownerId, includeHidden);
    }



    public void AddPond(Pond pond)
    {
        _repo.Add(pond);
    }

    public void UpdatePond(Pond pond)
    {
        _repo.Update(pond);
    }

    public void DeletePond(Pond pond)
    {
        _repo.Delete(pond);
    }
}