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

namespace Conferences.UI.Pages.Controls
{
    /// <summary>
    /// Interaction logic for AddDay.xaml
    /// </summary>
    public partial class AddDay : UserControl
    {
        public Conference Model {get; set;}
        public AddDay(Conference model)
        {
            InitializeComponent();
            Model = model;
            DataContext = Model;
            Days.ItemsSource = Model.GetAvailableDates().Where(x => !Model.HasDay(x)).Select(d => d.ToString("dd/MM/yyyy"));
        }


        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            var day = Model.AddDay(Days.SelectedItem.ConvertToDateTime(DateTime.Now));
            DBHelper.DB.Save(day);
            DBHelper.DB.Save(Model);
            MessagingCenter.Send<object>(this, Messages.ConferenceEdited);
        }


    }
}
