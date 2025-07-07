using System;
using System.Windows;
using Fishing_Lake.BLL.Services;

namespace Fishing_Lake
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private UserService _service = new();

        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            string phone = PhoneTextBox.Text;
            string password = PasswordBox.Password;

            string? error = _service.Register(name, phone, password);

            if (error != null)
            {
                MessageBox.Show(error, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show("Registration successful! Please log in to continue.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close(); // Close the registration window and return to login
        }
    }
}
