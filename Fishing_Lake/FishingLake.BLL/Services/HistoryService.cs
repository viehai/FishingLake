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

        public List<Booking> GetBookings(string keyword, int ownerId)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return _repo.GetByOwner(ownerId);

            return _repo.SearchByKeywordAndOwner(keyword, ownerId);
        }
    }
}
