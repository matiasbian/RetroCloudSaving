using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroCloudSaving.Network
{
    public interface IFileSyncer
    {
        Task UploadFile(string[] filenames, string game_path, Action successCallback, Action failureCallback);
        Task DownloadFile(string[] filenames, string game_path, Action successCallback, Action failureCallback);
    }
}
