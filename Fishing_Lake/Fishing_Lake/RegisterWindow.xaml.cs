using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
                MessageBox.Show(error, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show("Đăng ký thành công! Hãy đăng nhập để tiếp tục.", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close(); // Đóng cửa sổ đăng ký, quay lại login
        }
    }

}
