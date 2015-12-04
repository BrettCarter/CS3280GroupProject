using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3280GroupProject
{
    class clsLineItem
    {
        public String InvoiceNum { get; set; }
        public String LineItemNum { get; set; }
        public String ItemCode { get; set; }

        clsLineItem()
        {

        }
        

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
