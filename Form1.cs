﻿using Microsoft.Win32;
using RetroCloudSaving.Games;
using RetroCloudSaving.Network;
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
        IFileSyncer fileSyncer = new Network.FTPRequest();
        DisplayableGame gameSelected;
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
            gameSelected = (DisplayableGame)comboBox1.SelectedValue;
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
            ProcessHandler.StartProcess(gameSelected.GetExePath(), gameSelected.GetFolderPath(), UploadFile);
        }

        async void  UploadFile ()
        {
            foreach (string game_path in gameSelected.game.GetSavePaths())
            {
                string[] fileEntries = Directory.GetFiles(game_path);

                await fileSyncer.UploadFile(fileEntries, game_path, () => { Console.Write("Success"); }, () => { Console.WriteLine("Failed"); });
            }
        }

        async void DownloadFile ()
        {
            foreach (string game_path in gameSelected.game.GetSavePaths())
            {
                string[] fileEntries = Directory.GetFiles(game_path);

                await fileSyncer.DownloadFile(fileEntries, game_path, () => { Console.Write("Success"); }, () => { Console.WriteLine("Failed"); });
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://mati.games");
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            Console.WriteLine(openFileDialog1.FileName);
            gameSelected.SaveNewPath(openFileDialog1.FileName);
            
        }

      
    }
}
