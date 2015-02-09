using Conferences.Domain;
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

namespace Conferences.UI.Pages.Controls
{
    /// <summary>
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class Users : UserControl
    {
        public UserListViewModel ViewModel { get; set; }


        public Users()
        {
            InitializeComponent();
            ViewModel = new UserListViewModel();
            DataContext = ViewModel;
            Reload();
        }

        public async void Reload()
        {
            var usrs = DBHelper.DB.Session.CreateCriteria(typeof(User)).List<User>();
           // ViewModel.Users = usrs;
            ViewModel.Users.Clear();
            foreach (var usr in usrs)
                ViewModel.Users.Add(usr);
            grid.ItemsSource = ViewModel.Users;
           
        }

        private void ModernButton_Click(object sender, RoutedEventArgs e)
        {
            Reload();
            DataContext = ViewModel;
        }
    }

    public class UserListViewModel
    {
        public IList<User> Users { get; set; }
        public UserListViewModel()
        {
            Users = new List<User>();
        }
    }
}
