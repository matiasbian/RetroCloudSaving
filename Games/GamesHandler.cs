using RetroCloudSaving.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetroCloudSaving.Games
{
    public class GamesHandler
    {

        public static DisplayableGame[] games = new DisplayableGame[]
        {
            new DisplayableGame("Yu Gi Oh! Power Of Chaos Yugi the Destiny ", new Instances.YuGiOhPowerOfChaos())
        };
    }

    public class DisplayableGame
    {
        const string EXE_ID = "_PATH_EXE";
        const string FOLDER_ID = "_PATH_FOLDER";
        public DisplayableGame(string display, IGameData game)
        {
            this.display = display;
            this.game = game;
        }

        public string display { get; set; }
        public IGameData game { get; set; }

        public string GetExePath()
        {
            string savedData = (string)Settings.Default[game.GetID() + EXE_ID];
            return savedData != String.Empty ? savedData : game.GetExecutableName();
        }

        public string GetFolderPath()
        {
            string savedData = (string)Settings.Default[game.GetID() + FOLDER_ID];
            return savedData != String.Empty ? savedData : game.GetExecutablePath();
        }

        public void SaveNewPath(string newPath)
        {
            Settings.Default[game.GetID() + FOLDER_ID] = Path.GetDirectoryName(newPath);
            Settings.Default[game.GetID() + EXE_ID] = Path.GetFileName(newPath);
            Settings.Default.Save(); // Saves settings in application configuration file]
        }
    }
}
