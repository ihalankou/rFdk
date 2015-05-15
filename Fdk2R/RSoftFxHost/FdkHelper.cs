﻿using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using SharedFdkFunctionality;
using SoftFX.Extended;

namespace RHost
{
    public class FdkHelper
    {
        public  static void TestInvoke()
        {
            MessageBox.Show("SoftFX integration is working");
        }

        public static int ConnectToFdk(string address, string login, string password, string path)
        {
#if DEBUG
            //Library.Path = @"C:\Users\ciprian.khlud\Documents\R\win-library\3.2\FdkRLib\data";
#endif
            var addr = string.IsNullOrEmpty(address)
                ? "tpdemo.fxopen.com"
                : address;
            var loginStr = string.IsNullOrEmpty(login)
                ? "59932"
                : login;
            var passwordString = string.IsNullOrEmpty(login)
                ? "8mEx7zZ2"
                : password;
            if (Wrapper == null)
            {
                var wrapper = new FdkWrapper()
                {
                    Address = addr,
                    Login = loginStr,
                    Password = passwordString,

                };
                Wrapper = wrapper;
                FdkBars.Wrapper = Wrapper;
            }

            string localPath = string.Empty;

            if (!String.IsNullOrEmpty(path))
            {
                var localPathInfo = new DirectoryInfo(path);
                localPath = localPathInfo.FullName;
            }
            if (Wrapper.Connect(localPath))
            {
                return 0;
            }
            return -1;
        }

        public static FdkWrapper Wrapper { get; set; }
    }
}
 