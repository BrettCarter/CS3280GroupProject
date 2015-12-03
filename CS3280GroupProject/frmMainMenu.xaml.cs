using CS3280GroupProject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
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
        // Initializes the other windows and the class that holds the SQL statements
        private frmUpdate updateWindow;
        private frmSearch searchWindow;
        private clsSQLStatements SQLStatements;

        public MainWindow()
		{
			this.InitializeComponent();
            //MAKE SURE TO INCLUDE THIS LINE OR THE APPLICATION WILL NOT CLOSE
            //BECAUSE THE WINDOWS ARE STILL IN MEMORY
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

            //ResourceDictionary skin = new ResourceDictionary();
            //skin.Source = new Uri(@"ExpressionDark.xaml", UriKind.Absolute);
            //App.Current.Resources.MergedDictionaries.Add(skin);

            try
            {
                SQLStatements = new clsSQLStatements();

                infoLabel.Content = "";
                lblInvoiceNumber.Visibility = Visibility.Hidden;
                txtInvoice.Visibility = Visibility.Hidden;
                selectItem.IsEnabled = false;
                cmdAdd.IsEnabled = false;
                cmdDeleteItem.IsEnabled = false;
                cmdSave.IsEnabled = false;
                cmdEdit.IsEnabled = false;
                cmdDelete.IsEnabled = false;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Handles the Create New Invoice button being clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdCreateNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                infoLabel.Content = "";
                txtInvoice.Text = "";
                lblInvoiceNumber.Visibility = Visibility.Hidden;
                txtInvoice.Visibility = Visibility.Hidden;

                listItems.Items.Clear();
                selectItem.Items.Clear();
                ObservableCollection<clsItem> col_Items = new ObservableCollection<clsItem>();
                col_Items = SQLStatements.itemsCollection();
                foreach (clsItem item in col_Items)
                {
                    selectItem.Items.Add(item);
                }

                txtDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                selectItem.IsEnabled = true;
                cmdAdd.IsEnabled = true;
                cmdDeleteItem.IsEnabled = true;
                cmdSave.IsEnabled = true;
                txtCost.Text = "0";
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Handles the edit button being clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                infoLabel.Content = "";
                selectItem.Items.Clear();
                ObservableCollection<clsItem> col_Items = new ObservableCollection<clsItem>();
                col_Items = SQLStatements.itemsCollection();
                foreach (clsItem item in col_Items)
                {
                    selectItem.Items.Add(item);
                }

                selectItem.IsEnabled = true;
                cmdDeleteItem.IsEnabled = true;
                cmdAdd.IsEnabled = true;
                cmdSave.IsEnabled = true;
                cmdCreateNew.IsEnabled = false;
                cmdEdit.IsEnabled = false;
                cmdDelete.IsEnabled = false;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Handles the delete button being clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SQLStatements.deleteInvoice(txtInvoice.Text);
                infoLabel.Content = "Invoice Deleted";
                txtInvoice.Text = "";
                lblInvoiceNumber.Visibility = Visibility.Hidden;
                txtInvoice.Visibility = Visibility.Hidden;
                cmdEdit.IsEnabled = false;
                cmdDelete.IsEnabled = false;
                txtDate.Text = "";
                txtCost.Text = "0";
                listItems.Items.Clear();
                selectItem.IsEnabled = false;
                selectItem.Items.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Handles the Jewelry menu option being clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                infoLabel.Content = "";
                updateWindow = new frmUpdate(this);
                updateWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Handles the search menu option being clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                infoLabel.Content = "";
                searchWindow = new frmSearch(this);
                searchWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Handles the add item button being clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Handles the delete item button being clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (listItems.SelectedItem != null)
                {
                    int total = 0;
                    int itemCost = 0;
                    clsItem item = (clsItem)listItems.SelectedItem;
                    if (listItems.Items.Count == 1)
                    {
                        listItems.Items.Clear();
                    }
                    else
                    {
                        listItems.Items.Remove(item);
                    }
                    Int32.TryParse(txtCost.Text, out total);
                    Int32.TryParse(item.Cost, out itemCost);
                    total -= itemCost;
                    txtCost.Text = total.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Handles the save invoice button being clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (listItems.Items != null)
                {
                    ObservableCollection<clsItem> items = new ObservableCollection<clsItem>();
                    foreach (clsItem item in listItems.Items)
                    {
                        items.Add(item);
                    }

                    if (txtInvoice.Text == "")
                    {
                        SQLStatements.addInvoice(txtDate.Text, txtCost.Text, items);
                        cmdEdit.IsEnabled = false;
                        cmdDelete.IsEnabled = false;
                        infoLabel.Content = "Invoice Saved";
                    }
                    else
                    {
                        SQLStatements.updateInvoice(txtInvoice.Text, txtCost.Text, items);
                        cmdEdit.IsEnabled = true;
                        cmdDelete.IsEnabled = true;
                        infoLabel.Content = "Invoice Updated";
                    }
                    cmdAdd.IsEnabled = false;
                    cmdDeleteItem.IsEnabled = false;
                    cmdSave.IsEnabled = false;
                    cmdCreateNew.IsEnabled = true;
                    selectItem.Items.Clear();
                    selectItem.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Recieves the invoice that was selected in the search window and fills out the window with the invoice information
        /// </summary>
        /// <param name="invoice"></param>
        public void insertSelectedInvoice(clsInvoice invoice)
        {
            try
            {
                lblInvoiceNumber.Visibility = Visibility.Visible;
                txtInvoice.Visibility = Visibility.Visible;
                txtInvoice.Text = invoice.InvoiceNum;
                txtDate.Text = invoice.InvoiceDate.ToString();
                txtCost.Text = invoice.TotalCharge;
                selectItem.Items.Clear();
                selectItem.IsEnabled = false;
                cmdAdd.IsEnabled = false;
                cmdDeleteItem.IsEnabled = false;
                cmdSave.IsEnabled = false;
                cmdEdit.IsEnabled = true;
                cmdDelete.IsEnabled = true;
                listItems.Items.Clear();
                ObservableCollection<clsItem> col_Items = new ObservableCollection<clsItem>();
                col_Items = SQLStatements.invoiceItems(txtInvoice.Text);
                foreach (clsItem item in col_Items)
                {
                    listItems.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}