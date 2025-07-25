using Fishing_Lake.BLL.Services;
using Fishing_Lake.DAL.Repositories;
using FishingLake.DAL.Models;
using System.Windows;

namespace Fishing_Lake
{
    public partial class LoginWindow : Window
    {
        private readonly UserService _service;

        public LoginWindow()
        {
            InitializeComponent();
            _service = new UserService(new UserRepository());
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string phone = PhoneTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both phone number and password.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            User? user = _service.Authenticate(phone, password);

            if (user == null)
            {
                MessageBox.Show("Incorrect phone number or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (user.Role != 1)
            {
                MessageBox.Show("Only pond owners are allowed to access this system.", "Access Denied", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            MainWindow mainWindow = new MainWindow
            {
                CurrentUser = user
            };
            mainWindow.Show();
            this.Close();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.ShowDialog();
        }
    }
}
