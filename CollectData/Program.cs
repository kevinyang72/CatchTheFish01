using Core.Log;
using System;

namespace CatchTheFish.CollectData
{
    class Program
    {
        static void Main()
        {
            StaticLog.Info("CollectData Start");
            CollectDataManager.ScanStocks();
            StaticLog.Info("CollectData End");
        }
    }
}
