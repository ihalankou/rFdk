using System.Collections.Generic;
using System.Linq;

namespace RHost
{
    public static class FdkVars
    {
        private static readonly Dictionary<string, object> Vars = new Dictionary<string, object>();
        public static string RegisterVariable(object data, string prefix)
        {
            var pos = 0;
            if (string.IsNullOrEmpty(prefix))
                prefix = "fdk_";
            while (Vars.ContainsKey(string.Format("{0}_{1}", prefix, pos)))
            {
                pos++;
            }
            var varName = string.Format("{0}_{1}", prefix, pos);
            Vars[varName] = data;
            return varName;
        }
        
        public static string[] GetVarNames()
        {
			return Vars.Keys.ToArray();
		}

        public static void Unregister(string varName)
        {
            Vars.Remove(varName);
        }

        public static void ClearAll()
        {
            Vars.Clear();
        }

        public static T GetValue<T>(string varName)
        {
            object result;
            if (!Vars.TryGetValue(varName, out result))
                return default(T);
            return (T) result;
        }
    }
}