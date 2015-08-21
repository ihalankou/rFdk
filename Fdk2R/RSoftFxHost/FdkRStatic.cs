using FdkMinimal.Facilities;
using SoftFX.Extended.Financial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHost
{
    public static class FdkStatic
    {
        public static int ConnectToFdk(string address, string login, string password, string path)
        {
            var result = FdkHelper.ConnectToFdk(address, login, password, path);
            if(result == 0)
            {
                var symbolInfoDic = FdkHelper.Wrapper.GetSymbolsDict();
                var symbolInfoList = symbolInfoDic.Values.ToList();
                SetRatesOfCurrentTime rates = new SetRatesOfCurrentTime(symbolInfoList, Calculator);
            }
            return result;
        }
        public static void Disconnect()
        {
            FdkHelper.Disconnect();
        }

        public static void DisplayDate(DateTime time)
        {
            FdkHelper.DisplayDate(time);
        }

        static FinancialCalculator Calculator { get; set; }

        static FdkStatic()
        {
            Calculator = new FinancialCalculator();

        }

    }
}
