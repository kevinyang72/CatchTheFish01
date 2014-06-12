using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using CatchTheFish.CollectData.Models;

namespace CatchTheFish.CollectData
{
    public class YahooStockDownoad
    {
        public static void GetQuote(ref Dictionary<string, StockQuote> symbolList)
        {
            // Set the return string to null.
            var quoteList = new List<StockQuote>();
            try
            {
                // Use Yahoo finance service to download stock data from Yahoo
                var symbols = "";
                foreach (var symbol in symbolList.Keys)
                    symbols += symbols + ",";
                string yahooURL = @"http://download.finance.yahoo.com/d/quotes.csv?s=" +
                                  symbols + "&f=,l1c6k2oghjkva2j1";
                
                // Initialize a new WebRequest.
                HttpWebRequest webreq = (HttpWebRequest)WebRequest.Create(yahooURL);
                // Get the response from the Internet resource.
                HttpWebResponse webresp = (HttpWebResponse)webreq.GetResponse();
                // Read the body of the response from the server.
                
                using(var strm = new StreamReader(webresp.GetResponseStream(), Encoding.ASCII))
                {
                    // Construct a XML in string format.
                    
                    foreach(var symbol in symbolList.Keys)
                    {
                        try
                        {
                            if (symbol.Trim() == "")
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
                                //construct XML via strings.
                                quote.Ticker = contents[0];
                                quote.LastTrade = Convert.ToDecimal(contents[1]);
                                quote.Change = Convert.ToDecimal(contents[2]);
                                quote.ChangePct = contents[3];
                                quote.Open = Convert.ToDecimal(contents[3]);
                                quote.Low = Convert.ToDecimal(contents[4]);
                                quote.High = Convert.ToDecimal(contents[5]);
                                quote.Low52 = Convert.ToDecimal(contents[6]);
                                quote.High52 = Convert.ToDecimal(contents[7]);
                                quote.Volume = Convert.ToDecimal(contents[8]);
                                quote.AvgVolume = Convert.ToDecimal(contents[9]);
                                quote.MarketCapX = contents[10];
                            }
                            symbolList[quote.Ticker] = quote;
                        }catch(Exception ex)
                        { }
                    }
                }
            }
            catch
            {
                // Handle exceptions.
            }
                // Return the stock quote data in XML format.
            return quoteList;
        }        
    }
}
