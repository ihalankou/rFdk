using SoftFX.Extended;
using SoftFX.Extended.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FdkMinimal.Tests
{
    class Program
    {
        public static DataTradeServer Trade
        {
            get { return FdkHelper.Trade.Server; }
        }

        public static void Main()
        {
            //Library.Path = "<FRE>";
            //FdkHelper.UseLrp = true;
            //FdkHelper.ConnectToFdk("", "", "", "");
            //FdkHelper.ConnectToFdk("localhost", "100001", "123qwe!", "");

            FdkHelper.ConnectToFdk("exchange.tts.st.soft-fx.eu", "100033", "123qwe!", "");

            TradeTransactionReport[] tradeRecordList = GetTrades();
            var bars = FdkHelper.Feed.Server.GetHistoryBars("EURUSD", DateTime.UtcNow, -100, PriceType.Bid, BarPeriod.H1);
            /*
            var trade = tradeRecordList.Last();
            Trade.ModifyTradeRecord("236005", trade.ClientId, trade.Symbol, trade.TradeRecordType, trade.TradeRecordSide, trade.TransactionAmount, newComment: "");
            var tradeRecordList2 = GetTrades();
            */

        }

        private static TradeTransactionReport[] GetTrades()
        {
            var tradeRecordsStream = Trade.GetTradeTransactionReports(
                TimeDirection.Forward, false, null, null)
                .ToArray().ToList();
            var tradeRecordList = tradeRecordsStream.ToArray();
            return tradeRecordList;
        }
    }
}
