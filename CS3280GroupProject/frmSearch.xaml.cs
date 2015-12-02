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

namespace Group2_3280_Invoice
{
	/// <summary>
	/// Interaction logic for frmSearch.xaml
	/// </summary>
	public partial class frmSearch : Window
	{
        private MainWindow wnd_mainWindow;
        clsDataAccess db;

        string sSelectedDate = "";
        string sSelectedCharge = "";
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
        
        public void UpdateList(string Date, string Charge, string InvoiceNum)
        {
            string sSQL;    //Holds an SQL statement
            int iRet = 0;   //Number of return values
            DataSet ds = new DataSet();
            clsInvoice invoice;

            //If the form is just opened, the defaults, show all invoices
            if (drpDate.SelectedIndex == -1 && drpCharge.SelectedIndex == -1 && drpInvoice.SelectedIndex == -1)
            {
                sSQL = "SELECT InvoiceNum, InvoiceDate, TotalCharge " +
                    "FROM Invoices";
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);
            }
                //If only the date
            else if (Date != "")
            {
                sSQL = "SELECT InvoiceNum, InvoiceDate, TotalCharge " +
                    "FROM Invoices" +
                    "WHERE InvoiceDate =" + Date;
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);
            }
                //if only the charge
            else if (Charge != "")
            {
                sSQL = "SELECT InvoiceNum, InvoiceDate, TotalCharge " +
                    "FROM Invoices" +
                    "WHERE TotalCharge =" + Charge;
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);
            }
                //if only the invoicenum
            else if (InvoiceNum != "")
            {
                sSQL = "SELECT InvoiceNum, InvoiceDate, TotalCharge " +
                    "FROM Invoices" +
                    "WHERE InvoiceNum =" + InvoiceNum;
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);
            }
                //if the invoicenum and date
            else if (InvoiceNum != "" && Date != "")
            {
                sSQL = "SELECT InvoiceNum, InvoiceDate, TotalCharge " +
                    "FROM Invoices" +
                    "WHERE InvoiceDate =" + Date +
                    "AND InvoiceNum =" + InvoiceNum;
            }
                //if the charge and date
            else if (Charge != "" && Date != "")
            {
                sSQL = "SELECT InvoiceNum, InvoiceDate, TotalCharge " +
                    "FROM Invoices" +
                    "WHERE InvoiceDate =" + Date +
                    "AND TotalCharge =" + Charge;
            }
                //if the invoicenum and charge
            else if (InvoiceNum != "" && Charge != "")
            {
                sSQL = "SELECT InvoiceNum, InvoiceDate, TotalCharge " +
                    "FROM Invoices" +
                    "WHERE Charge =" + Charge +
                    "AND InvoiceNum =" + InvoiceNum;
            }
                //if everything
            else if (InvoiceNum != "" && Date != "" && Charge != "")
            {
                sSQL = "SELECT InvoiceNum, InvoiceDate, TotalCharge " +
                    "FROM Invoices" +
                    "WHERE InvoiceDate =" + Date +
                    "AND InvoiceNum =" + InvoiceNum +
                    "AND Charge =" + Charge;
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
        }

        //This populates the drops, this needs to be separate from Update List, because
        //update list will only show information based on these selections
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

        //IF THE SELECT INVOICE BUTTON IS PRESSED

        //IF THE UPDATE SEARCH BUTTON IS PRESSED
        //Check to ensure boxes aren't -1, if they are, set respective strings to ""
        //Otherwise, set respective strings to selected combobox items
        
	}
}