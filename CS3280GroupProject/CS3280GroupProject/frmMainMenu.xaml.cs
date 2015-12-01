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

        clsDataAccess db;
        ObservableCollection<clsItem> col_Items;

        public MainWindow()
		{
			this.InitializeComponent();

            // Insert code required on object creation below this point.

            ////MAKE SURE TO INCLUDE THIS LINE OR THE APPLICATION WILL NOT CLOSE
            ////BECAUSE THE WINDOWS ARE STILL IN MEMORY
            //Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

            ////Initialize the database class
            //db = new clsDataAccess();

            //string sSQL;    //Holds an SQL statement
            //int iRet = 0;   //Number of return values
            //DataSet ds = new DataSet(); //Holds the return values
            //clsItem item; //Used to load the return values into the combo box

            ////Create the SQL statement to extract the passengers
            //sSQL = "SELECT ItemCode, ItemDesc, Cost " +
            //    "FROM ItemDesc";

            //ds = db.ExecuteSQLStatement(sSQL, ref iRet);

            ////Loop through the data and load it into the combo box
            //for (int i = 0; i < iRet; i++)
            //{
            //    item = new clsItem();
            //    item.ItemCode = ds.Tables[0].Rows[i][0].ToString();
            //    item.ItemDesc = ds.Tables[0].Rows[i]["ItemDesc"].ToString();
            //    item.Cost = ds.Tables[0].Rows[i]["Cost"].ToString();

            //    selectItem.Items.Add(item);
            //}




            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

            db = new clsDataAccess();
            string sSQL;    //Holds an SQL statement
            int iRet = 0;   //Number of return values
            DataSet ds = new DataSet(); //Holds the return values
            clsItem items; //Used to load the return values into the combo box
            sSQL = "SELECT ItemCode, ItemDesc, Cost " + "FROM ItemDesc";
            col_Items = new ObservableCollection<clsItem>();

            ds = db.ExecuteSQLStatement(sSQL, ref iRet);

            //Creates Flight objects based on the data pulled from the query than adds the object to a list
            for (int i = 0; i < iRet; i++)
            {
                items = new clsItem();

                items.ItemCode = ds.Tables[0].Rows[i][0].ToString();
                items.ItemDesc = ds.Tables[0].Rows[i]["ItemDesc"].ToString();
                items.Cost = ds.Tables[0].Rows[i]["Cost"].ToString();

                col_Items.Add(items);
            }
            selectItem.ItemsSource = col_Items;

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