using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using CatchTheFish.DbEntities;
using Jarloo.CardStock.Helpers;
using Jarloo.CardStock.Models;
using Core.Messaging;


namespace CatchTheFish.CollectData
{
    public class CollectDataManager
    {
        public static void DownloadTickers()
        {
            
        }

        public static void ScanStocks()
        {
            var engine = new YahooStockEngine();
            
            var quoteList = new ObservableCollection<Quote>();
            var boList = new List<CompanyList>();
            using(var db=new TheFishEntities())
            {
                 boList = db.CompanyLists.Where(x=>x.Sector.Equals("Health care",StringComparison.OrdinalIgnoreCase)).ToList();
            }

            //int i = 0;
            foreach(var item in boList)
            {
                //if (i > 2)
                //    break;
                try
                {
                    var quoteSingleCollection = new ObservableCollection<Quote>();
                    var quote = new Quote(item.Symbol.Trim());
                    quoteSingleCollection.Add(quote);
                    YahooStockEngine.Fetch(quoteSingleCollection);
                    quoteList.Add(quoteSingleCollection.FirstOrDefault());
                }catch(Exception ex)
                {
                    var message = ex.Message;
                }
                //i++;
            }
            DateTime today = DateTime.Today;                    // earliest time today 
            DateTime tomorrow = DateTime.Today.AddDays(1);      // earliest time tomorrow

            using (var db = new TheFishEntities())
            {
                var analyzer = new StockAnalyzer();
                
                foreach(var item in quoteList.ToList())
                {
                    if (!analyzer.IsTheFish(item))
                        continue;
                    if (db.CatchedFish.Where(x => x.Symbol.Equals(item.Symbol) && x.WhenCreated > today && x.WhenCreated<tomorrow).Any())
                        continue;
                    var catchedFish = new CatchedFish();
                    catchedFish.Symbol = item.Symbol;
                    catchedFish.WhenCreated = DateTime.Now;
                    catchedFish.Price = item.LastTradePrice;
                    catchedFish.PriceChangePercentage = item.ChangeInPercent;
                    db.CatchedFish.Add(catchedFish);
                    db.SaveChanges();
                    Messaging.SendEmailGmail("Symbol:" + item.Symbol + " Price:" + item.LastTradePrice + " Volume:" + item.Volume +" Previous Close Price:" + item.PreviousClose);
                    //}
                }
            }
        }
    }
}
