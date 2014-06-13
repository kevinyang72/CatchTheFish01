using Stock.Models;

namespace Stock.StockAnalyzer
{
    public class StockAnalyzer
    {
        public static StockAnalyzerResult AnalyzeStock(Quote quote)
        {
            var result = new StockAnalyzerResult
                {
                    IsPriceChangedDramatically = PriceAnalyzer.IsPriceChangedDramatically(quote),
                    IsVolumeAbnormal = VolumeAnalyzer.IsVolumeAbnormal(quote),
                    IsPrice52WeeksLow = PriceAnalyzer.IsPrice52WeeksLow(quote)
                };
            return result;
        }
    }
}
