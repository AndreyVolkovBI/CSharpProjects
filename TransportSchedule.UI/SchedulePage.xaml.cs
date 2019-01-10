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
using TransportSchedule.Classes.Calculation;
using TransportSchedule.Classes.RegisteredUser;

namespace TransportSchedule.UI
{
    /// <summary>
    /// Логика взаимодействия для SchedulePage.xaml
    /// </summary>
    public partial class SchedulePage : Page
    {
        // change 'DatabaseRepository' to 'FileRepository' in case you want to work with JSON files
        // changes should be applyed on every single page in the following initialization:
        IRepository _repo = Factory.Default.GetRepository<DatabaseRepository>();

        Main main = new Main();

        public SchedulePage(FavouriteStation SelectedStation)
        {
            _repo.FillRoutesAndStations();
            InitializeComponent();
            if (SelectedStation != null)
                StationFromFavourites.SelectedItem = SelectedStation.Station.Name;
        }

        private void Favourites_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FavouritePage());
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
            if (_repo.GetUser().FavouriteStations != null)
            {
                foreach (var item in _repo.GetUser().FavouriteStations)
                    StationFromFavourites.Items.Add(item.Station.Name);
            }
        }

        // Change the result
        private void Station_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Station.SelectedIndex != 0)
                ShowResult(Station.SelectedItem.ToString());
            if (Station.SelectedItem.ToString() != "System.Windows.Controls.ComboBoxItem: -")
                StationFromFavourites.SelectedIndex = 0;
        }

        private void StationFromFavourites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StationFromFavourites.SelectedIndex != 0)
                ShowResult(StationFromFavourites.SelectedItem.ToString());
            if (StationFromFavourites.SelectedItem.ToString() != "System.Windows.Controls.ComboBoxItem: -")
                Station.SelectedIndex = 0;
        }

        private void ShowResult(string item)
        {
            List<ScheduleItem> result = main.Result(item, _repo.GetRoutes(), _repo.Stations);
            if (result.Count == 0)
                MessageBox.Show("There is no route that contains such station", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
            DataGridUpdate(result);
        }

        private void TransitionToLogInPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LogInPage());
        }
    }
}
