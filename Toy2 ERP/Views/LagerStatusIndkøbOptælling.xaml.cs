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
using Toy2_ERP.ViewModels;

namespace Toy2_ERP.Views
{
	/// <summary>
	/// Interaction logic for LagerStatusIndkøbOptælling.xaml
	/// </summary>
	public partial class LagerStatusIndkøbOptælling : UserControl
    {
        public LagerStatusIndkøbOptælling()
        {
            InitializeComponent();
            DataContext = new Connectors();
        }
    }
}
