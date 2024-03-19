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
        AppStateManager stateManager;
        public Form1()
        {
            InitializeComponent();
            stateManager = new AppStateManager();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var binding1 = new BindingSource();

            binding1.DataSource = GamesHandler.games;
            comboBox1.DataSource = binding1;
            comboBox1.DisplayMember = "display";
            //DownloadFile();
        }

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            stateManager.ChangeGameSelected(((DisplayableGame)comboBox1.SelectedValue).game);

            infoLabel.Text = "Downloading game data...";
            infoLabel.ForeColor = Color.Black;

            await stateManager.DownloadData(
                () => 
                {
                    infoLabel.Text = "Data synced successfully";
                    infoLabel.ForeColor = Color.Green;
                },
                () =>
                {
                    infoLabel.Text = "Data sync failed";
                    infoLabel.ForeColor = Color.Red;
                }
            );
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            infoLabel.Text = "";
            infoLabel.ForeColor = Color.Black;

            stateManager.StartGame();
            await stateManager.UploadData(
                () =>
                {
                    infoLabel.Text = "Data uploaded successfully";
                    infoLabel.ForeColor = Color.Green;
                },
                () =>
                {
                    infoLabel.Text = "Data upload failed";
                    infoLabel.ForeColor = Color.Red;
                }
            );
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://mati.games");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            EditGame editGame = new EditGame(stateManager);
            editGame.ShowDialog();
            return;
            
        }
    }
}
