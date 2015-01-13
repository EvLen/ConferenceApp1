using Conferences.Domain.Persistence;
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
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        
        
        public Home()
        {
            InitializeComponent();
           
            Init();
        }

        public void Init()
        {
            DBHelper.DB.BeginTransaction();
        }

        public void RefreshConferences()
        {
            var conferences = DBHelper.DB.Session.CreateCriteria(typeof(Conference)).List<Conference>();
            lstConferences.Items.Clear();
            foreach (var c in conferences)
            {
                lstConferences.Items.Add(c.Name);
            }
        }

        private void BtnCreateConference_OnClick(object sender, RoutedEventArgs e)
        {
            var conference  = new Conference(TextConferenceName.Text,DateConferenceStartDate.DisplayDate, DateConferenceEndDate.DisplayDate);
            DBHelper.DB.Save(conference);
            RefreshConferences();
        }
    }
}
