using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KnowMath.Helpers
{
    public class SharedStorageHelper
    {
        public static void Store(string key, string value)
        {
            Application.Current.Properties[key] = value;
        }

        public static string Get(string key)
        {
            if (Application.Current.Properties.TryGetValue(key, out object value))
                return value.ToString();

            return "";
        }

        public static void Remove(string key)
        {
            if (Application.Current.Properties.ContainsKey(key))
            {
                Application.Current.Properties.Remove(key);
            }
        }
    }
}
