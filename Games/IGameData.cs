using RetroCloudSaving.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroCloudSaving.Games
{
    public interface IGameData
    {
        string[] GetSavePaths();

        string GetExecutablePath();
        string GetID();

        void UploadExtras(IFileSyncer fileSyncer);
        void DownloadExtras(IFileSyncer fileSyncer);
    }
}
