using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Stock.Models;
using Core.Helpers;

namespace Stock.FrontEnd
{
    public class MessageDetail
    {
        private const int DisplayColumns = 1;
        private const string YahooFianceLink = "<a href=\"http://finance.yahoo.com/q?s={0}\">Yahoo Finance -- {0}</a>";
        public static string GetMessageDetail(Quote quote)
        {
            var sb = new StringBuilder();
            sb.Append("<html><body><table>");
            sb.Append("<tr><td>");
            sb.Append(string.Format(YahooFianceLink, quote.Symbol.Trim()));
            sb.Append("</td></tr></table>");
            sb.Append("<table border=\"1\" style=\"border-collapse : collapse; border : 1px solid orange;\"><tr>");
            var i = DisplayColumns;
            foreach (PropertyInfo prop in typeof(Quote).GetProperties())
            {
                sb.Append(string.Format("<td>{0}</td><td>{1}</td>", DisplayNameHelper.GetDisplayName(prop), prop.GetValue(quote, null)));
                if (i % ((DisplayColumns - 1) == 0 ? 1 : (DisplayColumns - 1)) == 0)
                    sb.Append("</tr><tr>");
                i++;
            }
            if (i % ((DisplayColumns - 1) == 0 ? 1 : (DisplayColumns - 1)) == 1)
                sb.Append("<td></td><td></td>");
            
           
            sb.Append("</tr>");
            sb.Append("</table></body></html>");
            return sb.ToString();
        }
    }
}
