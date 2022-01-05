using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core
{
    abstract class LogAbstract
    {
        protected String formatLog(String[] strings)
        {
            return String.Join(";\n\r", strings);
        }
        protected void writeConsole(String s)
        {
            Console.WriteLine(s);
        }
        protected void writeConsole(String[] s)
        {
            Console.WriteLine(formatLog(s));
        }
    }
}
