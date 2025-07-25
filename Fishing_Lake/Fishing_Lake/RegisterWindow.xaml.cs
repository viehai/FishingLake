using Fishing_Lake.BLL.Services;
using Fishing_Lake.DAL.Repositories;
using System;
using System.Windows;

namespace Fishing_Lake
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private readonly UserService _service;

        public RegisterWindow()
        {
            InitializeComponent();
            _service = new UserService(new UserRepository());
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
            this.Close();
        }
    }
}
