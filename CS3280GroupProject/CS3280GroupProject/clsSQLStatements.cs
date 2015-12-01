using Group2_3280_Invoice;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3280GroupProject
{
    class clsSQLStatements
    {
        clsDataAccess db;

        public ObservableCollection<clsItem> itemsCollection()
        {
            clsDataAccess db;
            ObservableCollection<clsItem> col_Items;
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
            return col_Items;
        }
    }
}
