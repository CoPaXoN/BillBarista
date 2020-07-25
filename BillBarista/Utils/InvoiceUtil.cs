using BillBarista.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace BillBarista
{
    public static class InvoiceUtil
    {
        public static bool CheckSupplierNumber(Excel.Range range, int row)
        {
            //Check suppllier number not empty
            int column = ColumnsInvoice.SupplierNumber;
            if (!ErrorUtil.IsEmptyCell(range, row, column))
            {
                return true;
            }
            return false;

        }

        public static bool CheckDate(Excel.Range range, int row)
        {
            int column = ColumnsInvoice.Date;
            if(!ErrorUtil.IsEmptyCell(range, row, column))
            {
                return true;
            }
            return false;
        }

        

        public static bool CheckTaxAmount(Excel.Range range, int row)
        {
            //Check Tax Amount not empty
            Error error = null;
            int column = ColumnsInvoice.TaxAmount;

            if (ExcelUtil.GetStringValue(range, row, column) != "")
            {
                //check Tax Amount is positive
                if (range.Cells[row, column].Value2 < 0)
                {
                    error = new Error();
                    error.Issue = "שדה לא חיובי";
                    error.CurrentValue = range.Cells[row, column].Value2.ToString();
                    ErrorUtil.FinallizeErrorAndAdd(error, row, column, range);
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}
