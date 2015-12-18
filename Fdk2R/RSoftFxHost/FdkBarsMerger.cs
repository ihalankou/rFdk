using System;
using System.Collections.Generic;
using System.Linq;
using SoftFX.Extended;

namespace RHost
{
    static class FdkBarsMerger
    {


        internal static Bar[] ProcessedBarsResult(Bar[] barsDataBid, Bar[] barsDataAsk)
        {
            var maxCount = Math.Max(barsDataBid.Length, barsDataAsk.Length);
            var resultBars = new List<Bar>(maxCount * 2);
            var positionAsk = 0;
            var positionBid = 0;
            var nowTime = DateTime.UtcNow;
            Bar previousAsk = new Bar(nowTime, nowTime,
                double.NaN, double.NaN, double.NaN, double.NaN, double.NaN);
            Bar previousBid = new Bar(nowTime, nowTime,
                double.NaN, double.NaN, double.NaN, double.NaN, double.NaN);
            while (positionAsk < barsDataAsk.Length && positionBid < barsDataBid.Length)
            {
                Bar barAsk = barsDataAsk[positionAsk];
                var barBid = barsDataBid[positionBid];
                if (barAsk.From == barBid.From)
                {
                    AddBarPairs(resultBars, barBid, barAsk);
                    positionAsk++;
                    positionBid++;
                }
                else if (barAsk.From < barBid.From)
                {
                    //Add undefined bid bar with times of Ask bar
                    var askUndefined = CalculateBarUndefined(barAsk.From, barAsk.To, previousAsk);

                    AddBarPairs(resultBars, barBid, askUndefined);
                    
                    positionAsk++;
                }
                else if (barAsk.From > barBid.From)
                {
                    //Add undefined ask bar with times of Bid bar
                    var bidUndefined = CalculateBarUndefined(barBid.From, barBid.To, previousBid);
                    AddBarPairs(resultBars, bidUndefined, barAsk);
                    
                    positionBid++;
                }
                else
                    throw new InvalidOperationException("This case should never be hit!");
                previousAsk = barAsk;
                previousBid = barBid;
            }
            if (positionBid < barsDataBid.Length)
            {
                CopyRange(resultBars, barsDataBid, positionBid, previousAsk, isUndefinedBid: false);
            }
            if (positionAsk < barsDataAsk.Length)
            {
                CopyRange(resultBars, barsDataAsk, positionAsk, previousBid, isUndefinedBid: true);
            }

            return resultBars.ToArray();
        }

        private static void AddBarPairs(List<Bar> resultBars, Bar barBid, Bar barAsk)
        {
            resultBars.Add(barBid);
            resultBars.Add(barAsk);
        }

        static void CopyRange(List<Bar> resultBars, Bar[] barsData, int position, Bar undefinedBar, bool isUndefinedBid)
        {
            for (var i = position; i < barsData.Length; i++)
            {
                if (isUndefinedBid)
                {
                    AddBarPairs(resultBars, undefinedBar, barsData[i]);
                }
                else
                {
                    AddBarPairs(resultBars, barsData[i], undefinedBar);
                }
            }
        }

        private static Bar CalculateBarUndefined(DateTime from, DateTime to, Bar previous)
        {
            var undefinedBar = new Bar(from, to,
                open: previous.Open, close: previous.Close,
                low: previous.Low, high: previous.High,
                volume: previous.Volume
                );
            return undefinedBar;
        }
    }
}