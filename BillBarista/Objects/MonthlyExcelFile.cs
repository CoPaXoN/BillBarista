using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace BillBarista.Objects
{
    class MonthlyExcelFile : ExcelFile
    {
        public Excel.Range invoicesRange;
        public Excel.Range ordersRange;
        public Excel.Range linesRange;

        public static string tempPath = @"C:\temp\monthlyFileCopy.xlsx";
        public MonthlyExcelFile(string path) : base(path)
        {
            invoicesRange = workbook.Sheets[Sheets.InvoicesSheetNumber].UsedRange;
            ordersRange = workbook.Sheets[Sheets.OrdersSheetNumber].UsedRange;
            linesRange = workbook.Sheets[Sheets.LinesSheetNumber].UsedRange;
            

        }


    }
}
