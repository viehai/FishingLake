using FishingLake.DAL.Models;
using FishingLake.DAL.Repositories;
using System;
using System.Linq;

namespace FishingLake.Services
{
    public class BookingService
    {
        private readonly IBookingRepository _repo;

        public BookingService(IBookingRepository repo)
        {
            _repo = repo;
        }

        public string BookPondForCustomer(int pondId, string customerName, string phone, DateTime bookingDate, decimal price, string note, DateTime paymentTime)
        {
            // Lấy hoặc tạo user
            var user = _repo.GetUserByPhone(phone);
            if (user == null)
            {
                user = new User { Name = customerName, Phone = phone, Role = 2 };
                _repo.AddUser(user);
                user = _repo.GetUserByPhone(phone);
            }

            // Lấy hồ
            var pond = _repo.GetPondById(pondId);
            if (pond == null)
                return "Không tìm thấy hồ.";

            if (pond.Capacity <= 0)
                return "Hồ đã hết chỗ cho hôm nay.";

            // Tính số booking của user theo chủ hồ
            int totalBooking = _repo.GetTotalBookingsByUserAndOwner(phone, pond.OwnerId);
            bool isVip = totalBooking >= 5;
            decimal finalPrice = isVip ? price * 0.8m : price;

            
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

            // Giảm sức chứa và lưu booking
            pond.Capacity--;
            _repo.AddBooking(booking);

            
            string result = $"Đã đặt hồ thành công cho khách {user.Name}!";
            if (isVip)
                result += $"\nKhách là VIP, được giảm 20%. Giá từ {price:N0}đ còn {finalPrice:N0}đ.";
            result += $"\nHết hạn lúc 24h ngày {bookingDate:dd/MM/yyyy}.";
            return result;
        }

        public decimal GetTodayRevenue(int ownerId)
        {
            return _repo.GetTodayBookings()
                .Where(b => b.Pond != null && b.Pond.OwnerId == ownerId && b.PaymentTime != null)
                .Sum(b => b.Price ?? 0);
        }

        public int GetActiveCustomerCountToday(int ownerId)
        {
            return _repo.GetTodayBookings()
                .Where(b => b.Pond != null && b.Pond.OwnerId == ownerId)
                .Select(b => b.UserId)
                .Distinct()
                .Count();
        }

        public int GetTotalBookingsToday(int ownerId)
        {
            return _repo.GetTodayBookings()
                .Count(b => b.Pond != null && b.Pond.OwnerId == ownerId);
        }
    }
}
