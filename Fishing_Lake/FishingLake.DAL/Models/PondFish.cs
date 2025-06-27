using System;
using System.Collections.Generic;

namespace FishingLake.DAL.Models;

public partial class PondFish
{
    public int Id { get; set; }

    public int PondId { get; set; }

    public int FishId { get; set; }

    public int? Quantity { get; set; }

    public virtual FishSpecy Fish { get; set; } = null!;

    public virtual Pond Pond { get; set; } = null!;
}
