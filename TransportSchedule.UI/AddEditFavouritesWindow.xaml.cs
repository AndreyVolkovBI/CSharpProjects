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
        private IRepository _repo;

        IJSON js = new JSON();

        public AddEditFavouritesWindow(FavouriteStation station, IRepository repo)
        {
            InitializeComponent();
            _repo = repo;

            FillComboBox();

            if (station != null) // Edit Station
            {
                StationsBox.IsEnabled = false;
                StationsBox.SelectedItem = _repo.Stations.FirstOrDefault(s => s.Name == station.Name).Name;
                Description.Text = _repo.Users[_repo.IndexId].FavouriteStations.FirstOrDefault(r => r.Name == station.Name).Description;

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
                StationsBox.Items.Add(res.Name);
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (StationsBox.SelectedItem.ToString() == "System.Windows.Controls.ComboBoxItem: -")
            {
                MessageBox.Show("A station should be chosen", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            // Edit
            else if (_repo.Users[_repo.IndexId].FavouriteStations.Contains(_repo.Users[_repo.IndexId].FavouriteStations.FirstOrDefault(s => s.Name == StationsBox.SelectedItem.ToString())))
            {
                _repo.Users[_repo.IndexId].FavouriteStations.FirstOrDefault(k => k.Name == StationsBox.SelectedItem.ToString()).Description = Description.Text;
                js.WriteToFile(_repo.Users[_repo.IndexId], _repo.Id);
                DialogResult = true;
            }
            // Add
            else
            {
                // Get index of the Station by its Name
                int index = _repo.Stations.FindIndex(s => string.Equals(s.Name, StationsBox.SelectedItem.ToString(), StringComparison.CurrentCultureIgnoreCase));
                
                if (_repo.Users[_repo.IndexId].FavouriteStations == null)
                {
                    _repo.Users[_repo.IndexId].FavouriteStations = new List<FavouriteStation>();
                }
                
                _repo.Users[_repo.IndexId].FavouriteStations.Add(new FavouriteStation { Id = _repo.Stations[index].Id, Name = _repo.Stations[index].Name, Description = Description.Text });

                js.WriteToFile(_repo.Users[_repo.IndexId], _repo.Users[_repo.IndexId].Id);
                DialogResult = true;
            }
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
