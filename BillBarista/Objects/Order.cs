using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillBarista
{
    class Order : Record
    {
        private List<Line> _lines = new List<Line>();

        public List<Line> Lines
        {
            get { return _lines; }
            set { _lines = value; }
        }

        private string _storeNumber = GetStringZeros(10);
        public string StoreNumber
        {
            get { return _storeNumber; }
            set
            {
                int count = 0;
                string str = "";
                foreach(Char c in value)
                {
                    if(Char.IsDigit(c))
                    {
                        count++;
                        str += c;
                    }
                    else
                    {
                        if (count == 3)
                        {
                            _storeNumber = PadLeft(str,10);
                            return;
                        }
                        str = "";
                        count = 0;

                    }
                }
                _storeNumber = PadLeft(str, 10);
            }
        }

        private string _orderNumber = GetStringZeros(10);
        public string OrderNumber
        {
            get { return _orderNumber; }
            set
            {
                _orderNumber = PadLeft(value, 10);
            }
        }

        private string _certificateType = "1";
        public string CertificateType 
        {
            get { return _certificateType; }
            set
            {
                _certificateType = value;
            }
        }

        private string _date = GetStringZeros(6);
        public string Date
        {
            get { return _date; }
            set
            {
                _date = value;
            }
        }

        private string _enteranceStampNum = GetStringZeros(10);
        public string EnteranceStampNum
        {
            get { return _enteranceStampNum; }
            set
            {
                _enteranceStampNum = PadLeft(value, 10);
            }
        }

        private string _receivedAmount = GetStringZeros(9);
        public string ReceivedAmount
        {
            get { return _receivedAmount; }
            set
            {
                _receivedAmount = PadLeft(value, 7,2);
            }
        }

        private string _discount = GetStringZeros(9);
        public string Discount
        {
            get { return _discount; }
            set
            {
                _discount = PadLeft(value, 7, 2);
            }
        }

        private string _discountPrecent = GetStringZeros(4);
        public string DiscountPrecent
        {
            get { return _discountPrecent; }
            set
            {
                _discountPrecent = PadLeft(value, 2, 2);
            }
        }

        private string _saleDiscount = GetStringZeros(9);
        public string SaleDiscount
        {
            get { return _saleDiscount; }
            set
            {
                _saleDiscount = PadLeft(value, 7, 2);
            }
        }

        private string _saleDiscountPrecent = GetStringZeros(4);
        public string SaleDiscountPrecent
        {
            get { return _saleDiscountPrecent; }
            set
            {
                _saleDiscountPrecent = PadLeft(value, 2, 2);
            }
        }

        private string _distrbutionDiscount = GetStringZeros(9);
        public string DistrbutionDiscount
        {
            get { return _distrbutionDiscount; }
            set
            {
                _distrbutionDiscount = PadLeft(value, 7, 2);
            }
        }

        private string _distrbutionDiscountPrecent = GetStringZeros(4);
        public string DistrbutionDiscountPrecent
        {
            get { return _distrbutionDiscountPrecent; }
            set
            {
                _distrbutionDiscountPrecent = PadLeft(value, 2, 2);
            }
        }

        public string FILLER = $"{GetStringZeros(14)}";

        public override string ToString()
        {
            string lines = "";
            foreach (Line line in Lines)
            {
                lines += line.ToString();
            }
            return $"{base.ToString()}{StoreNumber}{OrderNumber}{CertificateType}{Date}"
                 + $"{EnteranceStampNum}{ReceivedAmount}{Discount}{DiscountPrecent}"
                 + $"{SaleDiscount}{SaleDiscountPrecent}{DistrbutionDiscount}{DistrbutionDiscountPrecent}{FILLER}{Environment.NewLine}{lines}";
        }


    }
}
