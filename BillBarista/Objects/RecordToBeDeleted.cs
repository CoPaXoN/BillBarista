using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillBarista.Objects
{
    class RecordToBeDeleted
    {
        private int _record;

        public int Record
        {
            get { return _record; }
            set { _record = value; }
        }

        private int _sheet;

        public int Sheet
        {
            get { return _sheet; }
            set { _sheet = value; }
        }


    }
}
