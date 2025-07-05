using FishingLake.DAL;
using FishingLake.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace FishingLake.Services
{
    public class BookingService
    {
        public string BookPondForCustomer(
            int pondId,
            string customerName,
            string phone,
            DateTime bookingDate,
            decimal price,
            string note,
            DateTime paymentTime) // mới thêm
        {
            using var context = new FishingManagementContext();

            var user = context.Users.FirstOrDefault(u => u.Phone == phone);

            if (user == null)
            {
                user = new User
                {
                    Name = customerName,
                    Phone = phone,
                    Role = 2,
                    TotalBookings = 1,
                    IsVip = false
                };
                context.Users.Add(user);
                context.SaveChanges();
            }
            else
            {
                user.TotalBookings++;
                if (user.TotalBookings >= 5)
                    user.IsVip = true;

                context.Users.Update(user);
                context.SaveChanges();
            }

            var pond = context.Pond.FirstOrDefault(p => p.Id == pondId);
            if (pond == null)
                return "Không tìm thấy hồ.";

            var booking = new Booking
            {
                PondId = pondId,
                UserId = user.Id,
                BookingDate = DateOnly.FromDateTime(bookingDate),
                Price = price,
                PaymentMethod = "Cash",
                PaymentTime = paymentTime,
                Note = string.IsNullOrWhiteSpace(note)
                    ? $"Hết hạn lúc 24h ngày {bookingDate:dd/MM/yyyy}"
                    : note
            };

            if (pond.Capacity <= 0)
                return "Hồ đã hết chỗ cho hôm nay.";

            pond.Capacity--; // Giảm slot
            context.Pond.Update(pond);


            context.Bookings.Add(booking);
            context.SaveChanges();

            return $"✅ Đã đặt hồ thành công cho khách {user.Name}!\nHết hạn lúc 24h ngày {bookingDate:dd/MM/yyyy}.";
        }
    }
}
