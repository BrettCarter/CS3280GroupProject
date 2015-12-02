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

namespace Group2_3280_Invoice
{
	public class clsItem
	{
        public String ItemCode { get; set; }
        public String ItemDesc { get; set; }
        public String Cost { get; set; }

        private string sConnectionString;

        public clsItem()
		{
			#region Attributes
			#endregion
			
			#region Constructor
			#endregion
			
			#region Methods
			#endregion
		}

        public override string ToString()
        {
            try
            {
                return "$" + Cost + "  " + ItemDesc;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
	}
}