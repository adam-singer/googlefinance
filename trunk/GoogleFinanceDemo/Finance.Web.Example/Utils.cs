using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Finance.Web.Example
{
    public static class Utils
    {
        public static void WriteLine(ConsoleColor consoleColor, string format, params object[] args)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(format, args);
            Console.ResetColor();
        }
    }
}
