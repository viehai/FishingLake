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

            // Tính giá + hiển thị thông báo nếu VIP
            decimal originalPrice = 100_000m;
            decimal finalPrice = originalPrice;
            string discountMsg = "";

            if (user.IsVip == true)
            {
                finalPrice = originalPrice * 0.8m;
                discountMsg = $"Khách là VIP, được giảm 20%. Giá từ {originalPrice:N0}đ còn {finalPrice:N0}đ.";
            }
            var booking = new Booking
            {
                PondId = pondId,
                UserId = user.Id,
                BookingDate = DateOnly.FromDateTime(bookingDate),
                Price = finalPrice,
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

            string result = $"✅ Đã đặt hồ thành công cho khách {user.Name}!";

            if (user.IsVip == true)
            {
                // Nếu là VIP, thêm dòng thông báo giảm giá
                result +=
                    $"\nKhách là VIP, được giảm 20%. Giá từ {originalPrice:N0}đ còn {finalPrice:N0}đ.";
            }

            result += $"\nHết hạn lúc 24h ngày {bookingDate:dd/MM/yyyy}.";

            return result;
        }
    }
}
