
using System;
using System.Collections.Generic;
using NUnit.Framework;
using FdkMinimal;
using SoftFX.Extended;
using System.Linq;

namespace FdkMinimal.Tests
{
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	[TestFixture]
	public class TestSimpleConnection
	{
        public static DataTrade Trade
        {
            get { return FdkHelper.Trade; }
        }

        [Test]
        public void TestBasicConnect()
        {
            //FdkHelper.UseLrp = true;
            //FdkHelper.ConnectToFdk("", "", "", "");
            FdkHelper.ConnectToFdk("localhost", "100001", "123qwe!", "");
            
            var tradeRecordsStream = Trade.Server.GetTradeTransactionReports(TimeDirection.Forward, false, null, null)
                .ToArray().ToList();
            var tradeRecordList = tradeRecordsStream.ToArray();
            Assert.IsTrue(tradeRecordList.Length == 0);
        }
	}
}