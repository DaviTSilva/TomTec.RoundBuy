using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TomTec.RoundBuy.Lib.Utils
{
    public class CallerSettingsReader
    {
        /// <summary>
        /// Uses the caller app settings, that needs to be named "appsettings.json"
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static T GetValue<T>(string propertyName)
        {
            var obj = ReadJsonConfigFile();
            var a = obj[propertyName];
            return (T)a;
        }

        private static Dictionary<string, object> ReadJsonConfigFile()
        {
            using (StreamReader r = new StreamReader("appsettings.json"))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            }
        }
    }
}
