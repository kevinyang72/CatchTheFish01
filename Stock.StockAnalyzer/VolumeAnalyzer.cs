using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Models;

namespace Stock.StockAnalyzer
{
    class VolumeAnalyzer
    {
        private const int VolumeAbnormalTimes = 5;

        public static bool IsVolumeAbnormal(Quote quote)
        {
            var averageVolume = quote.AverageDailyVolume;
            if (averageVolume < 300000)
                return false; 
            var volume = quote.Volume;

            if (volume > averageVolume && averageVolume > 0)
            {
                if (volume / averageVolume > VolumeAbnormalTimes)
                    return true;
            }

            return false;
        }
    }
}
