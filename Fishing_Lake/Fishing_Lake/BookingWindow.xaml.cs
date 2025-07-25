// BookingWindow.xaml.cs – Interface for entering customer name, phone, date & price
using FishingLake.DAL.Models;
using FishingLake.DAL.Repositories;
using FishingLake.Services;
using System;
using System.Windows;

namespace Fishing_Lake
{
    public partial class BookingWindow : Window
    {
        private readonly Pond _pond;
        private readonly BookingService _bookingService;

        public BookingWindow(Pond pond)
        {
            InitializeComponent();
            _pond = pond;
            _bookingService = new BookingService(new BookingRepository());

            dpBookingDate.SelectedDate = DateTime.Today;
            dpBookingDate.DisplayDateStart = DateTime.Today;
        }

        private void ConfirmBooking_Click(object sender, RoutedEventArgs e)
        {
            string customerName = txtCustomerName.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string note = txtNote.Text.Trim();

            if (!dpBookingDate.SelectedDate.HasValue)
            {
                MessageBox.Show("Please select a booking date.", "Missing Date", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrice.Text.Trim(), out decimal price) || price < 0)
            {
                MessageBox.Show("Invalid price. Please enter a valid number.", "Invalid Price", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(customerName) || string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Please fill in all required customer information.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(phone, "^0[0-9]{8,10}$"))
            {
                MessageBox.Show("Invalid phone number. Please use a valid Vietnamese format (e.g., 0912345678).", "Invalid Phone", MessageBoxButton.OK, MessageBoxImage.Warning);
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

            MessageBox.Show(result, "Booking Result", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
