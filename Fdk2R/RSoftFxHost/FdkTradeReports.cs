using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SoftFX.Extended;
using SoftFX.Extended.Reports;

namespace RHost
{
    public static class FdkTradeReports
    {
        private static DataTrade Trade
        {
            get { return FdkHelper.Wrapper.ConnectLogic.TradeWrapper.Trade; }
        }

        public static string GetTradeTransactionReport(DateTime from, DateTime to)
        {
            List<TradeTransactionReport> tradeRecordsStream = Trade.Server.GetTradeTransactionReports(TimeDirection.Forward, false, from, to)
                .ToArray().ToList();
            var tradeRecordList = tradeRecordsStream.ToArray();

            var varName = FdkVars.RegisterVariable(tradeRecordList, "trades");
            return varName;
        }


        public static double[] GetTradeAccountBalance(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.AccountBalance).ToArray();
        }

        public static double[] GetTradeAgentCommission(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.AgentCommission).ToArray();
        }

        public static string[] GetTradeClientId(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.ClientId).ToArray();
        }


        public static double[] GetTradeCloseConversionRate(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.CloseConversionRate ?? -1.0).ToArray();
        }

        public static string[] GetTradeInitialVolume(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.CommCurrency).ToArray();
        }

        public static string[] GetTradeComment(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.Comment).ToArray();
        }

        public static double[] GetTradeCommission(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.Commission).ToArray();
        }

        public static string[] GetTradeId(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.Id).ToArray();
        }

        public static double[] GetTradeLeavesQuantity(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.LeavesQuantity).ToArray();
        }


        public static double[] GetTradeOpenConversionRate(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.OpenConversionRate ?? -1).ToArray();
        }


        public static DateTime[] GetTradeOrderCreated(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.OrderCreated).ToArray();
        }

        public static double[] GetTradeOrderFillPrice(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.OrderFillPrice ?? -1).ToArray();
        }

        public static double[] GetTradeOrderLastFillAmount(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.OrderLastFillAmount ?? -1).ToArray();
        }

        public static DateTime[] GetTradeOrderModified(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.OrderModified).ToArray();
        }

        public static double[] GetTradePosOpenPrice(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.PosOpenPrice).ToArray();
        }

        public static double[] GetTradePositionClosePrice(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.PositionClosePrice).ToArray();
        }

        public static double[] GetTradePositionCloseRequestedPrice(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.PositionCloseRequestedPrice).ToArray();
        }


        public static DateTime[] GetTradePositionClosed(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.PositionClosed).ToArray();
        }

        public static string[] GetTradePositionId(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.PositionId).ToArray();
        }

        public static double[] GetTradePositionLastQuantity(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.PositionLastQuantity).ToArray();
        }



        public static double[] GetTradePositionLeavesQuantity(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.PositionLeavesQuantity).ToArray();
        }


        public static DateTime[] GetTradePositionModified(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.PositionModified).ToArray();
        }


        public static DateTime[] GetTradePositionOpened(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.PositionOpened).ToArray();
        }


        public static double[] GetTradePositionQuantity(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.PositionQuantity).ToArray();
        }


        public static double[] GetTradePrice(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.Price).ToArray();
        }


        public static double[] GetTradeQuantity(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.Quantity).ToArray();
        }

        public static double[] GetTradeStopLoss(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.StopLoss).ToArray();
        }

        public static double[] GetTradeStopPrice(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.StopPrice).ToArray();
        }

        public static double[] GetTradeSwap(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.Swap).ToArray();
        }

        public static string[] GetTradeSymbol(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.Symbol).ToArray();
        }

        public static double[] GetTradeTakeProfit(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.TakeProfit).ToArray();
        }

        public static string[] GetTradeTradeRecordSide(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.TradeRecordSide.ToString()).ToArray();
        }

        public static string[] GetTradeTradeRecordType(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.TradeRecordType.ToString()).ToArray();
        }

        public static string[] GetTradeTradeTransactionReason(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.TradeTransactionReason.ToString()).ToArray();
        }

        public static string[] GetTradeTradeTransactionReportType(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.TradeTransactionReportType.ToString()).ToArray();
        }

        public static double[] GetTradeTransactionAmount(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.TransactionAmount).ToArray();
        }

        public static string[] GetTradeTransactionCurrency(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.TransactionCurrency).ToArray();
        }

        public static DateTime[] GetTradeTransactionTime(string varName)
        {
            var tradeData = FdkVars.GetValue<TradeTransactionReport[]>(varName);
            return tradeData.Select(it => it.TransactionTime).ToArray();
        }
    }
}