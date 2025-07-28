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
            
            var user = _repo.GetUserByPhone(phone);
            if (user == null)
            {
                user = new User { Name = customerName, Phone = phone, Role = 2 };
                _repo.AddUser(user);
                user = _repo.GetUserByPhone(phone);
            }

            
            var pond = _repo.GetPondById(pondId);
            if (pond == null)
                return "Pond not found.";

            if (pond.Capacity <= 0)
                return "The lake is full for today.";

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
                    ? $"Expires at 24:00 {bookingDate:dd/MM/yyyy}"
                    : note
            };

            // Giảm sức chứa và lưu booking
            pond.Capacity--;
            _repo.AddBooking(booking);

            
            string result = $"Pool successfully booked for customer {user.Name}!";
            if (isVip)
                result += $"\nVIP customers get 20% off. Price from {price:N0}đ to {finalPrice:N0}đ.";
            result += $"\nExpires at 24:00 {bookingDate:dd/MM/yyyy}.";
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
