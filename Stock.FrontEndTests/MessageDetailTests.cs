using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.FrontEnd;
using NUnit.Framework;
using Stock.Models;
using Core.Messaging;

namespace Stock.FrontEnd.Tests
{
    [TestFixture()]
    public class MessageDetailTests
    {
        [Test()]
        public void GetMessageDetailTest()
        {
            var quote = new Quote("SPPI");
            var listQuotes = new List<Quote>();
            listQuotes.Add(quote);
            Stock.StockFetcher.YahooStockEngine.Fetch(listQuotes);
            var result = MessageDetail.GetMessageDetail(listQuotes.FirstOrDefault());
            Messaging.SendEmailGmail("Stock alert", result);
            //Assert.Fail();
        }
    }
}
