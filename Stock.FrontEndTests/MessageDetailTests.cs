using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.FrontEnd;
using NUnit.Framework;
using Stock.Models;

namespace Stock.FrontEnd.Tests
{
    [TestFixture()]
    public class MessageDetailTests
    {
        [Test()]
        public void GetMessageDetailTest()
        {
            var quote = new Quote("SPPI");
            quote.Symbol = "SPPI";
            quote.LastTradePrice = 11.00M;
            var result = MessageDetail.GetMessageDetail(quote);
            Assert.Fail();
        }
    }
}
