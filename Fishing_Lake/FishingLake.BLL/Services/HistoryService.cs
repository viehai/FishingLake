using FishingLake.DAL.Models;
using FishingLake.DAL.Repositories;

namespace FishingLake.BLL.Services
{
    public class HistoryService
    {
        private readonly HistoryRepository _repo;

        public HistoryService()
        {
            _repo = new HistoryRepository();
        }

        public List<Booking> GetBookings(string keyword = "")
        {
            var bookings = string.IsNullOrWhiteSpace(keyword)
                ? _repo.GetAll()
                : _repo.Search(keyword);

            return bookings
                .OrderByDescending(b => b.BookingDate)
                .ToList();
        }

        public List<Booking> GetByUserId(int userId)
        {
            return _repo.GetByUserId(userId);
        }
    }
}
