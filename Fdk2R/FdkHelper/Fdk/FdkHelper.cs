using log4net;
using SoftFX.Extended;
using SoftFX.Extended.Storage;
using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace FdkMinimal
{
    public static class FdkHelper
	{
        public const string DefaultAddress = "ttlive.fxopen.com";
        public const string DefaultLogin = "100";
        public const string DefaultPassword = "TTqfdeppmhDR";
		static readonly ILog Log = LogManager.GetLogger(typeof(FdkHelper));
        static FdkHelper()
        {
            Wrapper = new FdkWrapper();
        }

        public static int ConnectToFdk(string address, string login, string password, string path)
        {
#if DEBUG
            //Library.Path = @"C:\Users\ciprian.khlud\Documents\R\win-library\3.2\FdkRLib\data";
            
#endif

            Log.InfoFormat("FdkHelper.ConnectToFdk( address: {0}, login: {1}, password: ?????, path: {2})",
                address, login, path);
            //Debugger.Launch();

            var addr = String.IsNullOrEmpty(address) ? DefaultAddress : address;
            var loginStr = String.IsNullOrEmpty(login) ? DefaultLogin : login;
            var passwordString = String.IsNullOrEmpty(password) ? DefaultPassword : password;

            Address = addr;
            Login = loginStr;
            Password = passwordString;
            return Reconnect(path);
        }

        public static int Reconnect(string path)
        {
            Wrapper.Address = String.IsNullOrEmpty(Wrapper.Address) ? DefaultAddress : Wrapper.Address;
            Wrapper.Login = String.IsNullOrEmpty(Wrapper.Login) ? DefaultLogin : Wrapper.Login;
            Wrapper.Password = String.IsNullOrEmpty(Wrapper.Password) ? DefaultPassword : Wrapper.Password;
            Log.InfoFormat("Connect using credentials: {0}@{1}", Wrapper.Login, Wrapper.Address);
            try
            {
                var localPath = String.Empty;

                if (!String.IsNullOrEmpty(path))
                {
                    var localPathInfo = new DirectoryInfo(path);
                    localPath = localPathInfo.FullName;
                }

                Wrapper.Path = localPath;

                Wrapper.SetupBuilder();

                return Wrapper.Connect() ? 0 : -1;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

        static FdkWrapper Wrapper { get; set; }

        public static DataFeed Feed
        {
            get
            {
                return Wrapper.ConnectLogic.Feed;
            }
        }
        public static DataFeedStorage Storage
        {
            get
            {
                return Wrapper.ConnectLogic.Storage;
            }
        }
        
        public static DataTrade Trade {
        	get{
				return Wrapper.ConnectLogic.TradeWrapper.Trade;
        	}
        }

        public static string Address { get
            {
                return Wrapper.Address;
            }
            set {
                Wrapper.Address = value;
            }
        }
        public static string Login
        {
            get
            {
                return Wrapper.Login;
            }
            set
            {
                Wrapper.Login = value;
            }
        }
        public static string Password
        {
            get
            {
                return Wrapper.Password;
            }
            set
            {
                Wrapper.Password = value;
            }
        }

        public static bool UseLrp { get; set; }

        public static void Disconnect()
        {
            Wrapper.Disconnect();
        }
        public static void WriteMessage(string message)
        {
			Console.WriteLine("FdkRLib: {0}", message);
        }

        public static Double GetCreatedEpoch(DateTime created)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
            var span = (created.ToLocalTime() - epoch);
            return span.TotalSeconds;
        }

        public static Double GetCreatedEpochFromText(string createdTimeStr)
        {
            var created = DateTime.Parse(createdTimeStr, CultureInfo.InvariantCulture);
            return GetCreatedEpoch(created);
        }

        public static void DisplayDate(DateTime time)
        {
            MessageBox.Show(time.ToString());
        }


        public static DateTime GetCreatedEpoch(Double value)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
            var created = epoch.AddSeconds(value);
            return created;
        }


        public static bool IsTimeZero(DateTime startTime)
        {
            return startTime.Year == 1970 && startTime.Month == 1;
        }

        #region Accessors

        public static T? ParseEnumStr<T>(string text) where T : struct
        {
            T result;
			if (Enum.TryParse(text, out result))
				return result;
			else 
				return null;
        }

        static void ValidateAllAscii(string text)
        {
            foreach(var c in text){
                if(c>=128)
                    throw new InvalidOperationException(
                        string.Format(
                            "Field's text: '{0}' is invalid. It does not use English characters", text)
                        );
            }
        }

        public static T GetFieldByName<T>(string fieldName, bool toUpperCase = false)
        {
            ValidateAllAscii(fieldName);
            var barPeriodField = typeof(T).GetField(fieldName);
            if (barPeriodField == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                            "Field's text: '{0}' is invalid. It was not a valid value", fieldName)
                            );
            }

            var result = (T)barPeriodField.GetValue(null);

            return result;
        }
        #endregion
    }

}
 