using Microsoft.Win32;
using RetroCloudSaving.Games;
using RetroCloudSaving.Network;
using RetroCloudSaving.Processes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetroCloudSaving
{
    public partial class Form1 : Form
    {
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
            DownloadFile();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            gameSelected = ((DisplayableGame)comboBox1.SelectedValue).game;
            DownloadFile();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveYuGiOhPowerOfChaos();
            
        }

        private async void SaveYuGiOhPowerOfChaos ()
        {
            
            string[] fileEntries = Directory.GetFiles(gameSelected.GetSavePath());
      
            await fileSyncer.UploadFile(fileEntries, gameSelected.GetSavePath(), () => { Console.Write("Success"); }, () => { Console.WriteLine("Failed"); });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DownloadFile();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Starting process");
            ProcessHandler.StartProcess(gameSelected.GetExecutableName(), gameSelected.GetExecutablePath(), UploadFile);
        }

        async void  UploadFile ()
        {
            string game_path = @"C:\Users\matia\AppData\Local\VirtualStore\Program Files (x86)\KONAMI\Yu-Gi-Oh! Power of Chaos YUGI THE DESTINY\Yu-Gi-Oh! Power of Chaos Common\";

            string[] fileEntries = Directory.GetFiles(game_path);

            await fileSyncer.UploadFile(fileEntries, game_path, () => { Console.Write("Success"); }, () => { Console.WriteLine("Failed"); });
        }

        async void DownloadFile ()
        {
            string game_path = @"C:\Users\matia\AppData\Local\VirtualStore\Program Files (x86)\KONAMI\Yu-Gi-Oh! Power of Chaos YUGI THE DESTINY\Yu-Gi-Oh! Power of Chaos Common\";

            string[] fileEntries = Directory.GetFiles(game_path);

            await fileSyncer.DownloadFile(fileEntries, game_path, () => { Console.Write("Success"); }, () => { Console.WriteLine("Failed"); });
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://mati.games");
        }
    }
}
