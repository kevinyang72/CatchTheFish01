using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Models;

namespace Stock.StockAnalyzer
{
    class PriceAnalyzer
    {
        private const int PricePercentageChangeInt = 20;

        public static bool IsPriceChangedDramatically(Quote quote)
        {
            var price = quote.LastTradePrice;
            var lastClosedPrice = quote.PreviousClose;
            if (lastClosedPrice > price)
            {
                if ((lastClosedPrice - price) * 100 / lastClosedPrice > PricePercentageChangeInt)
                    return true;
            }
            return false;
        }

        public static bool IsPrice52WeeksLow(Quote quote)
        {
            if (quote.LastTradePrice < quote.YearlyLow)
                return true;
            return false;
        }
    }
}
