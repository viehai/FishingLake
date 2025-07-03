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

namespace Fishing_Lake
{
    public partial class MainWindow : Window
    {
        public User? CurrentUser { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            LoadPonds();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (CurrentUser != null)
            {
                WelcomeTextBlock.Text = $"Xin chào, {CurrentUser.Name} 👋";
            }
        }

        private void LoadPonds()
        {
            using (var context = new FishingManagementContext())
            {
                var ponds = context.Pond
                    .Include(p => p.PondFishes)
                    .ThenInclude(pf => pf.Fish)
                    .ToList()
                    .Select(p => new
                    {
                        p.Id,
                        p.Name,
                        p.Location,
                        p.Capacity,
                        FishSpeciesList = string.Join(", ", p.PondFishes.Select(pf => $"{pf.Fish.Name} ({pf.Quantity})"))
                    }).ToList();

                LakeListView.ItemsSource = ponds;
            }
        }

        private void BookLake_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int pondId = (int)button.Tag;

            using (var context = new FishingManagementContext())
            {
                var pond = context.Pond.Find(pondId);
                if (pond == null)
                {
                    MessageBox.Show("Hồ không tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (CurrentUser == null)
                {
                    MessageBox.Show("Không xác định được người dùng!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                int userId = CurrentUser.Id;

                var booking = new Booking
                {
                    PondId = pondId,
                    UserId = userId,
                    BookingDate = DateOnly.FromDateTime(DateTime.Today),
                    Status = "Pending",
                    Price = 100000,
                    IsPaid = false,
                    PaymentMethod = "Cash"
                };

                context.Bookings.Add(booking);
                context.SaveChanges();

                MessageBox.Show($"Đã đặt hồ {pond.Name} thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            LoadPonds();
        }

        private void AddLake_Click(object sender, RoutedEventArgs e)
        {
            var detailWindow = new DetailWindow();
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
                    .FirstOrDefault(p => p.Id == pondId);

                var detailWindow = new DetailWindow(pond);
                detailWindow.ShowDialog();
                LoadPonds();
            }
        }

        private void DeleteLake_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int pondId = (int)button.Tag;

            var result = MessageBox.Show("Bạn có chắc chắn muốn xoá hồ này không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result != MessageBoxResult.Yes)
                return;

            using (var context = new FishingManagementContext())
            {
                var pond = context.Pond
                    .Include(p => p.PondFishes)
                    .FirstOrDefault(p => p.Id == pondId);

                if (pond == null)
                {
                    MessageBox.Show("Hồ không tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var pondService = new PondService();
                pondService.DeletePond(pond);
            }

            MessageBox.Show("Đã xoá hồ thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadPonds(); // Reload danh sách
        }

    }
}