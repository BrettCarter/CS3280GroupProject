using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Group2_3280_Invoice
{
	/// <summary>
	/// Interaction logic for frmSearch.xaml
	/// </summary>
	public partial class frmSearch : Window
	{
        private MainWindow wnd_mainWindow;

        public frmSearch(object sender)
		{
            this.InitializeComponent();
            wnd_mainWindow = (MainWindow)sender;

            // Insert code required on object creation below this point.
        }
	}
}