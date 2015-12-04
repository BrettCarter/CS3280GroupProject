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
using System.Data.OleDb;
using System.IO;
using System.Reflection;

namespace Group2_3280_Invoice
{
    /// <summary>
    /// Interaction logic for frmUpdate.xaml
    /// </summary>
    public partial class frmUpdate : Window
    {
        private MainWindow wnd_mainWindow;
        clsDataAccess db;

        public frmUpdate(object sender)
        {
            this.InitializeComponent();
            wnd_mainWindow = (MainWindow)sender;
            db = new clsDataAccess();

            UpdateBox();

        }

        public void UpdateBox()
        {
            string sSQL;    //Holds an SQL statement
            int iRet = 0;   //Number of return values
            DataSet ds = new DataSet();
            clsItem item;

            sSQL = "SELECT ItemCode, ItemDesc, Cost " +
                "FROM ItemDesc";
            ds = db.ExecuteSQLStatement(sSQL, ref iRet);

            for (int i = 0; i < iRet; i++)
            {
                item = new clsItem();

                item.ItemCode = ds.Tables[0].Rows[i][0].ToString();
                item.ItemDesc = ds.Tables[0].Rows[i]["ItemDesc"].ToString();
                item.Cost = ds.Tables[0].Rows[i]["Cost"].ToString();

                boxListItem.Items.Add(item);
            }
        }

        private void cmdAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //JD added .Trim() to ensure no extra spaces
                string sNewName = txtName.Text.Trim();
                string sNewDesc = txtDesc.Text.Trim();
                string sNewCost = txtCost.Text.Trim();
                int iRet = 0;
                if (sNewName != "" && sNewDesc != "" && sNewCost != "")
                {
                    string sSQLCheck = "SELECT * FROM ItemDesc WHERE ItemCode = '" + sNewName + "'";
                    db.ExecuteSQLStatement(sSQLCheck, ref iRet);

                    if (iRet == 0)
                    {
                        boxListItem.Items.Clear();
                        string sSQL = "INSERT INTO ItemDesc( ItemCode, ItemDesc, Cost) VALUES('" + sNewName + "','" + sNewDesc + "','" + sNewCost + "')";
                        db.ExecuteNonQuery(sSQL);
                        UpdateBox();
                        statusLabel.Content = "Item Added";
                    }
                    else
                    {
                        statusLabel.Content = "This item code already exists";
                    }
                }
                else
                {
                    statusLabel.Content = "Please fill out all of the information";
                }

                
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        private void cmdDeleteItem_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (boxListItem.SelectedItem != null)
                {
                    clsItem currentItem = (clsItem)boxListItem.SelectedItem;
                    String sItemCode = currentItem.ItemCode;

                    int iRet = 0;
                    String sSQLCheck = "SELECT * FROM LineItems WHERE ItemCode = '" + sItemCode + "'";
                    db.ExecuteSQLStatement(sSQLCheck, ref iRet);

                    if (iRet == 0)
                    {
                        String sSQL = "Delete FROM ItemDesc " +
                        "Where ItemCode = '" + sItemCode + "'";

                        db.ExecuteNonQuery(sSQL);
                        boxListItem.Items.Clear();
                        UpdateBox();

                        txtName.Text = "";
                        txtDesc.Text = "";
                        txtCost.Text = "";
                    }
                    else
                    {
                        //DISPLAY THE INVOICES THAT THIS ITEM IS A PART OF
                        DataSet ds = new DataSet();

                        statusLabel.Content = "That item can't be deleted because it is on the following invoices: ";
                        /*
                        //Select the InvoiceNum where the ItemCode = the current item code
                        String sSQL = "SELECT InvoiceNum " +
                        "FROM LineItems " +
                        "WHERE ItemCode =" + sItemCode;
                        
                        ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                        for (int i = 0; i < iRet; i++)
                        {
                            //Get the InvoiceNum and add it on to StatusLabel based on the ItemCode
                            
                            //statusLabel.Content + InvoiceNum
                            statusLabel.Content = statusLabel.Content;
                        }
                        */
                    }
                    
                }
                else
                {
                    statusLabel.Content = "Please select an item to delete";
                }
                
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        private void cmdEditItem_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text != "" && (txtDesc.Text != "" || txtCost.Text != ""))
            {
                if (txtCost.Text != "")
                {
                    String sSQL = "UPDATE ItemDesc SET Cost = '" + txtCost.Text + "' " +
                        "WHERE ItemCode = '" + txtName.Text + "'";
                    db.ExecuteNonQuery(sSQL);
                    boxListItem.Items.Clear();
                    UpdateBox();
                    statusLabel.Content = "Item Edited";
                }

                if (txtDesc.Text != "")
                {
                    String sSQL = "UPDATE ItemDesc SET ItemDesc = '" + txtDesc.Text + "' " +
                        "WHERE ItemCode = '" + txtName.Text + "'";
                    db.ExecuteNonQuery(sSQL);
                    boxListItem.Items.Clear();
                    UpdateBox();
                    statusLabel.Content = "Item Edited";
                }
            }
            else
            {
                statusLabel.Content = "Please fill out the necessary information";
            }
            
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        } 

        private void selectedItemChanged(object sender, SelectionChangedEventArgs e)
        {
            clsItem item = new clsItem();
            item = (clsItem)boxListItem.SelectedItem;
            if (item != null)
            {
                txtName.Text = item.ItemCode;
                txtDesc.Text = item.ItemDesc;
                txtCost.Text = item.Cost;
                statusLabel.Content = "";
            }
        }
    }
}


