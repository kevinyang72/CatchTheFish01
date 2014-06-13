using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.StockAnalyzer
{
    public class StockAnalyzerResult
    {
        public bool IsVolumeAbnormal { get; set; }
        public bool IsPriceChangedDramatically { get; set; }
        public bool IsPrice52WeeksLow { get; set; }
    }
}
