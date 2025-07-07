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
    DateTime paymentTime)
        {
            using var context = new FishingManagementContext();

            // 1. Tìm hoặc tạo người dùng
            var user = context.Users.FirstOrDefault(u => u.Phone == phone);
            if (user == null)
            {
                user = new User
                {
                    Name = customerName,
                    Phone = phone,
                    Role = 2
                };
                context.Users.Add(user);
                context.SaveChanges();
            }

            // 2. Tìm hồ (có OwnerId để xác định chủ hồ)
            var pond = context.Pond
                .Include(p => p.Owner)
                .FirstOrDefault(p => p.Id == pondId);

            if (pond == null)
                return "Không tìm thấy hồ.";

            if (pond.Capacity <= 0)
                return "Hồ đã hết chỗ cho hôm nay.";

            // 3. Đếm số booking của user với hồ của cùng OwnerId
            int totalBookingWithThisOwner = context.Bookings
                .Include(b => b.Pond)
                .Where(b => b.User.Phone == phone && b.Pond.OwnerId == pond.OwnerId)
                .Count();

            bool isVipForThisOwner = totalBookingWithThisOwner >= 5;

            // 4. Tính giá
            decimal originalPrice = price;
            decimal finalPrice = isVipForThisOwner ? originalPrice * 0.8m : originalPrice;

            // 5. Tạo booking
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

            pond.Capacity--; // Trừ chỗ
            context.Pond.Update(pond);
            context.Bookings.Add(booking);
            context.SaveChanges();

            // 6. Kết quả trả về
            string result = $"✅ Đã đặt hồ thành công cho khách {user.Name}!";

            if (isVipForThisOwner)
            {
                result += $"\nKhách là VIP, được giảm 20%. Giá từ {originalPrice:N0}đ còn {finalPrice:N0}đ.";
            }

            result += $"\nHết hạn lúc 24h ngày {bookingDate:dd/MM/yyyy}.";

            return result;
        }


        public decimal GetTodayRevenue(int ownerId)
        {
            using var context = new FishingManagementContext();
            var today = DateOnly.FromDateTime(DateTime.Today);

            return context.Bookings
                .Where(b => b.BookingDate == today && b.Pond.OwnerId == ownerId)
                .Sum(b => b.Price ?? 0);
        }


        public int GetActiveCustomerCountToday(int ownerId)
        {
            using var context = new FishingManagementContext();
            var today = DateOnly.FromDateTime(DateTime.Today);

            return context.Bookings
                .Where(b => b.BookingDate == today && b.Pond.OwnerId == ownerId)
                .Select(b => b.UserId)
                .Distinct()
                .Count();
        }


        public int GetTotalBookingsToday(int ownerId)
        {
            using var context = new FishingManagementContext();
            var today = DateOnly.FromDateTime(DateTime.Today);

            return context.Bookings
                .Count(b => b.BookingDate == today && b.Pond.OwnerId == ownerId);
        }

    }
}
