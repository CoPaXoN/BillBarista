using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillBarista
{
    class ErrorsModel
    {
        public static List<Error> errors = new List<Error>();

        public static void AddError(Error error)
        {
            errors.Add(error);
        }
    }
}
