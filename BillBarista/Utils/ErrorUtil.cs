using BillBarista.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace BillBarista
{
    public class ErrorUtil
    {
        public static bool CheckRecordType(Excel.Range range, int row, string type)
        {
            Error error = null;
            int column = 1; //recordType always on column 1

            if (!ErrorUtil.IsEmptyCell(range, row, column))
            {
                string value = ExcelUtil.GetStringValue(range, row, column);
                if (value != type)
                {
                    error = new Error();
                    error.Issue = $"ערך הרשומה לא {type}";
                    error.CurrentValue = value;

                    FinallizeErrorAndAdd(error, row, column, range);
                    return false;
                }
                return true;
            }
            return false;
        }

        public static void FinallizeErrorAndAdd(Error error, int row, int column, Excel.Range range)
        {
            if(range.Worksheet.Index == Sheets.InvoicesSheetNumber)
            {
                error.InvoiceNumber = ExcelUtil.GetStringValue(range, row, ColumnsInvoice.InvoiceNumber);
                error.RecordType = "Invoice";
            }
            if (range.Worksheet.Index == Sheets.OrdersSheetNumber)
            {
                error.InvoiceNumber = ExcelUtil.GetStringValue(range, row, ColumnsOrder.InvoiceNumber);
                error.RecordType = "Order";
            }
            if (range.Worksheet.Index == Sheets.LinesSheetNumber)
            {
                error.InvoiceNumber = ExcelUtil.GetStringValue(range, row, ColumnsLines.InvoiceNumber);
                error.RecordType = "Line";
            }
            error.Row = row;
            error.Column = column;
            error.FieldName = range.Cells[1, column].Value;
            error.WorksheetIndex = range.Worksheet.Index;
            error.InvoiceNumber = (error.InvoiceNumber == "" || error.InvoiceNumber == "Invoice") ? Invoice.GetInstance().InvoiceNumber:error.InvoiceNumber;
            Invoice.GetInstance().IsNoErrors = false;
            ErrorsModel.AddError(error);
        }

        public static bool IsEmptyCell(Excel.Range range, int row, int column)
        {
            //Check not empty
            Error error = null;

            if (ExcelUtil.GetStringValue(range,row,column) == "")
            {
                error = new Error();
                error.Issue = "שדה ריק";
                FinallizeErrorAndAdd(error, row, column, range);
                return true;
            }
            return false;
        }

        public static bool IsInvoiceNumberOk(Excel.Range range, int row, int column)
        {
            //if Invoice not empty
            Error error = null;
            if (IsEmptyCell(range, row, column))
            {
                return false;
            }
            //if invoice length is not 8
            int length = range.Cells[row, column].Value2.ToString().Length;
            if (length != 8)
            {
                error = new Error();
                error.Issue = "שדה לא מכיל 8 תווים";
                error.CurrentValue = range.Cells[row, column].Value2.ToString();
                FinallizeErrorAndAdd(error, row, column, range);
                return false;
            }
            return true;
        }
    }
}
