using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillBarista
{
  
    public static class Sheets
    {
        public static int InvoicesSheetNumber = 1;
        public static int OrdersSheetNumber = 2;
        public static int LinesSheetNumber = 3;
    }



    public static class ColumnsInvoice
    {
        public static int RecordType = 1;
        public static int InvoiceNumber = 5;
        public static int SupplierNumber = 3;
        public static int Date = 4;
        public static int TaxAmount = 10;
        public static int CustomerNumber = 2;
        public static int RegularDiscountNumber = 7;
        public static int SaleDiscountNumber = 8;
        public static int DistrupitionDiscountNumber = 9;
    }

    public static class ColumnsOrder
    {
        public static int RecordType = 1;
        public static int InvoiceNumber = 16;
        public static int OrderDesc = 15;
        public static int OrderNumber = 3;
        public static int RecivedAmount = 7;
        public static int StoreNumber = 2;
        public static int CertificateType = 7;
        public static int Date = 5;
        public static int EnteranceStampNum = 6;
        public static int Discount = 8;
        public static int DiscountPrecent = 9;
        public static int SaleDiscount = 10;
        public static int SaleDiscountPrecent = 11;
        public static int DistrbutionDiscount = 12;
        public static int DistrbutionDiscountPrecent = 13;

    }

    public static class ColumnsLines
    {
        public static int InvoiceNumber = 17;
        public static int RecordType = 1;
        public static int Product = 3;
        public static int UnitPrice = 8;
        public static int Quantity = 5;
        public static int OrderDesc = 16;
        public static int AMZProductCode = 2;
        public static int SaleUnitType = 4;
        public static int BoxesQuantity = 6;
        public static int PackageQuantity = 7;
        public static int UnitPriceTI = 8;
        public static int Discount = 9;
        public static int DiscountPrecent = 10;
        public static int SaleDiscount = 11;
        public static int SaleDiscountPrecent = 12;
        public static int DistrbutionDiscount = 13;
        public static int DistrbutionDiscountPrecent = 14;
    }

    public static class RecordType
    {
        public static string invoice = "0";
        public static string order = "1";
        public static string line = "2";

    }



}
