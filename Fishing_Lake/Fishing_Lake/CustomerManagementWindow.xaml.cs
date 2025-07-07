// CustomerManagementWindow.xaml.cs - xử lý UI danh sách khách hàng

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FishingLake.BLL.Services;
using FishingLake.Services;
using FishingLake.DAL.Models;

namespace Fishing_Lake
{
    public partial class CustomerManagementWindow : Window
    {
        private readonly CustomerService _customerService;
        private readonly User _currentUser;

        public CustomerManagementWindow(User currentUser)
        {
            InitializeComponent();
            _customerService = new CustomerService();
            _currentUser = currentUser;
            LoadCustomers();
        }

        private void LoadCustomers(string search = "")
        {
            var customers = _customerService.GetCustomersByOwner(_currentUser.Id, search)
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                    c.Phone,
                    IsVipText = c.IsVip == true ? "Yes" : "No",
                    c.TotalBookings
                }).ToList();

            CustomerListView.ItemsSource = customers;
        }


        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadCustomers(SearchBox.Text);
        }


    }
}
