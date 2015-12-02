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

            lblInvoiceNumber.Visibility = Visibility.Hidden;
            txtInvoice.Visibility = Visibility.Hidden;
            selectItem.IsEnabled = false;
            cmdAdd.IsEnabled = false;
            cmdDeleteItem.IsEnabled = false;
            cmdSave.IsEnabled = false;
            cmdEdit.IsEnabled = false;
            cmdDelete.IsEnabled = false;
        }

        private void cmdCreateNew_Click(object sender, RoutedEventArgs e)
        {
            selectItem.ItemsSource = SQLStatements.itemsCollection();
            txtDate.Text = DateTime.Now.ToString();
            selectItem.IsEnabled = true;
            cmdAdd.IsEnabled = true;
            cmdDeleteItem.IsEnabled = true;
            cmdSave.IsEnabled = true;
        }

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            
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

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            if (selectItem.SelectedItem != null)
            {
                int total = 0;
                int itemCost = 0;
                clsItem item = (clsItem)selectItem.SelectedItem;
                listItems.Items.Add(item);
                Int32.TryParse(txtCost.Text, out total);
                Int32.TryParse(item.Cost, out itemCost);
                total += itemCost;
                txtCost.Text = total.ToString();
            }
        }

        private void cmdDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (listItems.SelectedItem != null)
            {
                int total = 0;
                int itemCost = 0;
                clsItem item = (clsItem)listItems.SelectedItem;
                listItems.Items.Remove(item);
                Int32.TryParse(txtCost.Text, out total);
                Int32.TryParse(item.Cost, out itemCost);
                total -= itemCost;
                txtCost.Text = total.ToString();
            }
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (listItems.Items != null)
            {
                ObservableCollection<clsItem> items = new ObservableCollection<clsItem>();
                foreach (clsItem item in listItems.Items)
                {
                    items.Add(item);
                }
                DateTime date = Convert.ToDateTime(txtDate.Text);
                SQLStatements.addInvoice(date, txtCost.Text, items);
            }
        }
    }
}