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
using System.Data;
using System.Reflection;

namespace Group2_3280_Invoice
{
    /// <summary>
    /// Interaction logic for frmSearch.xaml
    /// </summary>
    public partial class frmSearch : Window
    {
        private MainWindow wnd_mainWindow;
        clsDataAccess db;

        /// <summary>
        /// Date of Invoice
        /// </summary>
        string sSelectedDate = "";
        /// <summary>
        /// TotalCharge of Invoice
        /// </summary>
        string sSelectedCharge = "";
        /// <summary>
        /// Invoice Number
        /// </summary>
        string sSelectedInvoice = "";

        public frmSearch(object sender)
        {
            this.InitializeComponent();
            wnd_mainWindow = (MainWindow)sender;

            db = new clsDataAccess();

            //populate drop boxes
            PopulateDrops();

            //Initialize the invoice list
            UpdateList(sSelectedDate, sSelectedCharge, sSelectedInvoice);

        }


        /// <summary>
        /// This method Updates the list on the left side of the form, showing a list of all invoices based on the selections
        /// on the right side dropboxes.
        /// </summary>
        /// <param name="Date">Date</param>
        /// <param name="Charge">TotalCharge</param>
        /// <param name="InvoiceNum">Invoice Number</param>
        public void UpdateList(string Date, string Charge, string InvoiceNum)
        {
            string sSQL;    //Holds an SQL statement
            int iRet = 0;   //Number of return values
            DataSet ds = new DataSet();
            clsInvoice invoice;

            //Empties the list so that duplicates are not shown
            listInvoice.Items.Clear();

            //If the form is just opened, the defaults, show all invoices
            if (drpDate.SelectedIndex == -1 && drpCharge.SelectedIndex == -1 && drpInvoice.SelectedIndex == -1)
            {
                sSQL = "SELECT InvoiceNum, InvoiceDate, TotalCharge " +
                    "FROM Invoices";
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);
            }
            //If only the date is not empty
            else if (Date != "" && Charge == "" && InvoiceNum == "")
            {
                sSQL = "SELECT InvoiceNum, InvoiceDate, TotalCharge " +
                    "FROM Invoices " +
                    "WHERE InvoiceDate = #" + Date + "#";
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);
            }
            //if only the charge
            else if (Charge != "" && Date == "" && InvoiceNum == "")
            {
                sSQL = "SELECT InvoiceNum, InvoiceDate, TotalCharge " +
                    "FROM Invoices " +
                    "WHERE TotalCharge =" + Charge;
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);
            }
            //if only the invoicenum
            else if (InvoiceNum != "" && Charge == "" && Date == "")
            {
                sSQL = "SELECT InvoiceNum, InvoiceDate, TotalCharge " +
                    "FROM Invoices " +
                    "WHERE InvoiceNum =" + InvoiceNum;
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);
            }
            //if the invoicenum and date
            else if (InvoiceNum != "" && Date != "" && Charge == "")
            {
                sSQL = "SELECT InvoiceNum, InvoiceDate, TotalCharge " +
                    "FROM Invoices " +
                    "WHERE InvoiceDate = #" + Date + "#" +
                    " AND InvoiceNum =" + InvoiceNum;
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);
            }
            //if the charge and date
            else if (Charge != "" && Date != "" && InvoiceNum == "")
            {
                sSQL = "SELECT InvoiceNum, InvoiceDate, TotalCharge " +
                    "FROM Invoices " +
                    "WHERE InvoiceDate = #" + Date + "#" +
                    " AND TotalCharge =" + Charge;
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);
            }
            //if the invoicenum and charge
            else if (InvoiceNum != "" && Charge != "" && Date == "")
            {
                sSQL = "SELECT InvoiceNum, InvoiceDate, TotalCharge " +
                    "FROM Invoices " +
                    "WHERE TotalCharge =" + Charge +
                    " AND InvoiceNum =" + InvoiceNum;
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);
            }
            //if everything
            else if (InvoiceNum != "" && Date != "" && Charge != "")
            {
                sSQL = "SELECT InvoiceNum, InvoiceDate, TotalCharge " +
                    "FROM Invoices " +
                    "WHERE InvoiceDate = #" + Date + "#" +
                    " AND TotalCharge =" + Charge +
                    " AND InvoiceNum =" + InvoiceNum;
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);
            }
            //insurance
            else
            {
                sSQL = "SELECT InvoiceNum, InvoiceDate, TotalCharge " +
                    "FROM Invoices";
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);
            }



            //update the table
            for (int i = 0; i < iRet; i++)
            {
                invoice = new clsInvoice();

                //date
                invoice.InvoiceNum = ds.Tables[0].Rows[i][0].ToString();
                //invoice #
                invoice.InvoiceDate = ds.Tables[0].Rows[i]["InvoiceDate"].ToString();
                //charge
                invoice.TotalCharge = ds.Tables[0].Rows[i]["TotalCharge"].ToString();

                listInvoice.Items.Add(invoice);
            }

            //Reset the drop boxes so that a new search can be completed without closing the window
            drpDate.SelectedIndex = -1;
            drpCharge.SelectedIndex = -1;
            drpInvoice.SelectedIndex = -1;

        }

        /// <summary>
        /// This method populates the drops on the right side of the Search Form
        /// This needs to be separate from UpdateList because UpdateList has
        /// limiters, where these always show all items.
        /// </summary>
        public void PopulateDrops()
        {
            string sSQL;    //Holds an SQL statement
            int iRet = 0;   //Number of return values
            DataSet ds = new DataSet();
            clsInvoice invoice;

            sSQL = "SELECT InvoiceNum, InvoiceDate, TotalCharge " +
                "FROM Invoices";
            ds = db.ExecuteSQLStatement(sSQL, ref iRet);


            //update dropboxes
            for (int i = 0; i < iRet; i++)
            {
                invoice = new clsInvoice();

                //Invoice #
                invoice.InvoiceNum = ds.Tables[0].Rows[i][0].ToString();
                //Date
                invoice.InvoiceDate = ds.Tables[0].Rows[i]["InvoiceDate"].ToString();
                //Charge
                invoice.TotalCharge = ds.Tables[0].Rows[i]["TotalCharge"].ToString();

                drpInvoice.Items.Add(invoice.InvoiceNum);
                drpDate.Items.Add(invoice.InvoiceDate);
                drpCharge.Items.Add(invoice.TotalCharge);
            }

        }


        /// <summary>
        /// This button updates the search list based on all drop box selections
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateSearch_Click(object sender, RoutedEventArgs e)
        {
            //Select item, if item is emtpy, set string to empty, otherwise set string to item
            if (drpDate.SelectedIndex == -1)
            {
                sSelectedDate = "";
            }
            else
            {
                sSelectedDate = drpDate.SelectedItem.ToString();
            }
            if (drpCharge.SelectedIndex == -1)
            {
                sSelectedCharge = "";
            }
            else
            {
                sSelectedCharge = drpCharge.SelectedItem.ToString();
            }
            if (drpInvoice.SelectedIndex == -1)
            {
                sSelectedInvoice = "";
            }
            else
            {
                sSelectedInvoice = drpInvoice.SelectedItem.ToString();
            }

            UpdateList(sSelectedDate, sSelectedCharge, sSelectedInvoice);

        }

        /// <summary>
        /// This button will take the selected Invoice and send it back to the main menu to
        /// open up for edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSelectInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (listInvoice.SelectedItem != null)
                {
                    clsInvoice selectedInvoice = (clsInvoice)listInvoice.SelectedItem;
                    wnd_mainWindow.insertSelectedInvoice(selectedInvoice);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}