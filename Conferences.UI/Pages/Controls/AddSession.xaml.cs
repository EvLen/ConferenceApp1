﻿using System;
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
    /// Interaction logic for AddSession.xaml
    /// </summary>
    public partial class AddSession : UserControl
    {

        public Room Model { get; set; }
        public AddSession(Room model)
        {
            InitializeComponent();
            Model = model;
            DataContext = Model;
        }

    }
}
