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
using TransportSchedule.Classes;
using TransportSchedule.Classes.RegisteredUser;

namespace TransportSchedule.UI
{
    /// <summary>
    /// Логика взаимодействия для AddEditFavouritesWindow.xaml
    /// </summary>
    public partial class AddEditFavouritesWindow : Window
    {
        // change 'DatabaseRepository' to 'FileRepository' in case you want to work with JSON files
        // changes should be applyed on every single page in the following initialization:
        IRepository _repo = Factory.Default.GetRepository<DatabaseRepository>();

        public AddEditFavouritesWindow(FavouriteStation station)
        {
            InitializeComponent();
            FillComboBox();

            if (station != null) // Edit Station
            {
                StationsBox.IsEnabled = false;
                FavouriteStation fs = _repo.GetUser().FavouriteStations.FirstOrDefault(s => s.Station.Id == station.Station.Id);
                Station st = _repo.Stations.FirstOrDefault(x => x.Id == fs.Station.Id);

                StationsBox.SelectedItem = st;
                Description.Text = fs.Description;

                Title = "Edit Station"; 
            }
            else // Add station
            {
                Title = "Add Station";
            }
        }

        public void FillComboBox()
        {
            foreach (var res in _repo.Stations)
                StationsBox.Items.Add(res);
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (StationsBox.SelectedItem.ToString() == "System.Windows.Controls.ComboBoxItem: -")
            {
                MessageBox.Show("A station should be chosen", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            // Edit
            else if (_repo.GetUser().FavouriteStations.FirstOrDefault(s => s.Station.Id == (StationsBox.SelectedItem as Station).Id) != null)
            {
                _repo.EditStation(_repo.GetUser(), StationsBox.SelectedItem as Station, Description.Text);
                DialogResult = true;
            }
            // Add
            else
            {
                // Get index of the Station by its Name
                int index = _repo.Stations.ToList().FindIndex(s => string.Equals(s.Name, StationsBox.SelectedItem.ToString(), StringComparison.CurrentCultureIgnoreCase));
                _repo.AddStation(_repo.GetUser(), StationsBox.SelectedItem as Station, Description.Text);

                DialogResult = true;
            }
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
