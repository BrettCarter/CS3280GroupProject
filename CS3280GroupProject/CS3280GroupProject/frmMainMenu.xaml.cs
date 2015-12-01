using CS3280GroupProject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
        private frmNewInvoice newInvoiceWindow;
        private frmUpdate updateWindow;
        private frmSearch searchWindow;
        private clsSQLStatements SQLStatements;

        public MainWindow()
		{
			this.InitializeComponent();
            //MAKE SURE TO INCLUDE THIS LINE OR THE APPLICATION WILL NOT CLOSE
            //BECAUSE THE WINDOWS ARE STILL IN MEMORY
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

            SQLStatements = new clsSQLStatements();

            selectItem.ItemsSource = SQLStatements.itemsCollection();

        }

        private void cmdCreateNew_Click(object sender, RoutedEventArgs e)
        {
            newInvoiceWindow = new frmNewInvoice(this);
            newInvoiceWindow.ShowDialog();
        }

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            newInvoiceWindow = new frmNewInvoice(this);
            newInvoiceWindow.ShowDialog();
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            newInvoiceWindow = new frmNewInvoice(this);
            newInvoiceWindow.ShowDialog();
        }

        private void mnuUpdate_Click(object sender, RoutedEventArgs e)
        {
            updateWindow = new frmUpdate(this);
            updateWindow.ShowDialog();
        }

        private void mnuSearch_Click(object sender, RoutedEventArgs e)
        {
            searchWindow = new frmSearch(this);
            searchWindow.ShowDialog();
        }
    }
}