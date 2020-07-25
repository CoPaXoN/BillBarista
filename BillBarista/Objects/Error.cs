using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillBarista
{
    
    public class Error
    {
        private string _invoiceNumber;

        public string InvoiceNumber
        {
            get { return _invoiceNumber; }
            set { _invoiceNumber = value; }
        }

        private string _recordType;

        public string RecordType
        {
            get { return _recordType; }
            set { _recordType = value; }
        }

        private string _fieldName;

        public string FieldName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }

        private string _issue;

        public string Issue
        {
            get { return _issue; }
            set { _issue = value; }
        }

        private string _currentValue;

        public string CurrentValue
        {
            get { return _currentValue; }
            set { _currentValue = value; }
        }

        private string _correctValue;

        public string CorrectValue
        {
            get { return _correctValue; }
            set { _correctValue = value; }
        }

        private int _row;

        public int Row
        {
            get { return _row; }
            set { _row = value; }
        }

        private int _coulmn;

        public int Column
        {
            get { return _coulmn; }
            set { _coulmn = value; }
        }

        private int _worksheetIndex;

        public int WorksheetIndex
        {
            get { return _worksheetIndex; }
            set { _worksheetIndex = value; }
        }






    }
}
