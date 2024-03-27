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
        const string APP_MINIMIZED_TEXT = "The application has been minimized to the system tray";

        const string DOWNLOAD_COMPLETED_TEXT = "Saved game synced successfully";
        const string DOWNLOAD_FAILED_TEXT = "Saved game sync failed";

        const string UPLOAD_COMPLETED_TEXT = "Saved game uploaded successfully";
        const string UPLOAD_FAILED_TEXT = "Saved game upload failed";

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
            this.Resize += new EventHandler(Form1_Resize);
        }



        private void Form1_Resize(object sender, EventArgs e)
        {
            Console.WriteLine("RESIZE");
            //if the form is minimized
            //hide it from the task bar
            //and show the system tray icon (represented by the NotifyIcon control)
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;

                if (Settings.Default.FIRST_INIT)
                {
                    ShowBalloonTip(APP_MINIMIZED_TEXT);
                    Settings.Default.FIRST_INIT = false;
                    Settings.Default.Save();
                } 
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
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
                    ShowBalloonTip(DOWNLOAD_COMPLETED_TEXT);
                },
                () =>
                {
                    infoLabel.Text = "Data sync failed";
                    infoLabel.ForeColor = Color.Red;
                    ShowBalloonTip(DOWNLOAD_FAILED_TEXT, toolTipIcon: ToolTipIcon.Error);
                }
            );
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            infoLabel.Text = "Starting game...";
            infoLabel.ForeColor = Color.Black;


            this.WindowState = FormWindowState.Minimized;
            stateManager.StartGame();

            infoLabel.Text = "Uploading saved game to cloud...";
            infoLabel.ForeColor = Color.Black;

            await Task.Delay(500);
            await stateManager.UploadData(
                () =>
                {
                    infoLabel.Text = "Data uploaded successfully";
                    infoLabel.ForeColor = Color.Green;
                    ShowBalloonTip(UPLOAD_COMPLETED_TEXT);
                },
                () =>
                {
                    infoLabel.Text = "Data upload failed";
                    infoLabel.ForeColor = Color.Red;
                    ShowBalloonTip(UPLOAD_FAILED_TEXT, toolTipIcon: ToolTipIcon.Error);

                }
            );
            Console.WriteLine("POST WAITING");
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

        void ShowBalloonTip(string text, string title = "", ToolTipIcon toolTipIcon = ToolTipIcon.Info)
        {
            notifyIcon1.BalloonTipTitle = !string.IsNullOrEmpty(title) ? title : notifyIcon1.BalloonTipTitle;
            notifyIcon1.BalloonTipText = text;
            notifyIcon1.BalloonTipIcon = toolTipIcon;
            notifyIcon1.ShowBalloonTip(1000);
        }
    }
}
