using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FishingLake.BLL.Services;

namespace Fishing_Lake
{
    public partial class BookingHistoryWindow : Window
    {
        private readonly HistoryService _historyService;

        public BookingHistoryWindow()
        {
            InitializeComponent();
            _historyService = new HistoryService();
            LoadData("");
        }

        private void LoadData(string keyword)
        {
            var bookings = _historyService.GetBookings(keyword);

            var formatted = bookings.Select(b => new
            {
                CustomerName = b.User?.Name,
                Phone = b.User?.Phone,
                PondName = b.Pond?.Name,
                BookingDate = b.BookingDate.ToString("dd/MM/yyyy"),
                Price = string.Format("{0:N0}", b.Price ?? 0)

            }).ToList();

            HistoryListView.ItemsSource = formatted;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadData(SearchBox.Text.Trim());
        }
    }
}
