using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace BillBarista
{
    public static class OrderUtil
    {
        
        public static bool CheckReceivedAmount(Excel.Range range, int row)
        {
            //Check Received Amount not empty
            Error error = null;
            int column = ColumnsOrder.RecivedAmount;
            bool result = false;

            if (ErrorUtil.IsEmptyCell(range, row, column))
            {
                return false;
            }
            //check is float
            try
            {
                if (range.Cells[row, column].Value2 is float)
                {
                    result = true;
                }
            }
            catch
            {
                error = new Error();
                error.Issue = "לא מספר עשרוני";
                error.CurrentValue = range.Cells[row, column].Value2.ToString();
                ErrorUtil.FinallizeErrorAndAdd(error, row, column, range);
                result = false;
            }

            //check is positive
            if (range.Cells[row, column].Value2 < 0)
            {
                if (!ErrorUtil.IsEmptyCell(range, row, column))
                {
                    error = new Error();
                    error.Issue = "שדה לא חיובי";
                    error.CurrentValue = range.Cells[row, column].Value2.ToString();
                }
                else
                {
                    error.Issue += Environment.NewLine + "שדה לא חיובי";
                }
                ErrorUtil.FinallizeErrorAndAdd(error, row, column, range);
                return result;
            }

            return false;
            
        }

        public static bool CheckOrderNumber(Excel.Range range, int row)
        {
            //Check order not empty
            Error error = null;
            int column = ColumnsOrder.OrderNumber;
            
            if (!ErrorUtil.IsEmptyCell(range, row, column))
            {
                //if orderNum length is not 8
                if (range.Cells[row, column].Value2.ToString().Length != 8)
                {
                    error = new Error();
                    error.Issue = "שדה לא מכיל 8 תווים";
                    error.CurrentValue = range.Cells[row, column].Value2.ToString();
                    ErrorUtil.FinallizeErrorAndAdd(error, row, column, range);
                    return false;
                }
                return true;
            }
            return false;
        }

        public static bool CheckOrderDesc(Excel.Range range, int row)
        {
            //Check orderDesc not empty
            Error error = null;
            int column = ColumnsOrder.OrderDesc;
            bool result = false;
            if (!ErrorUtil.IsEmptyCell(range, row, column))
            {
                if (!checkIfContainsExactDigitNumber(range.Cells[row, column].Value2.ToString(), 3))
                {
                    error = new Error();
                    error.Issue = "לא מכיל קוד נק' מכירה";
                    error.CurrentValue = range.Cells[row, column].Value2.ToString();
                    ErrorUtil.FinallizeErrorAndAdd(error, row, column, range);
                    result = false;
                }

                if (!checkIfContainsExactDigitNumber(range.Cells[row, column].Value2.ToString(), 9))
                {
                    if(error == null)
                    {
                        error = new Error();
                        error.Issue = "לא מכיל מס' הזמנה";
                        error.CurrentValue = range.Cells[row, column].Value2.ToString();
                    }
                    else
                    {
                        error.Issue += Environment.NewLine + "לא מכיל מס' הזמנה";
                    }

                    ErrorUtil.FinallizeErrorAndAdd(error, row, column, range);
                    return result;
                }
                return true;
            }
            return false;
        }

        public static bool checkIfContainsExactDigitNumber(string cell, int digitNumber)
        {
            int count = 0;
            foreach(char c in cell)
            {
                if(Char.IsDigit(c))
                {
                    count++;
                }
                else
                {
                    if (count == digitNumber)
                        return true;
                    else
                        count = 0;
                }
            }
            if (count == digitNumber)
                return true;
            return false;
        }
    }
}
