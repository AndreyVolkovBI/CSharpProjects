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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TransportSchedule.Classes;
using TransportSchedule.Classes.RegisteredUser;

namespace TransportSchedule.UI
{
    /// <summary>
    /// Логика взаимодействия для FavouritePage.xaml
    /// </summary>
    public partial class FavouritePage : Page
    {
        private IRepository _repo;
        IJSON js = new JSON();

        public FavouritePage(IRepository repo)
        {
            InitializeComponent();
            _repo = repo;
            UpdateStationList();
        }

        private void UpdateStationList()
        {
            FavouriteDataGrid.ItemsSource = null;
            FavouriteDataGrid.ItemsSource = _repo.Users[_repo.IndexId].FavouriteStations;
        }

        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SchedulePage(_repo, FavouriteDataGrid.SelectedItem as FavouriteStation));
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var stationWindow = new AddEditFavouritesWindow(null, _repo);

            if (stationWindow.ShowDialog() == true)
            {
                _repo.StationsInListBox = _repo.Users[_repo.IndexId].FavouriteStations;
                UpdateStationList();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var selectedStation = FavouriteDataGrid.SelectedItem as FavouriteStation;

            if (selectedStation != null)
            {
                var stationWindow = new AddEditFavouritesWindow(selectedStation, _repo);
                if (stationWindow.ShowDialog() == true)
                    UpdateStationList();
            }
            else
            {
                MessageBox.Show("Select an item from the list", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            var selectedStation = FavouriteDataGrid.SelectedItem as FavouriteStation;

            if (selectedStation != null)
            {
                _repo.Users[_repo.IndexId].FavouriteStations.Remove(selectedStation);
                js.WriteToFile(_repo.Users[_repo.IndexId], _repo.Id);
                UpdateStationList();
            }
            else
            {
                MessageBox.Show("Select an item from the list to delete it", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void TransitionToLogInPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LogInPage(_repo));
        }
    }
}