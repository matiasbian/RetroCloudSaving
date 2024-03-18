using RetroCloudSaving.Games;
using RetroCloudSaving.Network;
using RetroCloudSaving.Persistence;
using RetroCloudSaving.Processes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetroCloudSaving
{

    public class AppStateManager
    {
        const string PATH_ID = "_PATH";
        const string SAVE_PATH_ID = "_SAVEPATH";

        IFileSyncer fileSyncer = new Network.FTPRequest();
        IGameData gameSelected;

        public async void UploadData()
        {
            string[] savePaths = SimpleStorage.Load(gameSelected.GetID() + SAVE_PATH_ID, gameSelected.GetSavePaths());

            foreach (string game_path in savePaths)
            {
                string[] fileEntries = Directory.GetFiles(game_path);
                await fileSyncer.UploadFile(fileEntries, game_path, () => { Console.Write("Success"); }, () => { Console.WriteLine("Failed"); });
            }
            gameSelected.UploadExtras(fileSyncer);
        }

        public async void DownloadData()
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

        public void ChangeGameSelected(IGameData gameSelected)
        {
            this.gameSelected = gameSelected;
        }

        public IGameData GetgameSelected ()
        {
            return gameSelected;
        }

        public void StartGame ()
        {
            Console.WriteLine("Starting process");
            string loadedData = SimpleStorage.Load(gameSelected.GetID() + PATH_ID, Path.GetFileName(gameSelected.GetExecutablePath()));
            string exepath = Path.GetFileName(loadedData);

            string folderpath = loadedData.Replace(exepath, "");
            Console.WriteLine("folderpath " + folderpath);

            ProcessHandler.StartProcess(exepath, folderpath, UploadData);
        }

        public string GetGamePathData ()
        {
            return SimpleStorage.Load(gameSelected.GetID() + PATH_ID, gameSelected.GetExecutablePath());
        }

        public string[] GetSaveGamePathsData ()
        {
            return SimpleStorage.Load(gameSelected.GetID() + SAVE_PATH_ID, gameSelected.GetSavePaths());
        }

        public void SaveGameExeData(string path)
        {
            SimpleStorage.Save(gameSelected.GetID() + PATH_ID, path);
        }

        public void SaveSavedGamePaths(string[] paths)
        {
            SimpleStorage.Save(gameSelected.GetID() + SAVE_PATH_ID, paths);
        }
    }
}
