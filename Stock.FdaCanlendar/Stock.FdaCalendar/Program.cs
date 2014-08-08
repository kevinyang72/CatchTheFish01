using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.FdaCalendar
{
    class Program
    {
        static void Main(string[] args)
        {
            CalendarParser.ParseWebContent("http://www.biopharmcatalyst.com/fda-calendar/");
        }
    }
}
