using System;
using System.Linq;
using log4net;
using SoftFX.Extended;

namespace RHost
{
	public class FdkSymbolInfo
	{
		public static string GetSymbolInfos()
		{
			try
			{
				var symbolInfos = FdkHelper.Wrapper.ConnectLogic.Feed.Cache.Symbols;
				var varName = FdkVars.RegisterVariable(symbolInfos, "symbolsInfo");
				return varName;
			}
			catch (Exception ex)
			{
				Log.Error(ex);
				throw;
			}     
        }
        static readonly ILog Log = LogManager.GetLogger(typeof(FdkSymbolInfo));

        public static double[] GetSymbolComission(string symbolsInfo)
        {
            var symbolInfos = FdkVars.GetValue<SymbolInfo[]>(symbolsInfo);
            return symbolInfos.SelectToArray(b => b.Commission);
        }

		public static double[] GetSymbolContractMultiplier(string symbolsInfo)
        {
            var symbolInfos = FdkVars.GetValue<SymbolInfo[]>(symbolsInfo);
            return symbolInfos.SelectToArray(b => b.ContractMultiplier);
        }

        public static string[] GetSymbolCurrency(string symbolsInfo)
        {
            var symbolInfos = FdkVars.GetValue<SymbolInfo[]>(symbolsInfo);
            return symbolInfos.SelectToArray(b => b.Currency);
        }
        public static double[] GetSymbolLimitsCommission(string symbolsInfo)
        {
            var symbolInfos = FdkVars.GetValue<SymbolInfo[]>(symbolsInfo);
            return symbolInfos.SelectToArray(b => b.LimitsCommission);
        }
        public static double[] GetSymbolMaxTradeVolume(string symbolsInfo)
        {
            var symbolInfos = FdkVars.GetValue<SymbolInfo[]>(symbolsInfo);
            return symbolInfos.SelectToArray(b => b.MaxTradeVolume);
        }
        public static double[] GetSymbolMinTradeVolume(string symbolsInfo)
        {
            var symbolInfos = FdkVars.GetValue<SymbolInfo[]>(symbolsInfo);
            return symbolInfos.SelectToArray(b => b.MinTradeVolume);
        }
        public static string[] GetSymbolName(string symbolsInfo)
        {
            var symbolInfos = FdkVars.GetValue<SymbolInfo[]>(symbolsInfo);
            return symbolInfos.SelectToArray(b => b.Name);
        }
        public static double[] GetSymbolPrecision(string symbolsInfo)
        {
            var symbolInfos = FdkVars.GetValue<SymbolInfo[]>(symbolsInfo);
            return symbolInfos.SelectToArray(b => (double)b.Precision);
        }
        public static double[] GetRoundLot(string symbolsInfo)
        {
            var symbolInfos = FdkVars.GetValue<SymbolInfo[]>(symbolsInfo);
            return symbolInfos.SelectToArray(b => b.RoundLot);
        }
        public static string[] GetSymbolSettlementCurrency(string symbolsInfo)
        {
            var symbolInfos = FdkVars.GetValue<SymbolInfo[]>(symbolsInfo);
            return symbolInfos.SelectToArray(b => b.SettlementCurrency);
        }
        public static double[] GetSymbolSwapSizeLong(string symbolsInfo)
        {
            var symbolInfos = FdkVars.GetValue<SymbolInfo[]>(symbolsInfo);
            return symbolInfos.SelectToArray(b => b.SwapSizeLong ?? double.NaN);
        }

        public static double[] GetSymbolSwapSizeShort(string symbolsInfo)
        {
            var symbolInfos = FdkVars.GetValue<SymbolInfo[]>(symbolsInfo);
            return symbolInfos.SelectToArray(b => b.SwapSizeShort ?? double.NaN);
        }
    }
}

