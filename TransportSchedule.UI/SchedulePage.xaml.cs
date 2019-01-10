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
    /// Логика взаимодействия для SchedulePage.xaml
    /// </summary>
    public partial class SchedulePage : Page
    {
        private IRepository _repo;

        public SchedulePage(IRepository repo, FavouriteStation SelectedStation)
        {
            _repo = repo;
            InitializeComponent();
            if (SelectedStation != null)
            {
                StationFromFavourites.SelectedItem = SelectedStation.Name;
            }
        }

        private void Favourites_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FavouritePage(_repo));
        }

        // Loading all the components
        private void DataGridStations_Loaded(object sender, RoutedEventArgs e, List<ScheduleItem> result)
        {
            DataGridStations.ItemsSource = result.OrderBy(r => r.MinutesLeft);
        }

        private void DataGridUpdate(List<ScheduleItem> result)
        {
            DataGridStations.ItemsSource = null;
            DataGridStations.ItemsSource = result.OrderBy(r => r.MinutesLeft);

        }

        private void Station_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var item in _repo.Stations)
                Station.Items.Add(item.Name);
        }

        private void StationFromFavourites_Loaded(object sender, RoutedEventArgs e)
        {
            if (_repo.Users[_repo.IndexId].FavouriteStations != null)
            {
                foreach (var item in _repo.Users[_repo.IndexId].FavouriteStations)
                    StationFromFavourites.Items.Add(_repo.Stations[item.Id - 1].Name);
            }
        }

        // Change the rusult
        private void Station_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Station.SelectedIndex != 0)
            {
                List<ScheduleItem> result = _repo.Result(Station.SelectedItem.ToString());
                if (result.Count == 0)
                    MessageBox.Show("There is no route that contains such station", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                DataGridUpdate(result);

                if (Station.SelectedItem.ToString() != "System.Windows.Controls.ComboBoxItem: -")
                    StationFromFavourites.SelectedIndex = 0;
            }

        }

        private void StationFromFavourites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StationFromFavourites.SelectedIndex != 0)
            {
                List<ScheduleItem> result = _repo.Result(StationFromFavourites.SelectedItem.ToString());
                if (result.Count == 0)
                    MessageBox.Show("There is no route that contains such station", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                DataGridUpdate(result);

                if (StationFromFavourites.SelectedItem.ToString() != "System.Windows.Controls.ComboBoxItem: -")
                    Station.SelectedIndex = 0;
            }

        }

        private void TransitionToLogInPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LogInPage(_repo));
        }
    }
}
