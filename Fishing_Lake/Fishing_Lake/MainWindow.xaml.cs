using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FishingLake.DAL.Models;
using FishingLake.DAL;
using Microsoft.EntityFrameworkCore;
using FishingLake.Services;

namespace Fishing_Lake
{
    public partial class MainWindow : Window
    {
        public User? CurrentUser { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (CurrentUser != null)
            {
                WelcomeTextBlock.Text = $"Welcome, {CurrentUser.Name} 👋";
                LoadPonds();
                LoadDashboardStats();
            }
        }

        private PondService _pondService = new PondService();

        private void LoadPonds()
        {
            using var context = new FishingManagementContext();
            var ponds = context.Pond
                .Where(p => p.OwnerId == CurrentUser.Id)
                .Include(p => p.PondFishes)
                .ThenInclude(pf => pf.Fish)
                .ToList()
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Location,
                    p.Capacity,
                    p.IsDeleted,
                    FishSpeciesList = string.Join(", ", p.PondFishes.Select(pf => pf.Fish.Name))
                });

            LakeListView.ItemsSource = ponds;
        }

        private void BookLake_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button button || button.Tag is not int pondId)
            {
                MessageBox.Show("Unable to identify pond.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using var context = new FishingManagementContext();
            var pond = context.Pond.Find(pondId);
            if (pond == null)
            {
                MessageBox.Show("Pond does not exist.");
                return;
            }

            var bookingWindow = new BookingWindow(pond);
            bookingWindow.ShowDialog();

            LoadPonds();
            LoadDashboardStats();
        }

        private void AddLake_Click(object sender, RoutedEventArgs e)
        {
            var detailWindow = new DetailWindow(CurrentUser);
            detailWindow.ShowDialog();
            LoadPonds();
        }

        private void EditLake_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int pondId = (int)button.Tag;

            using (var context = new FishingManagementContext())
            {
                var pond = context.Pond
                    .Include(p => p.PondFishes)
                    .ThenInclude(pf => pf.Fish)
                    .FirstOrDefault(p => p.Id == pondId && p.OwnerId == CurrentUser.Id);

                var detailWindow = new DetailWindow(pond, CurrentUser);
                detailWindow.ShowDialog();
                LoadPonds();
            }
        }

        private void HideLake_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && int.TryParse(btn.Tag.ToString(), out int pondId))
            {
                var context = new FishingManagementContext();
                var pond = context.Pond.FirstOrDefault(p => p.Id == pondId);
                if (pond != null)
                {
                    pond.IsDeleted = true;
                    context.SaveChanges();
                    MessageBox.Show($"✅ Pond '{pond.Name}' has been hidden!");
                    LoadPonds();
                }
            }
        }

        private void RestoreLake_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && int.TryParse(btn.Tag.ToString(), out int pondId))
            {
                var context = new FishingManagementContext();
                var pond = context.Pond.FirstOrDefault(p => p.Id == pondId);
                if (pond != null)
                {
                    pond.IsDeleted = false;
                    context.SaveChanges();
                    MessageBox.Show($"✅ Pond '{pond.Name}' has been restored!");
                    LoadPonds();
                }
            }
        }

        private void OpenCustomerManagement_Click(object sender, RoutedEventArgs e)
        {
            var window = new CustomerManagementWindow(CurrentUser);
            window.ShowDialog();
        }

        private void HistoryBookingButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new BookingHistoryWindow(CurrentUser);
            window.ShowDialog();
        }

        private readonly BookingService _bookingService = new BookingService();

        private void LoadDashboardStats()
        {
            if (CurrentUser == null) return;

            int ownerId = CurrentUser.Id;

            decimal revenue = _bookingService.GetTodayRevenue(ownerId);
            RevenueTextBlock.Text = $"{revenue:N0}đ";

            int activeCustomers = _bookingService.GetActiveCustomerCountToday(ownerId);
            ActiveCustomerTextBlock.Text = $"{activeCustomers} anglers";

            int totalBookingCount = _bookingService.GetTotalBookingsToday(ownerId);
            TotalBookingTextBlock.Text = $"{totalBookingCount} bookings";
        }
    }
}
