using System;
using System.Collections.Generic;

namespace FishingLake.DAL.Models;

public partial class Booking
{
    public int Id { get; set; }

    public int PondId { get; set; }

    public int UserId { get; set; }

    public DateOnly BookingDate { get; set; }

    public string? Note { get; set; }

    

    public decimal? Price { get; set; }

    

    public DateTime? PaymentTime { get; set; }

    public string? PaymentMethod { get; set; }

    public virtual Pond Pond { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
