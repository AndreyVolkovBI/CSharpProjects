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
        // change 'DatabaseRepository' to 'FileRepository' in case you want to work with JSON files
        // changes should be applyed on every single page in the following initialization:
        IRepository _repo = Factory.Default.GetRepository<DatabaseRepository>();

        public CreateNewAccountPage()
        {
            InitializeComponent();
            FullName.Focus();
        }

        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(FullName.Text) && !string.IsNullOrWhiteSpace(Email.Text) && !string.IsNullOrWhiteSpace(Password.Text))
            {               
                if (_repo.SingleEmail(Email.Text))
                {
                    _repo.CreateNewUser(FullName.Text, Email.Text, Password.Text);
                    NavigationService.Navigate(new LogInPage());
                } else
                    MessageBox.Show("This user is already exist. Please, choose another email.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Please, fill all the gaps.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TransitionToLogInPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LogInPage());
        }
    }
}
