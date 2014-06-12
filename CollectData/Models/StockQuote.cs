using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatchTheFish.CollectData.Models
{
    public class StockQuote
    {
        public string Ticker {get;set;}// Stock ticker.
	    public decimal LastTrade {get;set;}  // l1: last trade.
	    public decimal Change {get;set;}  // c6: change real time.
	    public string ChangePct {get;set;}  // k2: percent change real time.
	    public decimal Open {get;set;} // o: market open price.
	    public decimal Low {get;set;}  // g: day's low.
	    public decimal High {get;set;} // h: day's high.
	    public decimal Low52 {get;set;}  // j: 52-weeks low.
	    public decimal High52 {get;set;}  // k: 52-weeks high.
	    public decimal Volume {get;set;}  // v: volume.
	    public decimal AvgVolume {get;set;}  // a2: average volume.
	    public string MarketCapX {get;set;}  // j1: market cap (fallback when real time is N/A).
    }
}
