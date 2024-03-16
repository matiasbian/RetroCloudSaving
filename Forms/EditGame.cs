using RetroCloudSaving.Games;
using RetroCloudSaving.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetroCloudSaving.Forms
{
    public partial class EditGame : Form
    {
        IGameData game;
        string PATH_ID;
        public EditGame(IGameData game, string PATH_ID)
        {
            InitializeComponent();
            this.game = game;
            this.PATH_ID = PATH_ID;
        }

        private void EditGame_Load(object sender, EventArgs e)
        {
            string loadedData = SimpleStorage.Load(game.GetID() + PATH_ID, game.GetExecutablePath());

            path_value.Text = loadedData;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            Console.WriteLine(openFileDialog1.FileName);
            path_value.Text  = openFileDialog1.FileName;
            SimpleStorage.Save(game.GetID() + PATH_ID, openFileDialog1.FileName);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
