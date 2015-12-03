using Group2_3280_Invoice;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CS3280GroupProject
{
    class clsSQLStatements
    {
        clsDataAccess db; // clsDataAccess object to be used to submit SQL statements to the database
        DataSet ds; // Dataset to hold the returned data from queries

        /// <summary>
        /// Returns the full list of jewelry items from the database
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<clsItem> itemsCollection()
        {
            try
            {
                db = new clsDataAccess();
                ObservableCollection<clsItem> col_Items = new ObservableCollection<clsItem>();
                string sSQL;    //Holds an SQL statement
                int iRet = 0;   //Number of return values
                ds = new DataSet(); //Holds the return values
                clsItem items; //Used to load the return values into the combo box
                sSQL = "SELECT ItemCode, ItemDesc, Cost " + "FROM ItemDesc";

                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                //Creates item objects based on the data pulled from the query than adds the object to a list
                for (int i = 0; i < iRet; i++)
                {
                    items = new clsItem();

                    items.ItemCode = ds.Tables[0].Rows[i][0].ToString();
                    items.ItemDesc = ds.Tables[0].Rows[i]["ItemDesc"].ToString();
                    items.Cost = ds.Tables[0].Rows[i]["Cost"].ToString();

                    col_Items.Add(items);
                }
                return col_Items;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns the full list of invoices from the database 
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<clsInvoice> invoicesCollection()
        {
            try
            {
                string sSQL;    //Holds an SQL statement
                int iRet = 0;   //Number of return values
                ds = new DataSet();
                db = new clsDataAccess();
                ObservableCollection<clsInvoice> col_Invoices = new ObservableCollection<clsInvoice>();
                clsInvoice invoice;

                sSQL = "SELECT InvoiceNum, InvoiceDate, TotalCharge " +
                    "FROM Invoices";
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                for (int i = 0; i < iRet; i++)
                {
                    invoice = new clsInvoice();

                    invoice.InvoiceNum = ds.Tables[0].Rows[i][0].ToString();
                    invoice.InvoiceDate = ds.Tables[0].Rows[i]["InvoiceDate"].ToString();
                    invoice.TotalCharge = ds.Tables[0].Rows[i]["TotalCharge"].ToString();

                    col_Invoices.Add(invoice);
                }
                return col_Invoices;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Adds a new invoice to the database
        /// </summary>
        /// <param name="date"></param>
        /// <param name="totalCharge"></param>
        /// <param name="items"></param>
        public void addInvoice(string date, string totalCharge, ObservableCollection<clsItem> items)
        {
            try
            {
                db = new clsDataAccess();
                int iRet = 0;

                string sSQL = "INSERT INTO Invoices( InvoiceDate, TotalCharge) VALUES(#" +
                    date + "#, " + totalCharge + ")";
                db.ExecuteNonQuery(sSQL);

                string sSQLInvoiceNumber = "SELECT MAX(InvoiceNum) FROM Invoices";

                DataSet newInvoice = db.ExecuteSQLStatement(sSQLInvoiceNumber, ref iRet);
                string invoiceNumber = newInvoice.Tables[0].Rows[0][0].ToString();

                int i = 1;
                foreach (clsItem item in items)
                {
                    string sSQLLineItem = "INSERT INTO LineItems(InvoiceNum, LineItemNum, ItemCode) " +
                        "VALUES('" + invoiceNumber + "', '" + i + "' , '" + item.ItemCode + "')";
                    db.ExecuteNonQuery(sSQLLineItem);
                    i += 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing invoice in the database
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="totalCharge"></param>
        /// <param name="items"></param>
        public void updateInvoice(string invoiceNum, string totalCharge, ObservableCollection<clsItem> items)
        {
            try
            {
                db = new clsDataAccess();

                string sSQL = "UPDATE Invoices " +
                    "SET TotalCharge = '" + totalCharge +
                    "' WHERE InvoiceNum = " + invoiceNum;
                db.ExecuteNonQuery(sSQL);

                sSQL = "Delete FROM LineItems WHERE InvoiceNum = " + invoiceNum;
                db.ExecuteNonQuery(sSQL);

                int i = 1;
                foreach (clsItem item in items)
                {
                    string sSQLLineItem = "INSERT INTO LineItems(InvoiceNum, LineItemNum, ItemCode) " +
                        "VALUES('" + invoiceNum + "', '" + i + "' , '" + item.ItemCode + "')";
                    db.ExecuteNonQuery(sSQLLineItem);
                    i += 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns the collection of items from a certain invoice
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public ObservableCollection<clsItem> invoiceItems(string invoiceNumber)
        {
            try
            {
                db = new clsDataAccess();
                ObservableCollection<clsItem> col_Items = new ObservableCollection<clsItem>();
                List<string> itemCode = new List<string>();
                string sSQL;    //Holds an SQL statement
                int iRet = 0;   //Number of return values
                ds = new DataSet(); //Holds the return values
                clsItem items; //Used to load the return values into the combo box
                sSQL = "SELECT ItemCode FROM LineItems "
                    + "WHERE InvoiceNum = " + invoiceNumber;

                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                // adds all of the different item codes on the invoice to a list
                for (int i = 0; i < iRet; i++)
                {
                    itemCode.Add(ds.Tables[0].Rows[i]["ItemCode"].ToString());
                }

                if (itemCode.Count > 0)
                {
                    foreach (string code in itemCode)
                    {
                        sSQL = "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc "
                            + "WHERE ItemCode = '" + code + "'";

                        ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                        items = new clsItem();

                        items.ItemCode = ds.Tables[0].Rows[0][0].ToString();
                        items.ItemDesc = ds.Tables[0].Rows[0]["ItemDesc"].ToString();
                        items.Cost = ds.Tables[0].Rows[0]["Cost"].ToString();

                        col_Items.Add(items);
                    }
                }
                return col_Items;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Deletes an invoice from the database
        /// </summary>
        /// <param name="invoiceNum"></param>
        public void deleteInvoice(string invoiceNum)
        {
            try
            {
                string sSQL = "Delete FROM LineItems WHERE InvoiceNum = " + invoiceNum;
                db.ExecuteNonQuery(sSQL);
                sSQL = "Delete FROM Invoices WHERE InvoiceNum = " + invoiceNum;
                db.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
