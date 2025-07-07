using System;
using System.Collections.Generic;

namespace FishingLake.DAL.Models;

public partial class Pond
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Location { get; set; }

    public string? Description { get; set; }

    public int Capacity { get; set; }

    public int OwnerId { get; set; }

    public bool IsDeleted { get; set; } = false;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual User Owner { get; set; } = null!;

    public virtual ICollection<PondFish> PondFishes { get; set; } = new List<PondFish>();
}
