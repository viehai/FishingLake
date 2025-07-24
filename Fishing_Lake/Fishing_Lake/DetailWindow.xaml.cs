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
        private User _currentUser;
        private List<PondFish> pondFishList = new();
        private List<FishSpecies> fishSpecies = new();

        private PondService _pondService = new();
        private bool isEditMode = false;
        private Pond? editingPond;

        public DetailWindow(User currentUser) // Create new pond
        {
            InitializeComponent();
            Loaded += Window_Loaded;
            _currentUser = currentUser;
        }

        public DetailWindow(Pond selectedPond, User currentUser) : this(currentUser)
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
            

            pondFishList = pond.PondFishes.ToList();
            dgFishList.ItemsSource = pondFishList;
        }

        private bool ValidatePond()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Pond name cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                MessageBox.Show("Location cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (!int.TryParse(txtCapacity.Text, out int capacity) || capacity <= 0)
            {
                MessageBox.Show("Capacity must be a positive integer.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        private void SavePond_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidatePond())
                return;

            string name = txtName.Text.Trim();
            string location = txtLocation.Text.Trim();
            string description = txtDescription.Text.Trim();
            int capacity = int.Parse(txtCapacity.Text);

            Pond pond;
            if (isEditMode)
            {
                pond = editingPond;
            }
            else
            {
                pond = new Pond();
                pond.OwnerId = _currentUser.Id; // Assign correct OwnerId when creating
            }

            pond.Name = name;
            pond.Location = location;
            pond.Description = description;
            pond.Capacity = capacity;
            pond.PondFishes = pondFishList;

            if (isEditMode)
            {
                _pondService.UpdatePond(pond);
                MessageBox.Show("Pond updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _pondService.AddPond(pond);
                MessageBox.Show("Pond created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
