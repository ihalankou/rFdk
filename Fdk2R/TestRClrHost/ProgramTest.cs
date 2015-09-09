
using System;
using FdkMinimal;
using RHost;

namespace TestRClrHost
{
	/// <summary>
	/// Description of ProgramTest.
	/// </summary>
	public class ProgramTest
	{
		public static void Main()
		{
			FdkHelper.ConnectToFdk("localhost", "100001", "123qwe!", "");
            var bars = FdkTradeReports.GetTradeTransactionReportAll();
            var comission = FdkTradeReports.GetTradeComment(bars);
            FdkVars.Unregister(bars);
		}
	}
}
