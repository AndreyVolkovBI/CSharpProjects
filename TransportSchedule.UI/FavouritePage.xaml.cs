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
        // change 'DatabaseRepository' to 'FileRepository' in case you want to work with JSON files
        // changes should be applyed on every single page in the following initialization:
        IRepository _repo = Factory.Default.GetRepository<DatabaseRepository>();

        public FavouritePage()
        {
            InitializeComponent();            
            UpdateStationList();
        }

        private void UpdateStationList()
        {
            FavouriteDataGrid.ItemsSource = null;
            FavouriteDataGrid.ItemsSource = _repo.GetUser().FavouriteStations;
        }

        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SchedulePage(FavouriteDataGrid.SelectedItem as FavouriteStation));
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var stationWindow = new AddEditFavouritesWindow(null);

            if (stationWindow.ShowDialog() == true)
                UpdateStationList();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var selectedStation = FavouriteDataGrid.SelectedItem as FavouriteStation;

            if (selectedStation != null)
            {
                var stationWindow = new AddEditFavouritesWindow(selectedStation);
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
                _repo.DeleteStation(selectedStation);
                UpdateStationList();
            }
            else
            {
                MessageBox.Show("Select an item from the list to delete it", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void TransitionToLogInPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LogInPage());
        }
    }
}