using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jarloo.CardStock.Models;

namespace CatchTheFish.CollectData
{
    public class StockAnalyzer
    {
        private const int VolumeAbnormalTimes = 5;
        private const int PricePercentageChangeInt = 20;

        public StockAnalyzerResult AnalyzeStock(Quote quote)
        {
            var result = new StockAnalyzerResult();
            result.IsPriceChangedDramatically = IsPriceChangedDramatically(quote);
            result.IsVolumeAbnormal = IsVolumeAbnormal(quote);
            result.IsPrice52WeeksLow = IsPrice52WeeksLow(quote);
            return result;

        }
        private bool IsTheFish(Quote quote)
        {
            if (IsPriceChangedDramatically(quote))
                return true;
            if (IsVolumeAbnormal(quote))
                return true;

            return false;
        }

        private bool IsVolumeAbnormal(Quote quote)
        {
            var averageVolume = quote.AverageDailyVolume;
            var volume = quote.Volume;

            if(volume > averageVolume && averageVolume > 0)
            {
                if (volume / averageVolume > VolumeAbnormalTimes)
                    return true;
            }

            return false;
        }

        private bool IsPriceChangedDramatically(Quote quote)
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

        private bool IsPrice52WeeksLow(Quote quote)
        {
            if (quote.LastTradePrice < quote.YearlyLow)
                return true;
            return false;
        }
    }
}
