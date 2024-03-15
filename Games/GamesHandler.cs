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
        public DisplayableGame(string display, IGameData game)
        {
            this.display = display;
            this.game = game;
        }

        public string display { get; set; }
        public IGameData game { get; set; }
    }
}
