using Fishing_Lake.BLL.Services;

using FishingLake.DAL.Models;
using System.Windows;

namespace Fishing_Lake
{
    public partial class LoginWindow : Window
    {
        private UserService _service = new();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string phone = PhoneTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ số điện thoại và mật khẩu.", "Thiếu thông tin", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            User? user = _service.Authenticate(phone, password);

            if (user == null)
            {
                MessageBox.Show("Sai số điện thoại hoặc mật khẩu.", "Đăng nhập thất bại", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (user.Role != 1) // Chỉ cho phép "chủ hồ" đăng nhập
            {
                MessageBox.Show("Chỉ chủ hồ mới được đăng nhập vào hệ thống này.", "Truy cập bị từ chối", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            // Đăng nhập thành công
            MainWindow mainWindow = new MainWindow();
            mainWindow.CurrentUser = user; // Gán user nếu cần truyền thông tin
            mainWindow.Show();
            this.Close();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.ShowDialog(); // mở dạng modal
        }
    }
}
