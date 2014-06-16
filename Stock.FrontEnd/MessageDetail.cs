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
        public static string GetMessageDetail(Quote quote)
        {
            var sb = new StringBuilder();
            sb.Append("<html><body><table>");
            sb.Append("<tr>");
            var i = 4;
            foreach (PropertyInfo prop in typeof(Quote).GetProperties())
            {
                
                sb.Append("<td>");
                sb.Append(string.Format("{0}: {1}", DisplayNameHelper.GetDisplayName(prop), prop.GetValue(quote, null)));
                sb.Append("</td>");
                if (i % 3 == 0)
                    sb.Append("</tr><tr>");
                i++;
            }
            if (i % 3 == 1)
                sb.Append("<td></td><td></td>");
            else if (i % 3 == 2)
                sb.Append("<td></td>");
           
            sb.Append("</tr>");
            sb.Append("</table></body></html>");
            return sb.ToString();
        }
    }
}
