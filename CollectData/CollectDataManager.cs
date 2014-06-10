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
        private const int StockFetchTrunk = 20;
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
                 boList = db.CompanyLists.Where(x=>x.Sector.Equals("Health care",StringComparison.OrdinalIgnoreCase) && x.Symbol.Length<5 ).ToList();
            }

            int i = 1;
            var quoteSingleCollectionTrunk = new ObservableCollection<Quote>();
            foreach(var item in boList)
            {
                //if (i > 2)
                //    break;
                try
                {
                    
                    var quote = new Quote(item.Symbol.Trim());
                    quoteSingleCollectionTrunk.Add(quote);
                    if (i == StockFetchTrunk)
                    {
                        YahooStockEngine.Fetch(quoteSingleCollectionTrunk);
                        foreach (var stockInfo in quoteSingleCollectionTrunk)
                            quoteList.Add(stockInfo);
                        quoteSingleCollectionTrunk = new ObservableCollection<Quote>();
                        i = 1;
                    }
                    i++;
                }catch(Exception ex)
                {
                    var message = ex.Message;
                }
                //i++;
            }

            try
            {
                if (quoteSingleCollectionTrunk.Count > 0)
                {
                    YahooStockEngine.Fetch(quoteSingleCollectionTrunk);
                    foreach (var stockInfo in quoteSingleCollectionTrunk)
                        quoteList.Add(stockInfo);
                }
            }catch(Exception ex)
            {
                var message = ex.Message;
            }

            DateTime today = DateTime.Today;                    // earliest time today 
            DateTime tomorrow = DateTime.Today.AddDays(1);      // earliest time tomorrow

            using (var db = new TheFishEntities())
            {
                var analyzer = new StockAnalyzer();
                
                foreach(var item in quoteList.ToList())
                {
                    var isPriceChangeFish = analyzer.IsPriceChangedDramatically(item);
                    var isVolumeChangeFish = analyzer.IsVolumeAbnormal(item);
                    if (!(isPriceChangeFish || isVolumeChangeFish))
                        continue;
                    if (db.CaughtFish.Where(x => x.Symbol.Equals(item.Symbol) && x.WhenCreated > today && x.WhenCreated<tomorrow).Any())
                        continue;
                    var caughtFish = new CaughtFish();
                    caughtFish.Symbol = item.Symbol;
                    caughtFish.WhenCreated = DateTime.Now;
                    caughtFish.Price = item.LastTradePrice;
                    caughtFish.PriceChangePercentage = item.ChangeInPercent;
                    caughtFish.Volume = item.Volume;
                    if (isPriceChangeFish)
                        caughtFish.FishType = 0;
                    else if (isVolumeChangeFish)
                        caughtFish.FishType = 1;
                    db.CaughtFish.Add(caughtFish);
                    db.SaveChanges();
                    Messaging.SendEmailGmail("Symbol:" + item.Symbol + " Price:" + item.LastTradePrice + " Volume:" + item.Volume +" Previous Close Price:" + item.PreviousClose);
                    //}
                }
            }
        }
    }
}
