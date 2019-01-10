using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для LogInPage.xaml
    /// </summary>
    public partial class LogInPage : Page
    {
        IRepository _repo = new Repository();

        IUserLogic ul = new UserLogic();

        public LogInPage(IRepository repo)
        {
            InitializeComponent();
            Email.Focus();
        }
        
        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Email.Text) && !string.IsNullOrWhiteSpace(Password.Text))
            {
                int id = ul.IsUserExit(Email.Text, Password.Text);
                if (id == 0)
                    MessageBox.Show("The user is not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    _repo.Id = id; // copy user's id to Repository
                    _repo.IndexId = _repo.Users.FindIndex(t => int.Equals(t.Id, _repo.Id)); // Get user's index by its id
                    NavigationService.Navigate(new SchedulePage(_repo, null));
                }
            }
            else
            {
                MessageBox.Show("Please, fill all the gaps.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateNewAccountPage(_repo));
        }
    }
}
