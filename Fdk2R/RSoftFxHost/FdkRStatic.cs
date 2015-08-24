using FdkMinimal.Facilities;
using log4net.Config;
using SoftFX.Extended.Financial;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RHost
{
    public static class FdkStatic
    {

        static FinancialCalculator Calculator { get; set; }
        static FdkStatic()
        {
            Calculator = new FinancialCalculator();
            SetupLog4Net();
        }

        static void SetupLog4Net()
        {
            Console.WriteLine("Logging in current folder: '{0}'", Directory.GetCurrentDirectory());
            // Configure log4net.
            var locateFileInfo = new FileInfo(Assembly.GetExecutingAssembly().Location);
            var parentPath = new FileInfo(Path.Combine(locateFileInfo.DirectoryName, "app.config"));
            XmlConfigurator.Configure(parentPath);
        }

        public static int ConnectToFdk(string address, string login, string password, string path)
        {
            Console.WriteLine("Connecting ... ");
            var result = FdkHelper.ConnectToFdk(address, login, password, path);
            if(result == 0)
            {
                var symbolInfoDic = FdkHelper.Wrapper.GetSymbolsDict();
                var symbolInfoList = symbolInfoDic.Values.ToList();
                SetRatesOfCurrentTime rates = new SetRatesOfCurrentTime(symbolInfoList, Calculator);
            }
            Console.WriteLine("Done");
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

    }
}
