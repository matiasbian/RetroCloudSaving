using RetroCloudSaving.Games;
using RetroCloudSaving.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        string SAVE_PATH_ID;
        public EditGame(IGameData game, string PATH_ID, string SAVE_PATH_ID)
        {
            InitializeComponent();
            this.game = game;
            this.PATH_ID = PATH_ID;
            this.SAVE_PATH_ID = SAVE_PATH_ID;
        }

        private void EditGame_Load(object sender, EventArgs e)
        {
            string loadedData = SimpleStorage.Load(game.GetID() + PATH_ID, game.GetExecutablePath());

            string[] savePaths = SimpleStorage.Load(game.GetID() + SAVE_PATH_ID, game.GetSavePaths());
            foreach (var s in savePaths)
            {
                Console.WriteLine("[SAVE PATH ] " + s);
            }
            pathslist.Items.Clear();
            pathslist.Items.AddRange(savePaths);

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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            pathslist.Items.Add(folderBrowserDialog1.SelectedPath);
            string[] savePaths = pathslist.Items.Cast<string>().ToArray();
            SimpleStorage.Save(game.GetID() + SAVE_PATH_ID, savePaths);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pathslist.Items.RemoveAt(pathslist.SelectedIndex);
            string[] savePaths = pathslist.Items.Cast<string>().ToArray();
            SimpleStorage.Save(game.GetID() + SAVE_PATH_ID, savePaths);
        }
    }
}
