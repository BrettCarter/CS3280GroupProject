﻿using System;
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
    public class clsInvoice
    {
        public String InvoiceNum { get; set; }
        public String InvoiceDate { get; set; }
        public String TotalCharge { get; set; }

        public clsInvoice()
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
                return InvoiceNum + "    " + InvoiceDate + "     $" + TotalCharge;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}