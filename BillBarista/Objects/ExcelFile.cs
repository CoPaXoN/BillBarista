using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace BillBarista
{
    public class ExcelFile
    {
        public Excel.Application app;
        public Excel.Workbooks workbooks;
        public Excel.Workbook workbook;
        public Excel.Worksheet worksheet;
        public Excel.Range range;
        public ExcelFile(string path)
        {
            app = new Excel.Application();
            
            workbooks = app.Workbooks;
            workbook = workbooks.Open(path);
            worksheet = workbook.Sheets[1];
            range = worksheet.UsedRange;
        }
        public void CleanUp()
        {
            workbook.Save();
            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects:
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(workbooks);
            workbook.Close();
            Marshal.ReleaseComObject(workbook);
            if (worksheet != null)
            {
                Marshal.ReleaseComObject(worksheet);
            }
            

            //quit and release
            app.Quit();
            Marshal.ReleaseComObject(app);
        }

        public Excel.Range GetRange(int index)
        {
            worksheet = workbook.Worksheets.get_Item(index);
            range = worksheet.UsedRange;
            return range;
        }

        public void SaveAs(string path)
        {
            app.DisplayAlerts = false;
            workbook.SaveAs(path);
        }
    }
}
