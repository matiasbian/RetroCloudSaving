using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroCloudSaving.Games.Instances
{
    internal class YuGiOhPowerOfChaos : IGameData
    {
        public string GetSavePath()
        {
            return @"C:\Users\matia\AppData\Local\VirtualStore\Program Files (x86)\KONAMI\Yu-Gi-Oh! Power of Chaos YUGI THE DESTINY\Yu-Gi-Oh! Power of Chaos Common\";  
        }

        public string GetExecutablePath()
        {
            return @"C:\Program Files (x86)\KONAMI\Yu-Gi-Oh! Power of Chaos YUGI THE DESTINY\Version2\";
        }
        public string GetExecutableName()
        {
            return "yugi_pc.exe";
        }
    }
}
