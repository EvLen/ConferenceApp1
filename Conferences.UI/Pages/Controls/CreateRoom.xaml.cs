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
using FirstFloor.ModernUI.Windows.Controls;

namespace Conferences.UI.Pages.Controls
{
    /// <summary>
    /// Interaction logic for CreateRoom.xaml
    /// </summary>
    public partial class CreateRoom : UserControl
    {
        public Conference Model { get; set; }
        public CreateRoom(Conference model)
        {
            InitializeComponent();
            Model = model;
            DataContext = Model;
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            Model.AddRoom(TextRoomName.Text, TextCapacity.ConvertToInt32(0));
            DBHelper.DB.Save(Model);
            MessagingCenter.Send<object>(this, Messages.ConferenceEdited);
        }
    }
}
