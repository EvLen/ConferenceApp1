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
using FirstFloor.ModernUI.Presentation;

namespace Conferences.UI.Pages
{
    /// <summary>
    /// Interaction logic for Conferences.xaml
    /// </summary>
    public partial class Conferences : UserControl
    {
        public Conferences()
        {
            InitializeComponent();
            RefreshConferences();
        }

       

        public void RefreshConferences()
        {
            var conferences = DBHelper.DB.Session.CreateCriteria(typeof(Conference)).List<Conference>();
            ConferencesList.Links.Clear();
            foreach (var c in conferences)
                ConferencesList.Links.Add(new Link() { DisplayName = c.Name, Source = new Uri("/" + c.Id, UriKind.Relative) });

            ConferencesList.Links.Add(new Link() { DisplayName = "Create New", Source = new Uri("/0", UriKind.Relative) });
        }
    }
}
