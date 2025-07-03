// PondService.cs
using FishingLake.DAL.Models;
using System.Collections.Generic;
using System.Linq;

public class PondService
{
    private readonly PondRepository _repo = new();

    public string? AddPondWithFishes(
        string name,
        string location,
        string description,
        int capacity,
        int ownerId,
        List<PondFish>? pondFishEntries)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(location) || capacity <= 0)
            return "Thông tin hồ không hợp lệ.";

        var pond = new Pond
        {
            Name = name,
            Location = location,
            Description = description,
            Capacity = capacity,
            OwnerId = ownerId,
            PondFishes = pondFishEntries?
                .Where(pf => pf.FishId > 0 && pf.Quantity.HasValue && pf.Quantity.Value > 0)
                .ToList()
        };

        _repo.Add(pond);
        return null;
    }
}
