using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using CatchTheFish.DbEntities;


namespace Stock.FdaCalendar
{
    public class CalendarParser
    {
        public static string ParseWebContent(string Url)
        {
            var web = new HtmlWeb();
            var document = web.Load(Url);
            var page = document.DocumentNode;
            var db = new TheFishEntities();
            //loop through all div tags with item css class
            foreach(var item in page.QuerySelectorAll("table.sortable>tbody>tr"))
            {
                var i = 0;
                var calendar = new CatchTheFish.DbEntities.FdaCalendar();
                foreach(var subItem in item.QuerySelectorAll("td"))
                {
                    var str = subItem.InnerText;
                    switch(i)
                    {
                        case 0:
                            calendar.Symbol = str;
                            break;
                        case 3:
                            calendar.Type = str;
                            break;
                        case 4:
                            DateTime dt;
                            if(DateTime.TryParse(str, out dt))
                            {
                                calendar.CatalystDate = dt;
                            }
                            break;
                        case 5:
                            calendar.CatelystNotes = str;
                            break;
                    }
                }
            }
            return "";
        }

        public static void ParseWebPage(string content)
        {

        }
    }
}
