// CustomerManagementWindow.xaml.cs - xử lý UI danh sách khách hàng

using FishingLake.BLL.Services;
using FishingLake.DAL.Models;
using FishingLake.DAL.Repositories;
using FishingLake.Services;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Fishing_Lake
{
    public partial class CustomerManagementWindow : Window
    {
        private readonly CustomerService _customerService;
        private readonly User _currentUser;

        public CustomerManagementWindow(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _customerService = new CustomerService(new CustomerRepository());
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
