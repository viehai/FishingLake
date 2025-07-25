using FishingLake.DAL.Models;
using FishingLake.DAL.Repositories;

namespace FishingLake.BLL.Services
{
    public class HistoryService
    {
        private readonly IHistoryRepository _repo;

        public HistoryService(IHistoryRepository repo)
        {
            _repo = repo;
        }

        public List<Booking> GetBookings(string keyword, int ownerId)
        {
            return string.IsNullOrWhiteSpace(keyword) ? _repo.GetByOwner(ownerId) : _repo.SearchByKeywordAndOwner(keyword, ownerId);
        }
    }
}
