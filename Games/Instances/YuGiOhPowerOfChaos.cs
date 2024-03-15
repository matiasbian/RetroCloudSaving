using Microsoft.Win32;
using RetroCloudSaving.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroCloudSaving.Games.Instances
{
    internal class YuGiOhPowerOfChaos : IGameData
    {
        public string[] GetSavePaths()
        {
            return new string[1] { @"C:\Users\matia\AppData\Local\VirtualStore\Program Files (x86)\KONAMI\Yu-Gi-Oh! Power of Chaos YUGI THE DESTINY\Yu-Gi-Oh! Power of Chaos Common\" };  
        }

        public string GetExecutablePath()
        {
            return @"C:\Program Files (x86)\KONAMI\Yu-Gi-Oh! Power of Chaos YUGI THE DESTINY";
        }
        public string GetExecutableName()
        {
            return "yugi_pc.exe";
        }

        public string GetID()
        {
            return "YUGIOH_POC_YD";
        }

        public void UploadExtras(IFileSyncer fileSyncer)
        {
            /*RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Computer\HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\KONAMI\Yu-Gi-Oh! Power Of Chaos\system", true);


            System.Byte[] data = (System.Byte[])key.GetValue("flcrc");
            string stringData = Encoding.ASCII.GetString(data);
            Console.WriteLine(stringData);
            //TODO: Upload the file to the server

            key.Close();*/
        }

        public void DownloadExtras(IFileSyncer fileSyncer)
        {
            /*RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\KONAMI\Yu-Gi-Oh! Power Of Chaos\system", true);

            //value = TODO GET HEXA VALUE
            //byte[] value = FromHex(value);
            //key.SetValue("flcrc", value);

   
            key.Close();*/
        }
        byte[] FromHex(string hex)
        {
            hex = hex.Replace("-", "");
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return raw;
        }
    }
}
