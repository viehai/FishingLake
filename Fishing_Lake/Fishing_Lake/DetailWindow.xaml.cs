using FishingLake.DAL.Models;
using System.Windows;

namespace Fishing_Lake
{
    /// <summary>
    /// Interaction logic for DetailWindow.xaml
    /// </summary>
    public partial class DetailWindow : Window
    {
        public DetailWindow(Pond selectedPond)
        {
            InitializeComponent();
            LoadPondDetails(selectedPond);
        }

        private void LoadPondDetails(Pond pond)
        {
            txtName.Text = pond.Name;
            txtLocation.Text = pond.Location;
            txtDescription.Text = pond.Description;
            txtCapacity.Text = pond.Capacity.ToString();
            txtOwnerId.Text = pond.Owner?.Name ?? "Không rõ";

            dgFishList.ItemsSource = pond.PondFishes.Select(pf => $"{pf.Fish.Name} - {pf.Quantity} con");
        }

        

        private void SavePond_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
