using Microsoft.Win32;
using RetroCloudSaving.Forms;
using RetroCloudSaving.Games;
using RetroCloudSaving.Network;
using RetroCloudSaving.Persistence;
using RetroCloudSaving.Processes;
using RetroCloudSaving.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetroCloudSaving
{
    public partial class Form1 : Form
    {
        const string PATH_ID = "_PATH";
        const string SAVE_PATH_ID = "_SAVEPATH";

        IFileSyncer fileSyncer = new Network.FTPRequest();
        IGameData gameSelected;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var binding1 = new BindingSource();

            binding1.DataSource = GamesHandler.games;
            comboBox1.DataSource = binding1;
            comboBox1.DisplayMember = "display";
            //DownloadFile();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            gameSelected = ((DisplayableGame)comboBox1.SelectedValue).game;
            DownloadFile();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UploadFile();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DownloadFile();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Starting process");
            string loadedData = SimpleStorage.Load(gameSelected.GetID() + PATH_ID, Path.GetFileName(gameSelected.GetExecutablePath()));
            string exepath = Path.GetFileName(loadedData);

            string folderpath = loadedData.Replace(exepath, "");
            Console.WriteLine("folderpath " + folderpath);

            ProcessHandler.StartProcess(exepath, folderpath, UploadFile);
        }

        async void  UploadFile ()
        {
            string[] savePaths = SimpleStorage.Load(gameSelected.GetID() + SAVE_PATH_ID, gameSelected.GetSavePaths());

            foreach (string game_path in savePaths)
            {
                string[] fileEntries = Directory.GetFiles(game_path);
                await fileSyncer.UploadFile(fileEntries, game_path, () => { Console.Write("Success"); }, () => { Console.WriteLine("Failed"); });
            }
            gameSelected.UploadExtras(fileSyncer);
        }

        async void DownloadFile ()
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://mati.games");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            EditGame editGame = new EditGame(gameSelected, PATH_ID, SAVE_PATH_ID);
            editGame.ShowDialog();
            return;
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
