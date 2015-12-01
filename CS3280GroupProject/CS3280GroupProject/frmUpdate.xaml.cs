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

        private void cmdAddItem_Click(object sender, RoutedEventArgs e)
        {
            boxListItem.Items.Clear();
            string sNewName = txtName.Text;
            string sNewDesc = txtDesc.Text;
            string sNewCost = txtCost.Text;

            string sSQL = "INSERT INTO ItemDesc( ItemCode, ItemDesc, Cost) VALUES('" + sNewName + "','" + sNewDesc + "','" + sNewCost + "')";
            db.ExecuteNonQuery(sSQL);
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
	}
}