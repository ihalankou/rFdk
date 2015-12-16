using log4net;
using FdkMinimal;
using SoftFX.Extended;
using SoftFX.Extended.Financial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FdkMinimal.Facilities
{
    public class SetRatesOfCurrentTime
    {
        readonly List<SymbolInfo> _symbolInfoDic;
        FinancialCalculator _calculator;
        readonly HashSet<string> _currencies;
        Dictionary<string, SymbolInfo> _symbols;

        ILog Log = LogManager.GetLogger(typeof(SetRatesOfCurrentTime));

        public Dictionary<string, SymbolInfo> Symbols
        {
            get
            {
                return _symbols;
            }

            set
            {
                _symbols = value;
            }
        }

        public SetRatesOfCurrentTime(List<SymbolInfo> symbolInfoDic, FinancialCalculator calculator)
        {
            _symbolInfoDic = symbolInfoDic;
            _calculator = calculator;
            _currencies = new HashSet<string>();
            Symbols = new Dictionary<string, SymbolInfo>();
            foreach (var sym in symbolInfoDic)
            {
                _currencies.Add(sym.Currency);
                _currencies.Add(sym.SettlementCurrency);
                Symbols[sym.Name] = sym;
            }

            foreach (var curr in _calculator.Currencies)
            {
                _currencies.Remove(curr);
            }

            foreach (var sym in _calculator.Symbols)
            {
                Symbols.Remove(sym.Symbol);
            }
        }

		void SetupSymbolsAndCurrencies()
		{
			foreach (var currName in _currencies) {
				_calculator.Currencies.Add(currName);
			}
			foreach (var sym in Symbols) {
				var symbolValue = sym.Value;
				SymbolEntry symbolEntry = new SymbolEntry(_calculator, sym.Key, symbolValue.SettlementCurrency, symbolValue.Currency);
				symbolEntry.ContractSize = symbolValue.RoundLot;
				symbolEntry.MarginFactor = symbolValue.MarginFactor;
				symbolEntry.Hedging = symbolValue.MarginHedge;
				_calculator.Symbols.Add(symbolEntry);
			}
		}
        public void ProcessUsingBars(DateTime intme)
        {
        	SetupSymbolsAndCurrencies();
        }

        public void Process()
        {
        	SetupSymbolsAndCurrencies();


            PriceEntries priceEntries = _calculator.Prices;

            DataFeed feed = FdkHelper.Feed;
            var server = feed.Server;
            server.SubscribeToQuotes(_symbolInfoDic.Select(sym => sym.Name), 1);
            AutoResetEvent autoResetEvent = new AutoResetEvent(true);
            feed.Tick += (arg, ev) => autoResetEvent.Set();
            autoResetEvent.WaitOne();
            Thread.Sleep(100);

            _symbolInfoDic.Each(sym =>
            {
                int retries = 5;
                double price;
                while (!FdkHelper.Feed.Cache.TryGetBid(sym.Name, out price) && retries > 0)
                {
                    Thread.Sleep(100);
                    retries--;
                }

                try
                {
                    priceEntries.Update(sym.Name, price, price);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception on updating calculator for symbol: {0} exception: {1}", sym.Name, ex);
                }
            });

        }
    }
}
