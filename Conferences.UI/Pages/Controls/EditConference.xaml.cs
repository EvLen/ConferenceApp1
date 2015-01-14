using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Conferences.Domain;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;


namespace Conferences.UI.Pages.Controls
{
    /// <summary>
    /// Interaction logic for EditConference.xaml
    /// </summary>
    public partial class EditConference : UserControl
    {
        public Conference Model { get; set; }
        public ModernWindow LastPopup { get; set; }
        public EditConference(int id)
        {
            InitializeComponent();
            Reload(id);
            MessagingCenter.Subscribe<object>(this, Messages.ConferenceEdited, (sender) =>
            {
                if(LastPopup != null) LastPopup.Close();
                Reload();
            });
           
        }

        private void BtnSaveConference_OnClick(object sender, RoutedEventArgs e)
        {
            var isNew = Model.Id == 0;
            if (isNew) Model = new Conference(TextConferenceName.Text, DateConferenceStartDate.SelectedDate.Value, DateConferenceEndDate.SelectedDate.Value);
            else Model.UpdateBasicInfo(TextConferenceName.Text, DateConferenceStartDate.SelectedDate.Value, DateConferenceEndDate.SelectedDate.Value);
            DBHelper.DB.Save(Model);
            var result = ModernDialog.ShowMessage("Conference Save", "Message", MessageBoxButton.OK);
            MessagingCenter.Send<object>(this, Messages.ConferencesUpdated);
            if(isNew) Model = new Conference();
            BindPage();
        }

        public void Reload(int id = -1)
        {
            if (id == -1) id = Model.Id;
            Model = (id != 0) ? DBHelper.DB.Get<Conference>(id) : new Conference();
            BindPage();
        }

        public void BindPage()
        {
            TextConferenceName.Text = Model.Name;
            DateConferenceStartDate.SelectedDate = Model.StartDate;
            DateConferenceEndDate.SelectedDate = Model.EndDate;
            Title.Text = Model.Id > 0 ? "Edit Conference" : "Create Conference";
            if (Model.Id > 0)
            {
                EditPanel.Visibility = Visibility.Visible;
                Rooms.Items.Clear();
                foreach (var room in Model.Rooms)
                    Rooms.Items.Add(room.Name);
            }
            else EditPanel.Visibility = Visibility.Hidden;
        }


        private void BtnCreateRoom_OnClick(object sender, RoutedEventArgs e)
        {
            var wnd = new ModernWindow
            {
                Style = (Style)App.Current.Resources["BlankWindow"],
                Title = "Create Room",
                IsTitleVisible = true,
                Content = new CreateRoom(Model),
                Width = 175,
                Height = 100,
                ResizeMode = ResizeMode.NoResize
            };
            wnd.Show();
            LastPopup = wnd;
        }

        private void BtnCreateDay_OnClick(object sender, RoutedEventArgs e)
        {
            
            var bbBlock = new BBCodeBlock();
            bbBlock.LinkNavigator.Navigate(new Uri("/Users?", UriKind.Relative), this, NavigationHelper.FrameParent);
        }

        private void Rooms_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var room = Model.Rooms.SingleOrDefault(x => x.Name == Rooms.SelectedItem.ToString());
            var bbBlock = new BBCodeBlock();
            bbBlock.LinkNavigator.Navigate(new Uri("/rooms?" + room.Id, UriKind.Relative), this, NavigationHelper.FrameSelf);
        }

      
    }
}
