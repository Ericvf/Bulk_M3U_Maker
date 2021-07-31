using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Bulk_M3U_Maker
{
    public partial class Form1 : Form
    {
        private FolderBrowserDialog dlg;
        private string currentFolder;

        public Form1()
        {
            InitializeComponent();

            this.dlg = new FolderBrowserDialog();
            this.dlg.Description = "Select the directory that you want to scan.";
            // this.dlg.RootFolder = Environment.SpecialFolder.MyDocuments;
            this.dlg.ShowNewFolderButton = false;

        }

        private void setCurrentFolder(string folderPath)
        {
            if (folderPath == null)
                return;

            if (!Directory.Exists(folderPath))
                return;

            this.tbPath.Text = folderPath;
            this.currentFolder = folderPath;
        }

        private void scanFolder(string sPath, string sExt, bool bRecurse, int iDeep)
        {
            if (bRecurse && iDeep > 1)
            {
                string[] directoryArray = Directory.GetDirectories(sPath);
                foreach (string sDir in directoryArray)
                    this.scanFolder(sDir, sExt, bRecurse, iDeep - 1);
            }

            string[] fileArray = Directory.GetFiles(sPath, "*." + sExt);
            m3uFile m3u = new m3uFile(sPath);
            foreach (string sFile in fileArray)
            {
                m3u.mp3Files.Add(sFile);
            }

            m3u.Save();
            this.listBox1.Items.Add(m3u.Name);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = this.dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.setCurrentFolder(this.dlg.SelectedPath);
                this.scanFolder(this.currentFolder, "mp3", true, 2);
            }
        }
    }
}