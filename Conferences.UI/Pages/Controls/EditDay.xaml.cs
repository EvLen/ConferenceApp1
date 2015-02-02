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
    /// Interaction logic for EditDay.xaml
    /// </summary>
    public partial class EditDay : UserControl
    {
        public EditDay(int dayId)
        {
            InitializeComponent();
            var days = DBHelper.DB.Get<Day>(dayId);
            
        }
    }
}
