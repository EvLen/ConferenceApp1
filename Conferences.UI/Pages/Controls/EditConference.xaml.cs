using System.Windows;
using System.Windows.Controls;
using Conferences.Domain;
using FirstFloor.ModernUI.Windows.Controls;


namespace Conferences.UI.Pages.Controls
{
    /// <summary>
    /// Interaction logic for EditConference.xaml
    /// </summary>
    public partial class EditConference : UserControl
    {
        public Conference Model { get; set; }
        public EditConference(int id)
        {
            InitializeComponent();
            Model = (id != 0) ? DBHelper.DB.Get<Conference>(id) : new Conference();
           
            BindPage();
        }

        private void BtnSaveConference_OnClick(object sender, RoutedEventArgs e)
        {
            if (Model.Id == 0) Model = new Conference(TextConferenceName.Text, DateConferenceStartDate.SelectedDate.Value, DateConferenceEndDate.SelectedDate.Value);
            else Model.UpdateBasicInfo(TextConferenceName.Text, DateConferenceStartDate.SelectedDate.Value, DateConferenceEndDate.SelectedDate.Value);
            DBHelper.DB.Save(Model);
            ModernDialog.ShowMessage("Conference Save", "Message", MessageBoxButton.OK);
            MessagingCenter.Send<object>(this, Messages.ConferenceEditied);
        }

        public void BindPage()
        {
            TextConferenceName.Text = Model.Name;
            DateConferenceStartDate.SelectedDate = Model.StartDate;
            DateConferenceEndDate.SelectedDate = Model.EndDate;
            Title.Text = Model.Id > 0 ? "Edit Conference" : "Create Conference";
        }

        
    }
}
