using Microsoft.Win32;
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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveYuGiOhPowerOfChaos();
            
        }

        private async void SaveYuGiOhPowerOfChaos ()
        {
            string game_path = @"C:\Users\matia\AppData\Local\VirtualStore\Program Files (x86)\KONAMI\Yu-Gi-Oh! Power of Chaos YUGI THE DESTINY\Yu-Gi-Oh! Power of Chaos Common\";
            
            string[] fileEntries = Directory.GetFiles(game_path);
      
            fileSyncer.UploadFile(fileEntries, game_path, () => { Console.Write("Success"); }, () => { Console.WriteLine("Failed"); });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DownloadFile();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Starting process");
            ProcessHandler.StartProcess("yugi_pc.exe", @"C:\Program Files (x86)\KONAMI\Yu-Gi-Oh! Power of Chaos YUGI THE DESTINY\Version2\", UploadFile);
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
    }
}
