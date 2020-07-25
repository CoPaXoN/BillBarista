using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillBarista
{
    class Record
    {
        private string _recordType;

        public string RecordType
        {
            get { return _recordType; }
            set { _recordType = value; }
        }

        private string _invoiceNumber = GetStringZeros(10);
        public string InvoiceNumber
        {
            get { return _invoiceNumber; }
            set
            {
                _invoiceNumber = PadLeft(value, 10);
            }
        }

        public override string ToString()
        {
            return RecordType;
        }

        public static string PadLeft(string value, int beforeDot, int afterDot)
        {
            if(value == "")
            {
                return GetStringZeros(beforeDot + afterDot);
            }
            //pads right since the toString removes zeros
            value = value.PadRight(beforeDot + afterDot, '0'); 
            //remove unecessery 0 from right
            value = value.Remove(value.IndexOf('.') + afterDot + 1);

            value = new String(value.Where(Char.IsDigit).ToArray());

            return value.PadLeft(beforeDot + afterDot);
        }

        public static string PadLeft(string value, int totalWidth)
        {
            return value.PadLeft(totalWidth);
        }

        public static string GetStringZeros(int numberOfZeros)
        {
            return String.Empty.PadRight(numberOfZeros);
        }
    }
}
