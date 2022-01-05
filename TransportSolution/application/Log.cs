using core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application
{
    class Log : LogAbstract, ILog
    {
        private static Log instance;

        private Log()
        { }

        public static Log I()
        {
            if (instance == null)
                instance = new Log();
            return instance;
        }

        private List<string> strs = new List<string>();

        public ILog log(string str)
        {
            strs.Add(str);
            return this;
        }
        public ILog write(string str)
        {
            writeConsole($"{DateTime.Now.ToShortTimeString()} - {str};");
            return this;
        }

        public ILog write()
        {
            writeConsole(strs.ToArray());
            return this;
        }
    }
}
