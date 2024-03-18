using RetroCloudSaving.Games;
using RetroCloudSaving.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroCloudSaving.Network
{
    internal class Syncer
    {

        public async void DownloadFile(IFileSyncer fileSyncer, IGameData gameSelected)
        {
            if (!Directory.Exists(gameSelected.GetExecutablePath()))
            {
                Console.WriteLine("Game isn't installed, avoiding downloading it");
                return;
            }


            foreach (string game_path in gameSelected.GetSavePaths())
            {
                if (!Directory.Exists(game_path))
                {
                    Console.WriteLine("Save location doesn't exists, avoiding downloading it");
                    continue;
                }

                string[] fileEntries = Directory.GetFiles(game_path);

                await fileSyncer.DownloadFile(fileEntries, game_path, () => { Console.Write("Success"); }, () => { Console.WriteLine("Failed"); });
            }
            gameSelected.DownloadExtras(fileSyncer);
        }

        public async void UploadFile(IFileSyncer fileSyncer, IGameData gameSelected, string SAVE_PATH_ID)
        {
            string[] savePaths = SimpleStorage.Load(gameSelected.GetID() + SAVE_PATH_ID, gameSelected.GetSavePaths());

            foreach (string game_path in savePaths)
            {
                string[] fileEntries = Directory.GetFiles(game_path);
                await fileSyncer.UploadFile(fileEntries, game_path, () => { Console.Write("Success"); }, () => { Console.WriteLine("Failed"); });
            }
            gameSelected.UploadExtras(fileSyncer);
        }
    }
}
