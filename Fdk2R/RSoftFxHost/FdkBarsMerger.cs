using System;
using System.Collections.Generic;
using System.Linq;
using SoftFX.Extended;
using log4net;

namespace RHost
{
    static class FdkBarsMerger
    {

        static readonly ILog Log = LogManager.GetLogger(typeof(FdkBarsMerger));

        internal static BarData[] ProcessedBarsResult(BarData[] barsDataBid, BarData[] barsDataAsk)
        {
            var maxCount = Math.Max(barsDataBid.Length, barsDataAsk.Length);
            var resultBars = new List<BarData>(maxCount * 2);
           
            BarCursor bidCursor = new BarCursor(barsDataBid);
            BarCursor askCursor = new BarCursor(barsDataAsk);

            while (bidCursor.CanContinue && askCursor.CanContinue)
            {
                BarData barAsk = askCursor.Current;
                BarData barBid = bidCursor.Current;
                if (barAsk.From == barBid.From)
                {
                    AddBarPairs(resultBars, barBid, barAsk);
                    askCursor.Next();
                    bidCursor.Next();
                }
                else if (barAsk.From > barBid.From)
                {
                    //Add undefined bid bar with times of Ask bar
                    BarData askUndefined = CalculateBarUndefined(barBid.From, barBid.To, askCursor.Previous);
                    AddBarPairs(resultBars, barBid, askUndefined);
                    bidCursor.Next();
                }
                else if (barAsk.From < barBid.From)
                {
                    //Add undefined ask bar with times of Bid bar
                    BarData bidUndefined = CalculateBarUndefined(barAsk.From, barAsk.To, bidCursor.Previous);
                    AddBarPairs(resultBars, bidUndefined, barAsk);
                    askCursor.Next();
                }
                else
                    throw new InvalidOperationException("This case should never be hit!");
            }
            if (bidCursor.CanContinue)
            {
                bidCursor.CopyRange(resultBars, askCursor.Previous, isUndefinedBid: false);
            }
            if (askCursor.CanContinue)
            {
                askCursor.CopyRange(resultBars, bidCursor.Previous, isUndefinedBid: true);
            }

            return resultBars.ToArray();
        }

        internal static void AddBarPairs(List<BarData> resultBars, BarData barBid, BarData barAsk)
        {
            resultBars.Add(barBid);
            resultBars.Add(barAsk);
        }

        private static BarData CalculateBarUndefined(DateTime from, DateTime to, BarData previous)
        {
            var undefinedBar = new BarData(from, to,
                open: previous.Open, close: previous.Close,
                low: previous.Low, high: previous.High,
                volume: previous.Volume
                );
            return undefinedBar;
        }
        
    }
}