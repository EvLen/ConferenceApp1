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
using Conferences.Domain;

namespace Conferences.UI.Pages
{
    /// <summary>
    /// Interaction logic for BasicPage1.xaml
    /// </summary>
    public partial class AddUser : UserControl
    {
        public AddUser()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            DBHelper.DB.BeginTransaction();
        }

        public void RefreshUsers()
        {
            var users = DBHelper.DB.Session.CreateCriteria(typeof(User)).List<User>();
            NewUsers.Items.Clear();
            foreach (var u in users)
            {
                NewUsers.Items.Add(u.FirstName);
            }
        }

        private void BtnCreateUser_OnClick(object sender, RoutedEventArgs b)
        {
            var user = new User(FirstName.Text, SurName.Text, Email.Text, Password.Text);
            DBHelper.DB.Save(user);
            RefreshUsers();
        }
    }
}
