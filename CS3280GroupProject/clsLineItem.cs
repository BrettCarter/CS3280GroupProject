using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group2_3280_Invoice
{
    public class clsLineItem
    {
        /// <summary>
        /// Invoice Number
        /// </summary>
        public String InvoiceNum { get; set; }
        /// <summary>
        /// LineItemNumber
        /// </summary>
        public String LineItemNum { get; set; }
        /// <summary>
        /// ItemCode
        /// </summary>
        public String ItemCode { get; set; }


        /// <summary>
        /// Default Constructor
        /// </summary>
        public clsLineItem()
        {

        }
        

        /// <summary>
        /// Overide ToString()
        /// </summary>
        /// <returns>LineItem Table values : InvoiceNum, LineItemNum, Item Code</returns>
        public override string ToString()
        {
            try
            {
                return InvoiceNum + "    " + LineItemNum + "  " + ItemCode;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
