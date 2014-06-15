using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using Stock.Models;

namespace Stock.StockFetcher
{
    public class YahooStockDownoader
    {
        public static void GetQuote(List<Quote> quotes)
        {
            // Set the return string to null.
            var quoteList = new List<StockQuote>();
            try
            {
                // Use Yahoo finance service to download stock data from Yahoo
                var symbols = "";
                foreach (var symbol in quotes)
                    symbols += symbol.Symbol + ",";
                string yahooURL = @"http://download.finance.yahoo.com/d/quotes.csv?s=" +
                                  symbols + "&f=,l1c6k2oghjkva2j1";
                
                // Initialize a new WebRequest.
                HttpWebRequest webreq = (HttpWebRequest)WebRequest.Create(yahooURL);
                // Get the response from the Internet resource.
                HttpWebResponse webresp = (HttpWebResponse)webreq.GetResponse();
                // Read the body of the response from the server.
                var quoteDict = new Dictionary<string, Quote>();
                using(var strm = new StreamReader(webresp.GetResponseStream(), Encoding.ASCII))
                {
                    // Construct a XML in string format.
                    foreach (var item in quotes)
                    {
                        try
                        {
                            if (item.Symbol.Trim() == "")
                                continue;
                            var quote = new StockQuote();
                            var content = strm.ReadLine().Replace("\"", "");
                            string[] contents = content.ToString().Split(',');
                            // If contents[2] = "N/A". the stock symbol is invalid.
                            if (contents[2] == "N/A")
                            {
                                continue;
                            }
                            else
                            {
                                var quoteFromDownloader = new Quote(contents[0]);
                                
                                quoteFromDownloader.Symbol = contents[0];
                                quoteFromDownloader.YearlyLow = Convert.ToDecimal(contents[6]);
                                quoteFromDownloader.YearlyHigh = Convert.ToDecimal(contents[7]);
                                quoteDict.Add(quoteFromDownloader.Symbol, quoteFromDownloader);
                            }
                        }catch(Exception ex)
                        { }
                    }
                }
                foreach(var item in quotes)
                {
                    if(quoteDict.ContainsKey(item.Symbol))
                    {
                        item.Price52WeeksLow = quoteDict[item.Symbol].Price52WeeksLow;
                        item.Price52WeeksHigh = quoteDict[item.Symbol].Price52WeeksHigh;
                    }
                }
            }
            catch
            {
                // Handle exceptions.
            }
                // Return the stock quote data in XML format.
        }        
    }
}
