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
                foreach(var item in boList)
                {
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
                }
                using (var db = new TheFishEntities())
                {
                    foreach(var item in quoteList.ToList())
                    {
                        //if(item.ChangeInPercent > 20)
                        //{
                            var catchedFish = new CatchedFish();
                            catchedFish.Symbol = item.Symbol;
                            catchedFish.WhenCreated = DateTime.Now;
                            catchedFish.Price = item.LastTradePrice;
                            catchedFish.PriceChangePercentage = item.ChangeInPercent;
                            db.CatchedFish.Add(catchedFish);
                            db.SaveChanges();
                        //}
                    }
                }
                
            
        }
    }
}
