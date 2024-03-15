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
            Console.WriteLine("saved Key " + key + " saved value " + value);
            string value2 = (string)Properties.Settings.Default[key];
            Console.WriteLine("Loaded Key " + key + " laoded value " + value2);

        }

        public static string Load(string key, string defaultValue)
        {
            string value = (string)Properties.Settings.Default[key];
            Console.WriteLine("Loaded Key " + key + " laoded value " + value);
            Console.WriteLine("value is empty " + (value == string.Empty ? defaultValue : value));
            return value == string.Empty ? defaultValue : value;
        }
    }
}
