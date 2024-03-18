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
        AppStateManager stateManager;
        public EditGame(AppStateManager stateManager)
        {
            InitializeComponent();
            this.stateManager = stateManager;
        }

        private void EditGame_Load(object sender, EventArgs e)
        {
            string loadedData = stateManager.GetGamePathData();

            string[] savePaths = stateManager.GetSaveGamePathsData();
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
            stateManager.SaveGameExeData(openFileDialog1.FileName);
            Console.WriteLine(openFileDialog1.FileName);
            path_value.Text  = openFileDialog1.FileName;
            
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
            stateManager.SaveSavedGamePaths(savePaths);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pathslist.Items.RemoveAt(pathslist.SelectedIndex);
            string[] savePaths = pathslist.Items.Cast<string>().ToArray();
            stateManager.SaveSavedGamePaths(savePaths);
        }
    }
}
