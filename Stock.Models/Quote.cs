﻿
using System;
using System.ComponentModel;

namespace Stock.Models
{
    public class Quote
    {
        public Quote(string ticker)
        {
            Symbol = ticker;
        }

        [DisplayName("Symbol")]
        public string Symbol { get; set; }
        [DisplayName("Stock Exchange")]
        public string StockExchange { get; set; }
        public decimal? LastTradePrice { get; set; }
        public decimal? ChangeInPercent { get; set; }
        public decimal? PreviousClose { get; set; }
        public decimal? Open { get; set; }
        public decimal? Volume { get; set; }
        public decimal? YearlyHigh { get; set; }
        public decimal? YearlyLow { get; set; }
        public decimal? DailyHigh { get; set; }
        public decimal? DailyLow { get; set; }
        public decimal? PeRatio { get; set; }
        [DisplayName("% Change From 200 Ave")]
        public decimal? PercentChangeFromTwoHundredDayMovingAverage { get; set; }
        public DateTime? DividendPayDate { get; set; }

        public decimal? DividendYield { get; set; }


        public decimal? OneYearPriceTarget { get; set; }

        public decimal? ShortRatio { get; set; }


        public decimal? PriceEpsEstimateNextYear { get; set; }


        public decimal? PriceEpsEstimateCurrentYear { get; set; }


        public decimal? PegRatio { get; set; }


        public DateTime? ExDividendDate { get; set; }


        public decimal? PriceBook { get; set; }


        public decimal? PriceSales { get; set; }

        public string Name { get; set; }

        [DisplayName("% Change From 50 Ave")]
        public decimal? PercentChangeFromFiftyDayMovingAverage { get; set; }

        [DisplayName("Change From 200 Ave")]
        public decimal? ChangeFromTwoHundredDayMovingAverage { get; set; }

        [DisplayName("200 Day Ave")]
        public decimal? TwoHunderedDayMovingAverage { get; set; }

        [DisplayName("50 Day Ave")]
        public decimal? FiftyDayMovingAverage { get; set; }

        public decimal? PercentChangeFromYearHigh { get; set; }


        public decimal? ChangeFromYearHigh { get; set; }


        public decimal? PercentChangeFromYearLow { get; set; }


        public decimal? ChangeFromYearLow { get; set; }


        public decimal? Ebitda { get; set; }


        public decimal? MarketCapitalization { get; set; }


        public decimal? EpsEstimateNextQuarter { get; set; }


        public decimal? EpsEstimateNextYear { get; set; }


        public decimal? EpsEstimateCurrentYear { get; set; }


        public decimal? EarningsShare { get; set; }


        public DateTime? LastTradeDate { get; set; }


        public decimal? DividendShare { get; set; }


        public decimal? Change { get; set; }


        public decimal? ChangePercent { get; set; }


        public decimal? BookValue { get; set; }


        public decimal? Ask { get; set; }


        public decimal? Bid { get; set; }


        public decimal? AverageDailyVolume { get; set; }

         
         public DateTime LastUpdate { get; set; }
    }
}