using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Conferences.Domain;
using NHibernate;
using NHibernate.Criterion;

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

        public void Reload()
        {
            ICriteria query = DBHelper.DB.Session.CreateCriteria<User>();
            if(!string.IsNullOrWhiteSpace(txtFirstName.Text))
                query.Add(Restrictions.Like("FirstName", txtFirstName.Text));
            if (!string.IsNullOrWhiteSpace(txtSurname.Text))
                query.Add(Restrictions.Like("Surname", txtSurname.Text));
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
                query.Add(Restrictions.Like("Email", txtEmail.Text));
            query.Add(Restrictions.Eq("IsSpeaker", chkSpeaker.IsChecked));

            var users = query.List<User>() as List<User>;
            ViewModel.Users.Clear();
            foreach (var usr in users)
                ViewModel.Users.Add(usr);
        }

        private void Refresh_OnClick(object sender, RoutedEventArgs e)
        {
            Reload();
        }
    }

    public class UserListViewModel
    {
        public ObservableCollection<User> Users { get; set; }
        public UserListViewModel()
        {
            Users = new ObservableCollection<User>();
        }
    }
}
