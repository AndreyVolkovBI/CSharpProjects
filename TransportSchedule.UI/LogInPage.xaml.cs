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
        // change 'DatabaseRepository' to 'FileRepository' in case you want to work with JSON files
        // changes should be applyed on every single page in the following initialization:
        IRepository _repo = Factory.Default.GetRepository<DatabaseRepository>();

        public LogInPage()
        {
            InitializeComponent();
            Email.Focus();
        }
        
        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Email.Text) && !string.IsNullOrWhiteSpace(Password.Text))
            {
                if (_repo.IsUserExist(Email.Text, Password.Text) == null)
                    MessageBox.Show("The user is not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    NavigationService.Navigate(new SchedulePage(null));
                }
            }
            else
            {
                MessageBox.Show("Please, fill all the gaps.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateNewAccountPage());
        }
    }
}
