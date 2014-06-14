using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using CatchTheFish.DbEntities;
using Stock.Models;
using Stock.StockFetcher;
using Stock.StockAnalyzer;
using Core.Log;
using Core.Messaging;

namespace CatchTheFish.CollectData
{
    public class CollectDataManager
    {
        private const int StockFetchTrunk = 20;
        public const string MessageText = @"{0} Symbol: {1} Price: {2} Price Change: {3}% Volume: {4} Volume Change: {5}% ";
        public static void DownloadTickers()
        {
            
        }

        public static void ScanStocks()
        {
            Log.Error(typeof(CollectDataManager),"Test tets");
            Log.Error(typeof(CollectDataManager), "Test tets 123", new Exception("Failed to test"));
            var engine = new YahooStockEngine();
            
            var quoteList = new ObservableCollection<Quote>();
            var boList = new List<CompanyList>();
            using(var db=new TheFishEntities())
            {
                 boList = db.CompanyLists.Where(x=>x.Sector.Equals("Health care",StringComparison.OrdinalIgnoreCase) && x.Symbol.Length<5 ).ToList();
            }

            int i = 1;
            var quoteSingleCollectionTrunk = new ObservableCollection<Quote>();
            var quoteYahooDownloadDict = new Dictionary<string, StockQuote>();
            foreach(var item in boList)
            {
                //if (i > 2)
                //    break;
                try
                {
                    
                    var quote = new Quote(item.Symbol.Trim());
                    quoteSingleCollectionTrunk.Add(quote);
                    quoteYahooDownloadDict.Add(item.Symbol, null);
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
                foreach(var item in quoteList.ToList())
                {
                    var result = StockAnalyzer.AnalyzeStock(item);
                    var isPriceChangeFish = result.IsPriceChangedDramatically;
                    var isVolumeChangeFish = result.IsVolumeAbnormal;
                    var isPrice52WeeksLow = result.IsPrice52WeeksLow;
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
                    if(item.AverageDailyVolume> 0 && item.Volume>0)
                        caughtFish.VolumeChangePercentage = (int)(0.5M + 100M * (item.Volume - item.AverageDailyVolume) / item.AverageDailyVolume);
                    var message = "";
                    if (isPriceChangeFish)
                    {
                        caughtFish.FishType = 0;
                        message = string.Format(MessageText, "Price Change Alert -- ", caughtFish.Symbol, caughtFish.Price.ToString(), caughtFish.PriceChangePercentage.ToString(), caughtFish.Volume.ToString(), caughtFish.VolumeChangePercentage);
                    }
                    else if (isVolumeChangeFish)
                    {
                        caughtFish.FishType = 1;
                        message = string.Format(MessageText, "Volume Change Alert -- ", caughtFish.Symbol, caughtFish.Price.ToString(), caughtFish.PriceChangePercentage.ToString(), caughtFish.Volume.ToString(), caughtFish.VolumeChangePercentage.ToString());
                    }
                    else if (isPrice52WeeksLow)
                    {
                        caughtFish.FishType = 1;
                        message = string.Format(MessageText, "52 Weeks low price Alert -- ", caughtFish.Symbol, caughtFish.Price.ToString(), caughtFish.PriceChangePercentage.ToString(), caughtFish.Volume.ToString(), caughtFish.VolumeChangePercentage.ToString());
                    }
                    db.CaughtFish.Add(caughtFish);
                    db.SaveChanges();
                    Messaging.SendEmailGmail(message);
                    //}
                }
            }
        }
    }
}
