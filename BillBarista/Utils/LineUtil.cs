using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace BillBarista.Utils
{
    public static class LineUtil
    {
        public static bool CheckProductTypeExist(Excel.Range range, int row, Excel.Range catalogRange)
        {
            Error error = null;

            int column = ColumnsLines.Product;

            if(!ErrorUtil.IsEmptyCell(range,row,column))
            {
                int catalogLastRow = ExcelUtil.GetLastRow(catalogRange.Worksheet);
                for (int catalogRow = 2; catalogRow <= catalogLastRow; catalogRow++)
                {
                    if (range.Cells[row, column].Value2.ToString() == catalogRange.Cells[catalogRow, 1].Value2.ToString())
                    {
                        return true;
                    }
                }
                error = new Error();
                error.Issue = "לא קיים בקטלוג";
                error.CurrentValue = range.Cells[row, column].Value2.ToString();
                ErrorUtil.FinallizeErrorAndAdd(error, row, column, range);
                return false;
            }
            return false;
        }

        public static bool CheckUnitPrice(Excel.Range range, int row)
        {
            //Check Received Amount not empty
            Error error = null;
            int column = ColumnsLines.UnitPrice;

            if(!ErrorUtil.IsEmptyCell(range,row,column))
            {
                //check is positive
                if (range.Cells[row, column].Value2 < 0)
                {
                    error = new Error();
                    error.Issue = "לא חיובי";
                    error.CurrentValue = range.Cells[row, column].Value2.ToString();
                    ErrorUtil.FinallizeErrorAndAdd(error, row, column, range);
                    return false;
                }
                return true;
            }
            return false;
        }

        public static bool CheckQuantity(Excel.Range range, int row)
        {
            //Check Received Amount not empty
            Error error = null;
            int column = ColumnsLines.UnitPrice;

            if(!ErrorUtil.IsEmptyCell(range,row,column))
            {

                //check is positive
                if (range.Cells[row, column].Value2 == (int)range.Cells[row, column].Value)
                {
                    error = new Error();
                    error.Issue = "מספר לא שלם";
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
