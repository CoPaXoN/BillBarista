using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillBarista
{
    class Line : Record
    {

        private string _orderDesc;
        public string OrderDesc
        {
            get { return _orderDesc; }
            set { _orderDesc = value; }
        }

        private string _AMZProductCode = GetStringZeros(13);
        public string AMZProductCode
        {
            get { return _AMZProductCode; }
            set
            {
                _AMZProductCode = PadLeft(value, 13);
            }
        }

        private string _productCode = GetStringZeros(13);
        public string ProductCode
        {
            get { return _productCode; }
            set
            {
                _productCode = PadLeft(value, 13);
            }
        }

        private string _saleUnitType = "0";
        public string SaleUnitType
        {
            get { return _saleUnitType; }
            set
            {
                _saleUnitType = value;
            }
        }

        private string _quantity = GetStringZeros(7);
        public string Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = PadLeft(value, 5,2);
            }
        }

        private string _boxesQuantity = GetStringZeros(3);
        public string BoxesQuantity
        {
            get { return _boxesQuantity; }
            set
            {
                _boxesQuantity = PadLeft(value, 3);
            }
        }

        private string _packageQuantity = GetStringZeros(3);
        public string PackageQuantity
        {
            get { return _boxesQuantity; }
            set
            {
                _boxesQuantity = PadLeft(value, 3);
            }
        }

        private string _unitPriceTI = GetStringZeros(11);
        public string UnitPriceTI
        {
            get { return _unitPriceTI; }
            set
            {
                _unitPriceTI = PadLeft(value, 7, 4);
            }
        }

        private string _discount = GetStringZeros(9);
        public string Discount
        {
            get { return _discount; }
            set
            {
                _discount = PadLeft(value, 5, 4);
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
                _saleDiscount = PadLeft(value, 5, 4);
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
                _distrbutionDiscount = PadLeft(value, 5, 4);
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

        public string FILLER = $"{GetStringZeros(9)}";

        public override string ToString()
        {
            return $"{base.ToString()}{AMZProductCode}{ProductCode}{SaleUnitType}{Quantity}"
                 + $"{BoxesQuantity}{PackageQuantity}{UnitPriceTI}{Discount}{DiscountPrecent}"
                 + $"{SaleDiscount}{SaleDiscountPrecent}{DistrbutionDiscount}{DistrbutionDiscountPrecent}{FILLER}{Environment.NewLine}";
        }










    }
}
