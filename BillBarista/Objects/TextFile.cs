using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BillBarista.Objects
{
    class TextFile
    {
        public static string path = @"C:\temp\AMZ.txt";
        public TextFile(string path)
        {
            File.WriteAllText(path, String.Empty);
        }

        public void Write(string text)
        {
            using (StreamWriter file = new StreamWriter(path,true))
            {
                file.Write(text);
                file.Flush();
            }
        }
    }
}
