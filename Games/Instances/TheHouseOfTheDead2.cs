using RetroCloudSaving.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroCloudSaving.Games.Instances
{
    internal class TheHouseOfTheDead2 : IGameData
    {
        public void DownloadExtras(IFileSyncer fileSyncer)
        {
            
        }

        public string GetExecutablePath()
        {
            return @"C:\Program Files (x86)\SEGA\THE HOUSE OF THE DEAD 2\Hod2.exe";
        }

        public string GetID()
        {
            return "THOTD2";
        }

        public string[] GetSavePaths()
        {
            throw new NotImplementedException();
        }

        public void UploadExtras(IFileSyncer fileSyncer)
        {
           
        }
    }
}
