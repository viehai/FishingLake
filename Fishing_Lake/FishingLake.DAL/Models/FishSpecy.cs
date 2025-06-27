using System;
using System.Collections.Generic;

namespace FishingLake.DAL.Models;

public partial class FishSpecy
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public double? AverageWeight { get; set; }

    public string? ImageUrl { get; set; }

    public virtual ICollection<PondFish> PondFishes { get; set; } = new List<PondFish>();
}
