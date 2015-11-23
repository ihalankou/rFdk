using System;
using System.Linq;
using FdkMinimal;
using SoftFX.Extended;
using SoftFX.Extended.Storage;
using log4net;
using System.Diagnostics;
using System.Collections.Generic;

namespace RHost
{
	public static class FdkBars
	{
		static readonly ILog Log = LogManager.GetLogger(typeof(FdkBars));
		#region Bars
		
		//10 million
		public const int HugeCount = 10000000;


        public static string ComputeBarsRangeTime(string symbol, string priceTypeStr, string barPeriodStr,
            DateTime startTime, DateTime endTime, double barCountDbl)
		{
            Stopwatch stopWatch = Stopwatch.StartNew();
			try
			{
				var barPeriod = FdkHelper.GetFieldByName<BarPeriod>(barPeriodStr);
				if (barPeriod == null)
					return string.Empty;

                if(priceTypeStr == "BidAsk")
                {
                    return CombinedBarsRangeTime(symbol, barPeriod, startTime, endTime, barCountDbl);
                }

				var priceType = FdkHelper.ParseEnumStr<PriceType>(priceTypeStr);
				if (priceType == null)
					return string.Empty;

				Log.InfoFormat("FdkBars.ComputeBarsRangeTime( symbol: {0}, barPeriod: {1}, startTime: {2}, endTime: {3}, barCount: {4})",
					symbol, barPeriodStr, startTime, endTime, barCountDbl);
				
				Bar[] barsData;
				if (FdkHelper.IsTimeZero(startTime))
				{
					var barCount = (int) barCountDbl;
					if(barCount == 0)
					{
						barCount = HugeCount;
					}
					barsData = CalculateBarsForSymbolArray(symbol, priceType.Value, endTime, barPeriod, barCount);
				}else
				{
					barsData = CalculateBarsForSymbolArrayRangeTime(symbol, priceType.Value, startTime, endTime, barPeriod);
				}

                Log.InfoFormat("Elapsed time: {0} ms for {1} items", stopWatch.ElapsedMilliseconds, barsData.Length);

                var bars = FdkVars.RegisterVariable(barsData, "bars");
				return bars;
			}
			catch(Exception ex)
			{
				Log.Error(ex);
				throw;
			}
        }

        private static string CombinedBarsRangeTime(string symbol, BarPeriod barPeriod, DateTime startTime, DateTime endTime, double barCountDbl)
        {
            var isTimeZero =FdkHelper.IsTimeZero(startTime) ;


            Bar[] barsDataBid;
            Bar[] barsDataAsk;
            if (FdkHelper.IsTimeZero(startTime))
            {
                var barCount = (int)barCountDbl;
                if (barCount == 0)
                {
                    barCount = HugeCount;
                }
                barsDataAsk = CalculateBarsForSymbolArray(symbol, PriceType.Ask, endTime, barPeriod, barCount);
                barsDataBid = CalculateBarsForSymbolArray(symbol, PriceType.Bid, endTime, barPeriod, barCount);
            }
            else
            {
                barsDataAsk = CalculateBarsForSymbolArrayRangeTime(symbol, PriceType.Ask, startTime, endTime, barPeriod);
                barsDataBid = CalculateBarsForSymbolArrayRangeTime(symbol, PriceType.Bid, startTime, endTime, barPeriod);
            }
            var barsData = ProcessedBarsResult(barsDataBid, barsDataAsk);

            var bars = FdkVars.RegisterVariable(barsData, "bars");
            return bars;
        }

        private static object ProcessedBarsResult(Bar[] barsDataBid, Bar[] barsDataAsk)
        {
            var maxCount = Math.Max(barsDataBid.Length, barsDataAsk.Length);
            var resultBars = new List<Bar>(maxCount*2);
            var positionAsk = 0;
            var positionBid = 0;
            Bar previousAsk = new Bar(DateTime.Now, DateTime.Now, 0,0,0,0,0);
            Bar previousBid = previousAsk;
            while (positionAsk<barsDataAsk.Length && positionBid < barsDataBid.Length)
            {
                Bar barAsk = barsDataAsk[positionAsk];
                var barBid = barsDataBid[positionBid];
                if (barAsk.From == barBid.From)
                {
                    resultBars.Add(barBid);
                    resultBars.Add(barAsk);
                    positionAsk++;
                    positionBid++;
                }
                else if (barAsk.From < barBid.From)
                {
                    //Add undefined bid bar with times of Ask bar
                    AddBarUndefined(resultBars, barAsk.From, barAsk.To, previousAsk);
                    resultBars.Add(barAsk);
                    positionAsk++;
                }
                else if (barAsk.From > barBid.From)
                {
                    resultBars.Add(barBid);
                    //Add undefined ask bar with times of Bid bar
                    AddBarUndefined(resultBars, barBid.From, barBid.To, previousBid);
                    positionBid++;
                }
                else
                    throw new InvalidOperationException("This case should never be hit!");
                previousAsk = barAsk;
                previousBid = barBid;
            }
            if (positionBid < barsDataBid.Length)
            {
                CopyRange(resultBars, barsDataBid, positionBid);
            }
            if (positionAsk < barsDataAsk.Length)
            {
                CopyRange(resultBars, barsDataAsk, positionAsk);
            }

            return resultBars.ToArray();
        }

        private static void AddBarUndefined(List<Bar> resultBars, DateTime from, DateTime to, Bar previous)
        {
            var undefinedBar = new Bar(from, to,
                open: previous.Open, close: previous.Close,
                low: previous.Low, high: previous.High,
                volume: previous.Volume
                );
            resultBars.Add(undefinedBar);
        }

        static void CopyRange(List<Bar> resultBars, Bar[] barsData, int position)
        {
            for (var i = position; i < barsData.Length; i++)
            {
                resultBars.Add(barsData[i]);
            }
        }

        #region Fdk direct wrapper
        static Bar[] CalculateBarsForSymbolArray(
			string symbol, PriceType priceType, DateTime startTime, BarPeriod barPeriod, int barCount)
		{
			return FdkHelper.Storage.Online.GetBars(symbol, priceType, barPeriod, startTime, -barCount).ToArray();
		}

		static Bar[] CalculateBarsForSymbolArrayRangeTime(
			string symbol, PriceType priceType, DateTime startTime, DateTime endTime, BarPeriod barPeriod)
		{
			return FdkHelper.Storage.Online.GetBars(symbol, priceType, barPeriod, startTime, endTime).ToArray();
		}

		static HistoryInfo GetQuotesInfo(string symbol, int depth)
		{
			return FdkHelper.Storage.Online.GetQuotesInfo(symbol, depth);
		}

		static HistoryInfo GetBarsInfo(string symbol, PriceType priceType, BarPeriod period)
		{
			return FdkHelper.Storage.Online.GetBarsInfo(symbol, priceType, period);
		}

		#endregion

        public static DateTime[] ComputeGetQuotesInfo(string symbol, int depth)
        {
            var barsData = GetQuotesInfo(symbol, depth);
            var bars = new[]
            {
                barsData.AvailableFrom,
                barsData.AvailableTo
            };
            return bars;
        }

        public static DateTime[] ComputeGetBarsInfo(string symbol, string priceTypeStr, string barPeriodStr)
        {
            var barPeriod = FdkHelper.GetFieldByName<BarPeriod>(barPeriodStr);
            if (barPeriod == null)
                return new DateTime[0];
            var priceType = FdkHelper.ParseEnumStr<PriceType>(priceTypeStr);
            if (priceType == null)
                return new DateTime[0];
            var barsData = GetBarsInfo(symbol, priceType.Value, barPeriod);
            var bars = new[]
            {
                barsData.AvailableFrom,
                barsData.AvailableTo
            };
            return bars;
        }



        public static string ComputeGetPairBarsRange(string symbol, string barPeriodStr, DateTime startTime, DateTime endTime)
        {
            var barPeriod = FdkHelper.GetFieldByName<BarPeriod>(barPeriodStr);
            if (barPeriod == null)
                return string.Empty;
            var barsData = FdkBarPairs.GetPairBarsSymbolArrayRangeTime(symbol, barPeriod, startTime, endTime);
            var bars = FdkVars.RegisterVariable(barsData, "barPairs");
            return bars;
        }

        #endregion

        #region Bar fields
        public static double[] BarHighs(string bars)
        {
            var barData = FdkVars.GetValue<Bar[]>(bars);

            return GetBarsHigh(barData);
        }


        public static double[] BarLows(string bars)
        {
            var barData = FdkVars.GetValue<Bar[]>(bars);

            return GetBarsLow(barData);
        }

        public static double[] BarVolumes(string bars)
        {
            var barData = FdkVars.GetValue<Bar[]>(bars);

            return GetBarsVolume(barData);
        }

        public static double[] BarOpens(string bars)
        {
            var barData = FdkVars.GetValue<Bar[]>(bars);

            return GetBarsOpen(barData);
        }

        public static double[] BarCloses(string bars)
        {
            var barData = FdkVars.GetValue<Bar[]>(bars);

            return GetBarsClose(barData);
        }

        public static DateTime[] BarFroms(string bars)
        {
            var barData = FdkVars.GetValue<Bar[]>(bars);

            return GetBarsFrom(barData);
        }

        public static DateTime[] BarTos(string bars)
        {
            var barData = FdkVars.GetValue<Bar[]>(bars);

            return GetBarsTo(barData);
        }


        public static double[] GetBarsHigh(Bar[] barData)
        {
            return barData.SelectToArray(b => b == null ? 0.0 : b.High);
        }

        public static double[] GetBarsLow(Bar[] barData)
        {
            return barData.SelectToArray(b => b == null ? 0.0 : b.Low);
        }

        public static double[] GetBarsVolume(Bar[] barData)
        {
            return barData.SelectToArray(b => b == null ? 0.0 : b.Volume);
        }

        public static double[] GetBarsOpen(Bar[] barData)
        {
            return barData.SelectToArray(b => b == null ? 0.0 : b.Open);
        }

        public static double[] GetBarsClose(Bar[] barData)
        {
            return barData.SelectToArray(b => b == null ? 0.0 : b.Close);
        }


        internal static DateTime[] GetBarsFrom(Bar[] barData)
        {
            return barData.SelectToArray(b => b.From.AddUtc());
        }

        internal static DateTime[] GetBarsTo(Bar[] barData)
        {
            return barData.SelectToArray(b =>  b.To.AddUtc());
        }
        #endregion
    }
}