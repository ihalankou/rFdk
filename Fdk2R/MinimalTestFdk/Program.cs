using RHost;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalTestFdk
{
    class Program
    {
        static void Main(string[] args)
        {
            FdkStatic.ConnectToFdk("", "", "", "");
            var calculator = FdkStatic.Calculator;
            var symbols = FdkSymbolInfo.Symbols;
            var symFirst = symbols.First(sym => sym.Name == "EURUSD");
            Debugger.Launch();
            //FdkSymbolInfo.RegisterToFeed(FdkSymbolInfo.Feed, calculator);
            //Thread.Sleep(1000);
            double volumeByHand = FdkSymbolInfo.CalculatePipsValue(symFirst);
        }
    }
}
