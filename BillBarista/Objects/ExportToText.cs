using BillBarista.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace BillBarista.Objects
{
    class ExportToText
    {
        private ExcelFile catalogExcelFile;
        private MonthlyExcelFile monthlyExcelFile;

        private List<RecordToBeDeleted> recordsToBeDeleted = new List<RecordToBeDeleted>();
        public static TextFile textFile;
        public ExportToText(MonthlyExcelFile monthlyExcelFile, ExcelFile catalogExcelFile)
        {
            this.monthlyExcelFile = monthlyExcelFile;
            this.catalogExcelFile = catalogExcelFile;
            textFile = new TextFile(TextFile.path);

            int lastRow = ExcelUtil.GetLastRow(monthlyExcelFile.worksheet);
            for (int row = 2; row <= lastRow; row++)
            {
                CreateInvoice(row);
                if (Invoice.GetInstance().IsNoErrors)
                {
                    textFile.Write(Invoice.GetInstance().ToString());
                    deleteRecordsToBeDeleted();
                }
                Invoice.GetInstance().Zeroize();
            }
            PassOrdersLeft();
            PassLinesLeft();

            monthlyExcelFile.CleanUp();
            catalogExcelFile.CleanUp();
            if (ErrorsModel.errors.Count < 1)
            {
                Process.Start(TextFile.path);
            }
        }

        private bool IsAnyCellContainsValue(Excel.Range range, int row)
        {
            if (range.Worksheet.Index == Sheets.OrdersSheetNumber)
            {
                for (int cell = 1; cell <= 16; cell++)
                {
                    if (ExcelUtil.GetStringValue(range, row, cell) != String.Empty)
                    {
                        return true;
                    }
                }
            }
            else
            {
                for (int cell = 1; cell <= 17; cell++)
                {
                    if (ExcelUtil.GetStringValue(range, row, cell) != String.Empty)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void PassOrdersLeft()
        {
            Excel.Range range = monthlyExcelFile.ordersRange;
            int length = ExcelUtil.GetLastRow(monthlyExcelFile.ordersRange.Worksheet);
            for (int row = 2; row < length; row++)
            {
                if (IsAnyCellContainsValue(range, row))
                {
                    OrderUtil.CheckOrderDesc(monthlyExcelFile.ordersRange, row);

                    ErrorUtil.IsInvoiceNumberOk(monthlyExcelFile.ordersRange, row, ColumnsOrder.InvoiceNumber);

                    ErrorUtil.CheckRecordType(monthlyExcelFile.ordersRange, row, RecordType.order);

                    OrderUtil.CheckReceivedAmount(monthlyExcelFile.ordersRange, row);

                    OrderUtil.CheckOrderNumber(monthlyExcelFile.ordersRange, row);
                }
            }
        }

        private void PassLinesLeft()
        {
            Excel.Range range = monthlyExcelFile.linesRange;
            int length = ExcelUtil.GetLastRow(range.Worksheet);
            for (int row = 2; row < length; row++)
            {
                if (IsAnyCellContainsValue(range, row))
                {
                    ErrorUtil.IsInvoiceNumberOk(range, row, ColumnsLines.InvoiceNumber);

                    ErrorUtil.CheckRecordType(range, row, RecordType.line);

                    LineUtil.CheckUnitPrice(range, row);

                    LineUtil.CheckQuantity(range, row);

                    LineUtil.CheckProductTypeExist(range, row, catalogExcelFile.range);
                }
            }
        }



        private void deleteRecordsToBeDeleted()
        {
            foreach (RecordToBeDeleted recordToBeDeleted in recordsToBeDeleted)
            {
                if (recordToBeDeleted.Sheet == Sheets.InvoicesSheetNumber)
                {
                    monthlyExcelFile.invoicesRange.Rows[recordToBeDeleted.Record].Clear();
                }
                if (recordToBeDeleted.Sheet == Sheets.OrdersSheetNumber)
                {
                    monthlyExcelFile.ordersRange.Rows[recordToBeDeleted.Record].Clear();
                }
                if (recordToBeDeleted.Sheet == Sheets.LinesSheetNumber)
                {
                    monthlyExcelFile.linesRange.Rows[recordToBeDeleted.Record].Clear();
                }
            }
        }

        private void CreateInvoice(int row)
        {
            //check if invoice number is ok, or quit
            if (ErrorUtil.IsInvoiceNumberOk(monthlyExcelFile.invoicesRange, row, ColumnsInvoice.InvoiceNumber))
            {
                Invoice.GetInstance().InvoiceNumber = ExcelUtil.GetStringValue(monthlyExcelFile.invoicesRange, row, ColumnsInvoice.InvoiceNumber);
            }

            //check and add invoice
            if (ErrorUtil.CheckRecordType(monthlyExcelFile.invoicesRange, row, RecordType.invoice))
            {
                Invoice.GetInstance().RecordType = ExcelUtil.GetStringValue(monthlyExcelFile.invoicesRange, row, ColumnsInvoice.RecordType);
            }

            if (InvoiceUtil.CheckSupplierNumber(monthlyExcelFile.invoicesRange, row))
            {
                Invoice.GetInstance().SupplierNumber = ExcelUtil.GetStringValue(monthlyExcelFile.invoicesRange, row, ColumnsInvoice.SupplierNumber);
            }

            if (InvoiceUtil.CheckDate(monthlyExcelFile.invoicesRange, row))
            {
                Invoice.GetInstance().Date = ExcelUtil.GetStringValue(monthlyExcelFile.invoicesRange, row, ColumnsInvoice.Date);
            }

            if (InvoiceUtil.CheckTaxAmount(monthlyExcelFile.invoicesRange, row))
            {
                Invoice.GetInstance().TaxAmount = ExcelUtil.GetStringValue(monthlyExcelFile.invoicesRange, row, ColumnsInvoice.TaxAmount);
            }

            Invoice.GetInstance().CustomerNumber = ExcelUtil.GetStringValue(monthlyExcelFile.invoicesRange, row, ColumnsInvoice.CustomerNumber);
            Invoice.GetInstance().RegularDiscount = ExcelUtil.GetStringValue(monthlyExcelFile.invoicesRange, row, ColumnsInvoice.RegularDiscountNumber);
            Invoice.GetInstance().SaleDiscount = ExcelUtil.GetStringValue(monthlyExcelFile.invoicesRange, row, ColumnsInvoice.SaleDiscountNumber);
            Invoice.GetInstance().DistrupitionDiscount = ExcelUtil.GetStringValue(monthlyExcelFile.invoicesRange, row, ColumnsInvoice.DistrupitionDiscountNumber);

            if (Invoice.GetInstance().IsNoErrors)
            {
                AddOrders(Invoice.GetInstance().InvoiceNumber);
                if (Invoice.GetInstance().Orders.Count < 1)
                {
                    ErrorUtil.FinallizeErrorAndAdd(new Error { Issue = "לא נמצאו הזמנות לחשבונית "}, row: 1, column: 1, range: monthlyExcelFile.ordersRange);
                }
                else
                {
                    //mark row to be deleted
                    recordsToBeDeleted.Add(new RecordToBeDeleted { Record = row, Sheet = Sheets.InvoicesSheetNumber });
                }
            }
        }

        public void AddOrders(string invoiceNumber)
        {
            Excel.Range cell = monthlyExcelFile.ordersRange.Find(invoiceNumber.TrimStart());
            int row;

            int nextsRow;
            if (cell != null)
            {
                row = cell.Row;
                if (!AddOrder(cell.Row))
                {
                    ErrorUtil.FinallizeErrorAndAdd(new Error { Issue = "לא נמצאו שורות להזמנה" }, row: 1, column: 1, range: monthlyExcelFile.ordersRange);

                    return;
                }
                recordsToBeDeleted.Add(new RecordToBeDeleted { Record = cell.Row, Sheet = Sheets.OrdersSheetNumber });

                while (monthlyExcelFile.ordersRange.FindNext(cell) != null)
                {
                    cell = monthlyExcelFile.ordersRange.Find(invoiceNumber.TrimStart());
                    nextsRow = cell.Row;

                    if (row == nextsRow)
                        return;
                    if (!AddOrder(cell.Row))
                    {
                        return;
                    }
                    recordsToBeDeleted.Add(new RecordToBeDeleted { Record = cell.Row, Sheet = Sheets.OrdersSheetNumber });
                }
            }
        }
        private bool AddOrder(int row)
        {
            if (!OrderUtil.CheckOrderDesc(monthlyExcelFile.ordersRange, row))
            {
                return false;
            }
            Order order = new Order();

            if (ErrorUtil.IsInvoiceNumberOk(monthlyExcelFile.ordersRange, row, ColumnsOrder.InvoiceNumber))
            {
                order.InvoiceNumber = ExcelUtil.GetStringValue(monthlyExcelFile.ordersRange, row, ColumnsOrder.InvoiceNumber);
            }
            else
            {
                return false;
            }

            if (ErrorUtil.CheckRecordType(monthlyExcelFile.ordersRange, row, RecordType.order))
            {
                order.RecordType = ExcelUtil.GetStringValue(monthlyExcelFile.ordersRange, row, ColumnsOrder.RecordType);
            }

            if (OrderUtil.CheckReceivedAmount(monthlyExcelFile.ordersRange, row))
            {
                order.ReceivedAmount = ExcelUtil.GetStringValue(monthlyExcelFile.ordersRange, row, ColumnsOrder.RecivedAmount);
            }

            if (OrderUtil.CheckOrderNumber(monthlyExcelFile.ordersRange, row))
            {
                order.OrderNumber = ExcelUtil.GetStringValue(monthlyExcelFile.ordersRange, row, ColumnsOrder.OrderNumber);
            }
            order.StoreNumber = ExcelUtil.GetStringValue(monthlyExcelFile.ordersRange, row, ColumnsOrder.StoreNumber);
            order.Date = ExcelUtil.GetStringValue(monthlyExcelFile.ordersRange, row, ColumnsOrder.Date);
            order.EnteranceStampNum = ExcelUtil.GetStringValue(monthlyExcelFile.ordersRange, row, ColumnsOrder.EnteranceStampNum);
            order.Discount = ExcelUtil.GetStringValue(monthlyExcelFile.ordersRange, row, ColumnsOrder.Discount);
            order.DiscountPrecent = ExcelUtil.GetStringValue(monthlyExcelFile.ordersRange, row, ColumnsOrder.DiscountPrecent);
            order.SaleDiscount = ExcelUtil.GetStringValue(monthlyExcelFile.ordersRange, row, ColumnsOrder.SaleDiscount);
            order.SaleDiscountPrecent = ExcelUtil.GetStringValue(monthlyExcelFile.ordersRange, row, ColumnsOrder.SaleDiscountPrecent);
            order.DistrbutionDiscount = ExcelUtil.GetStringValue(monthlyExcelFile.ordersRange, row, ColumnsOrder.DistrbutionDiscount);
            order.DistrbutionDiscountPrecent = ExcelUtil.GetStringValue(monthlyExcelFile.ordersRange, row, ColumnsOrder.DistrbutionDiscountPrecent);

            Excel.Range cell = monthlyExcelFile.linesRange.Find(order.InvoiceNumber.TrimStart());
            int cellRow;

            int nextsRow = 1;

            if (cell != null)
            {
                cellRow = cell.Row;

                if (ExcelUtil.GetStringValue(monthlyExcelFile.ordersRange, row, ColumnsOrder.OrderDesc).TrimStart()
                    == ExcelUtil.GetStringValue(monthlyExcelFile.linesRange, cell.Row, ColumnsLines.OrderDesc).TrimStart())
                {
                    AddLine(order, cell.Row);
                }
                cell = monthlyExcelFile.linesRange.Find(order.InvoiceNumber.TrimStart());
                nextsRow = cell.Row;
                while (cellRow != nextsRow)
                {
                    cell = monthlyExcelFile.linesRange.FindNext(cell);
                    if (cell != null)
                    {
                        nextsRow = cell.Row;
                        if (ExcelUtil.GetStringValue(monthlyExcelFile.ordersRange, row, ColumnsOrder.OrderDesc).TrimStart()
                            == ExcelUtil.GetStringValue(monthlyExcelFile.linesRange, cell.Row, ColumnsLines.OrderDesc).TrimStart())
                        {
                            AddLine(order, cell.Row);
                        }
                    }
                }
            }
            if (order.Lines.Count < 1)
            {
                ErrorUtil.FinallizeErrorAndAdd(new Error { Issue = "לא נמצאו שורות להזמנה" }, row: 1, column: 1, range: monthlyExcelFile.linesRange);
                return false;
            }

            Invoice.GetInstance().AddOrder(order);
            recordsToBeDeleted.Add(new RecordToBeDeleted { Record = row, Sheet = Sheets.OrdersSheetNumber });
            return true;
        }

        private void AddLine(Order order, int row)
        {
            Line line = new Line();

            if (ErrorUtil.IsInvoiceNumberOk(monthlyExcelFile.linesRange, row, ColumnsLines.InvoiceNumber))
            {
                line.InvoiceNumber = ExcelUtil.GetStringValue(monthlyExcelFile.linesRange, row, ColumnsLines.InvoiceNumber);
            }

            if (ErrorUtil.CheckRecordType(monthlyExcelFile.linesRange, row, RecordType.line))
            {
                line.RecordType = ExcelUtil.GetStringValue(monthlyExcelFile.linesRange, row, ColumnsLines.RecordType);
            }

            if (LineUtil.CheckUnitPrice(monthlyExcelFile.linesRange, row))
            {
                line.UnitPriceTI = ExcelUtil.GetStringValue(monthlyExcelFile.linesRange, row, ColumnsLines.UnitPriceTI);
            }

            if (LineUtil.CheckQuantity(monthlyExcelFile.linesRange, row))
            {
                line.Quantity = ExcelUtil.GetStringValue(monthlyExcelFile.linesRange, row, ColumnsLines.Quantity);
            }

            if (LineUtil.CheckProductTypeExist(monthlyExcelFile.linesRange, row, catalogExcelFile.range))
            {
                line.ProductCode = ExcelUtil.GetStringValue(monthlyExcelFile.linesRange, row, ColumnsLines.Product);
            }

            line.AMZProductCode = ExcelUtil.GetStringValue(monthlyExcelFile.linesRange, row, ColumnsLines.AMZProductCode);
            line.SaleUnitType = ExcelUtil.GetStringValue(monthlyExcelFile.linesRange, row, ColumnsLines.SaleUnitType);
            line.BoxesQuantity = ExcelUtil.GetStringValue(monthlyExcelFile.linesRange, row, ColumnsLines.BoxesQuantity);
            line.PackageQuantity = ExcelUtil.GetStringValue(monthlyExcelFile.linesRange, row, ColumnsLines.PackageQuantity);
            line.Discount = ExcelUtil.GetStringValue(monthlyExcelFile.linesRange, row, ColumnsLines.Discount);
            line.DiscountPrecent = ExcelUtil.GetStringValue(monthlyExcelFile.linesRange, row, ColumnsLines.DiscountPrecent);
            line.SaleDiscount = ExcelUtil.GetStringValue(monthlyExcelFile.linesRange, row, ColumnsLines.SaleDiscount);
            line.SaleDiscountPrecent = ExcelUtil.GetStringValue(monthlyExcelFile.linesRange, row, ColumnsLines.SaleDiscountPrecent);
            line.DistrbutionDiscount = ExcelUtil.GetStringValue(monthlyExcelFile.linesRange, row, ColumnsLines.DistrbutionDiscount);
            line.DistrbutionDiscountPrecent = ExcelUtil.GetStringValue(monthlyExcelFile.linesRange, row, ColumnsLines.DistrbutionDiscountPrecent);

            order.Lines.Add(line);
            recordsToBeDeleted.Add(new RecordToBeDeleted { Record = row, Sheet = Sheets.LinesSheetNumber });
        }

        private int FindLineRow(string invoice, string orderDesc, string orderOrderDesc)
        {
            Excel.Range cell = monthlyExcelFile.linesRange.Find(invoice);
            int row = cell.Row;
            while (ExcelUtil.GetStringValue(monthlyExcelFile.linesRange, row, ColumnsLines.OrderDesc) != orderOrderDesc)
            {
                row = monthlyExcelFile.linesRange.FindNext(cell).Row;
            }
            return row;
        }
    }
}
