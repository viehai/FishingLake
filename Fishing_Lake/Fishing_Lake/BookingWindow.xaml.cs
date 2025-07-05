// BookingWindow.xaml.cs - giao diện nhập tên, sđt, ngày & giá của khách
using System;
using System.Windows;
using FishingLake.DAL.Models;
using FishingLake.Services;

namespace Fishing_Lake
{
    public partial class BookingWindow : Window
    {
        private readonly Pond _pond;
        private readonly BookingService _bookingService = new();

        public BookingWindow(Pond pond)
        {
            InitializeComponent();
            _pond = pond;
            dpBookingDate.SelectedDate = DateTime.Today; // Gán mặc định hôm nay
        }

        private void ConfirmBooking_Click(object sender, RoutedEventArgs e)
        {
            string customerName = txtCustomerName.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string note = txtNote.Text.Trim();

            if (!dpBookingDate.SelectedDate.HasValue)
            {
                MessageBox.Show("Vui lòng chọn ngày đặt hồ.");
                return;
            }

            if (!decimal.TryParse(txtPrice.Text.Trim(), out decimal price) || price < 0)
            {
                MessageBox.Show("Giá tiền không hợp lệ.");
                return;
            }

            if (string.IsNullOrWhiteSpace(customerName) || string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin khách hàng.");
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(phone, @"^0[0-9]{8,10}$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ. Vui lòng nhập đúng định dạng Việt Nam (VD: 0912345678).");
                return;
            }

            string result = _bookingService.BookPondForCustomer(
                _pond.Id,
                customerName,
                phone,
                dpBookingDate.SelectedDate.Value,
                price,
                note,
                DateTime.Now
            );

            MessageBox.Show(result);
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
