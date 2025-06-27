using System;
using System.Collections.Generic;

namespace FishingLake.DAL.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? PasswordHash { get; set; }

    public int Role { get; set; }

    public int? TotalBookings { get; set; }

    public bool? IsVip { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Pond> Ponds { get; set; } = new List<Pond>();
}
