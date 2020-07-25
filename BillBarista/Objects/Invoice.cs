using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillBarista
{
    class Invoice : Record
    {

        private List<Order> _orders = new List<Order>();

        private bool _isNoErrors = true;

        public bool IsNoErrors
        {
            get { return _isNoErrors; }
            set { _isNoErrors = value; }
        }

        public List<Order> Orders
        {
            get { return _orders; }
            set
            {
                _orders = value;
            }
        }


        public void AddOrder(Order order)
        {
            if(order != null)
                Orders.Add(order);
        }

        private static Invoice instance = null;

        private Invoice()
        {
        }
        public static Invoice GetInstance()
        {
            if (instance == null)
                instance = new Invoice();
            return instance;
        }

        public void Zeroize()
        {
            instance = new Invoice();
        }

        

        private string _customerNumber = GetStringZeros(5);
        public string CustomerNumber
        {
            get { return _customerNumber; }
            set
            {
                _customerNumber = PadLeft(value,5);
            } 
        }

        private string _supplierNumber = GetStringZeros(10);
        public string SupplierNumber
        {
            get { return _supplierNumber; }
            set{ _supplierNumber = PadLeft(value, 10); }
        }

        private string _date = GetStringZeros(6);
        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }

        private string _receivedAmount = GetStringZeros(12);

        public string ReceivedAmount
        {
            get { return _receivedAmount; }
            set
            {
                _receivedAmount = PadLeft(value,10,2);
            }
        }

        private string _regularDiscount = GetStringZeros(12);

        public string RegularDiscount 
        {
            get { return _regularDiscount; }
            set { _regularDiscount = PadLeft(value,10,2); }
        }

        private string _saleDiscount = GetStringZeros(12);

        public string SaleDiscount
        {
            get { return _saleDiscount; }
            set { _saleDiscount = PadLeft(value, 10, 2); }
        }

        private string _distrupitionCommision = GetStringZeros(12);

        public string DistrupitionDiscount
        {
            get { return _distrupitionCommision; }
            set { _distrupitionCommision = PadLeft(value, 10, 2); }
        }

        private string _taxAmount = GetStringZeros(12);

        public string TaxAmount
        {
            get { return _taxAmount; }
            set { _taxAmount = PadLeft(value, 10, 2); }
        }

        public string FILLER = $"{GetStringZeros(8)}";

        public override string ToString()
        {
            string orders = "";
            foreach(Order order in Orders)
            {
                orders += order.ToString();
            }
            return $"{base.ToString()}{CustomerNumber}{SupplierNumber}{Date}{InvoiceNumber}{ReceivedAmount}{RegularDiscount}{SaleDiscount}{DistrupitionDiscount}{TaxAmount}{FILLER}{Environment.NewLine}{orders}";
        }

    }
}
