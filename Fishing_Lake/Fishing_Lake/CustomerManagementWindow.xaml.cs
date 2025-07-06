// CustomerManagementWindow.xaml.cs - xử lý UI danh sách khách hàng

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FishingLake.BLL.Services;
using FishingLake.Services;

namespace Fishing_Lake
{
    public partial class CustomerManagementWindow : Window
    {
        private readonly CustomerService _customerService;


        public CustomerManagementWindow()
        {
            InitializeComponent();
            _customerService = new CustomerService();
            LoadCustomers();
        }

        private void LoadCustomers(string search = "")
        {
            var customers = _customerService.GetCustomers(search)
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                    c.Phone,
                    IsVipText = c.IsVip == true ? "Có" : "Không",
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
