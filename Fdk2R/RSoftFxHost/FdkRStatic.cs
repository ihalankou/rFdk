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
            return FdkHelper.ConnectToFdk(address, login, password, path);
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
