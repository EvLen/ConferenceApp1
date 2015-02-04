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
    /// Interaction logic for EditRoom.xaml
    /// </summary>
    public partial class EditRoom : UserControl
    {
        public Conference Model { get; set; }
        public EditRoom(int roomId)
        {
            InitializeComponent();
            var room = DBHelper.DB.Get<Room>(roomId);
            Heading.Text = room.Name;
            //ConferenceNameHeader.Text = room.Conference;
            //TextCapacity.Text = room.Capacity;
        }
    }
}
