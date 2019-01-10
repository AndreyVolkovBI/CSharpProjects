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
    /// Логика взаимодействия для CreateNewAccountPage.xaml
    /// </summary>
    public partial class CreateNewAccountPage : Page
    {
        private IRepository _repo;

        IUserLogic ul = new UserLogic();

        public CreateNewAccountPage(IRepository repo)
        {
            InitializeComponent();
            _repo = repo;
            FullName.Focus();
        }

        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(FullName.Text) && !string.IsNullOrWhiteSpace(Email.Text) && !string.IsNullOrWhiteSpace(Password.Text))
            {               
                if (ul.IsUserExist(Email.Text) == null)
                {
                    User user = ul.CreateNewUser(FullName.Text, Email.Text, Password.Text);
                    _repo.Users.Add(user); // add new user to List<User> Users
                    NavigationService.Navigate(new LogInPage(_repo));
                } else
                    MessageBox.Show("This user has already exist. Please, choose another email.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Please, fill all the gaps.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TransitionToLogInPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LogInPage(_repo));
        }
    }
}
