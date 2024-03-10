using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RetroCloudSaving
{
    public partial class Form1 : Form
    {
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
            /*const string userRoot = "HKEY_CURRENT_USER";
            const string subkey = "RegistrySetValueExample";
            const string keyName = userRoot + "\\" + subkey;

            string value = "";
            value = (string) Registry.GetValue(keyName, value, "NO VALUE");
            MessageBox.Show(value);*/
            SaveYuGiOhPowerOfChaos();
            
        }

        private async void SaveYuGiOhPowerOfChaos ()
        {
            string game_path = @"C:\Program Files (x86)\KONAMI\Yu-Gi-Oh! Power of Chaos YUGI THE DESTINY\";
            string file_path = "Region.dat";

            await FTPRequest.Main(file_path, game_path);
            MessageBox.Show("Task finalized");
        }
    }
}
