using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace BillBarista.Utils
{
    public static class ExcelUtil
    {
        public static int GetLastRow(Excel.Worksheet worksheet)
        {
            return worksheet.Cells.Find(
                What: "*",
                SearchOrder: Excel.XlSearchOrder.xlByRows,
                SearchDirection: Excel.XlSearchDirection.xlPrevious,
                MatchCase: false
            ).Row;
        }

        public static string GetStringValue(Excel.Range range, int row, int column)
        {
            return (range.Cells[row, column].Value2 == null) ? String.Empty : range.Cells[row, column].Value2.ToString();
        }
    }
}
