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
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;

namespace Conferences.UI.Pages.Controls
{
    /// <summary>
    /// Interaction logic for EditRoom.xaml
    /// </summary>
    public partial class EditRoom : UserControl
    {
        public Room Model { get; set; }
        public ModernWindow LastPopup { get; set; }
        public EditRoom(int roomId)
        {
            InitializeComponent();
            var room = DBHelper.DB.Get<Room>(roomId);
            Model = room;
            Heading.Text = room.Name;
            ConferenceNameHeader.Text = room.Conference.Name;
            TextCapacity.Text = room.Capacity.ToString();

            //Reload(id);
            //MessagingCenter.Subscribe<object>(this, Messages.ConferenceEdited, (sender) =>
            //{
            //    if (LastPopup != null) LastPopup.Close();
            //    Reload();
            //});
        }

        //public void Reload(int id = -1)
        //{
        //    if (id == -1) id = Model.Id;
        //    Model = (id != 0) ? DBHelper.DB.Get<Room>(id) : new Room();
            
        //}


        private void BtnCreateSession_OnClick(object sender, RoutedEventArgs e)
        {

            var wnd = new ModernWindow
            {
                Style = (Style)App.Current.Resources["BlankWindow"],
                Title = "Add Session",
                IsTitleVisible = true,
                Content = new AddSession(Model),
                Width = 175,
                Height = 100,
                ResizeMode = ResizeMode.NoResize
            };
            wnd.Show();
            LastPopup = wnd;
        }

        private void BtnRemoveRoom_OnClick(object sender, RoutedEventArgs e)
        {
            var confId = Model.Conference.Id;
            Model.Conference.DeleteRoom(Model);
            DBHelper.DB.Save(Model.Conference);
            DBHelper.DB.Delete<Room>(Model);
            
            MessagingCenter.Send<object>(this, Messages.ConferenceEdited);
            var bbBlock = new BBCodeBlock();
            //bbBlock.LinkNavigator.Navigate(new Uri("/Pages/Controls/AddUser.xaml", UriKind.Relative), this, NavigationHelper.FrameParent);
            bbBlock.LinkNavigator.Navigate(new Uri("/" + confId, UriKind.Relative), this, NavigationHelper.FrameSelf);
        }
    }
    
}
