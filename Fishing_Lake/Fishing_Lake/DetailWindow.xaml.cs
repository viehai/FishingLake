// DetailWindow.xaml.cs
using FishingLake.DAL;
using FishingLake.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Fishing_Lake
{
    public partial class DetailWindow : Window
    {
        private List<PondFish> pondFishList = new();
        private List<FishSpecies> fishSpecies = new();

        private PondService _pondService = new();
        private bool isEditMode = false;
        private Pond? editingPond;

        public DetailWindow() // Tạo mới
        {
            InitializeComponent();
            Loaded += Window_Loaded;
        }

        public DetailWindow(Pond selectedPond) : this() // Xem chi tiết
        {
            isEditMode = true;
            editingPond = selectedPond;
            LoadPondDetails(selectedPond);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using var context = new FishingManagementContext();
            fishSpecies = context.FishSpecies.ToList();
            dgFishList.ItemsSource = pondFishList;

            var comboBoxColumn = dgFishList.Columns[0] as DataGridComboBoxColumn;
            if (comboBoxColumn != null)
            {
                comboBoxColumn.ItemsSource = fishSpecies;
                comboBoxColumn.SelectedValuePath = "Id";
                comboBoxColumn.DisplayMemberPath = "Name";
            }
        }

        private void LoadPondDetails(Pond pond)
        {
            txtName.Text = pond.Name;
            txtLocation.Text = pond.Location;
            txtDescription.Text = pond.Description;
            txtCapacity.Text = pond.Capacity.ToString();
            txtOwnerId.Text = pond.Owner?.Name ?? "Không rõ";

            pondFishList = pond.PondFishes.ToList();
            dgFishList.ItemsSource = pondFishList;
        }

        private void SavePond_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text.Trim();
            string location = txtLocation.Text.Trim();
            string description = txtDescription.Text.Trim();
            int capacity;

            if (!int.TryParse(txtCapacity.Text, out capacity))
            {
                MessageBox.Show("Dung lượng không hợp lệ.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int ownerId = 1; // Có thể đổi thành CurrentUser.Id nếu cần

            string? error = _pondService.AddPondWithFishes(name, location, description, capacity, ownerId, pondFishList);

            if (error != null)
            {
                MessageBox.Show(error, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show("Tạo hồ thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
