using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroCloudSaving.Persistence
{
    internal class SimpleStorage
    {
        public static void Save(string key, string value)
        {
            Properties.Settings.Default[key] = value;
            Properties.Settings.Default.Save();
            string value2 = (string)Properties.Settings.Default[key];

        }

        public static void Save(string key, string[] value)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var v in value)
            {
                if (stringBuilder.Length > 0)
                    stringBuilder.Append(",");

                stringBuilder.Append(v);
            }

            Save(key, stringBuilder.ToString());

        }

        public static string[] Load(string key, string[] defaultValue)
        {
            string[] value = ((string)Properties.Settings.Default[key]).Split(',');

            return value.Length == 0 || (value.Length == 1 && string.IsNullOrWhiteSpace(value[0])) ? defaultValue : value;
        }

        public static string Load(string key, string defaultValue)
        {
            string value = (string)Properties.Settings.Default[key];
            return value == string.Empty ? defaultValue : value;
        }
    }
}
