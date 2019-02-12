using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace SLDAP
{
    public partial class Form1 : Form
    {
        public Form1(string PatchPath)
        {
            InitializeComponent();
            //PatchServerPath = PatchPath;
            PatchServerPath = "http://snake/Patch/";
        }

        private string ll;
        private string PatchServerPath;
        private void button1_Click(object sender, EventArgs e)
        {
            string appFolder = System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("Silverlight Designer.exe", "");
            System.Diagnostics.Process.Start("Silverlight Designer.exe");
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("Silverlight Designer.exe");
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            WebClient zz = new WebClient();
            string[] Ap = zz.DownloadString(PatchServerPath + "files.txt").Split('|');
            string PatchThese = "";
            try
            {
                
                //MessageBox.Show("0 < "+Ap.Length);
                string appFolder = System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("SLDAP.exe", "");
                for (int I = 0; I < Ap.Length; I++)
                {
                    if (File.Exists(appFolder + Ap[I]))
                    {
                        if (zz.DownloadString(PatchServerPath + Ap[I]) == File.ReadAllText(appFolder + Ap[I])) { /*MessageBox.Show(Ap[I] + " Exists, and has equal content.");*/ }
                        else
                        {
                            //MessageBox.Show(Ap[I] + " Does not Have Equal Content.");
                            if (!(I == Ap.Length - 1))
                            {
                                PatchThese += Ap[I] + "|";
                            }
                            else { PatchThese += Ap[I]; }
                        }
                    }
                    else
                    {
                        if (!(I == Ap.Length - 1))
                        {
                            PatchThese += Ap[I] + "|";
                        }
                        else { PatchThese += Ap[I]; }
                        //MessageBox.Show(Ap[I] + " Does not Exist.");
                    }
                }

                //MessageBox.Show(PatchThese);
                string[] PatchFiles = PatchThese.Split('|');
                for (int I = 0; I < PatchFiles.Length; I++)
                {
                    listBox1.Items.Add(PatchFiles[I]);
                    listBox2.Items.Add("Downloading...");
                }
                Patch(PatchFiles, appFolder);
                label1.Text = "Starting Autopatch...";
            }
            catch (Exception q) { MessageBox.Show(q.InnerException.Message); }
        }

        private void Patch(string[] FilesNeeded, string FolderForFiles)
        {
            WebClient zz = new WebClient();
            MessageBox.Show(PatchServerPath + FilesNeeded[0] + "|" + FolderForFiles + FilesNeeded[0]);
            for (int I = 0; I < FilesNeeded.Length; I++)
            {
                try { zz.DownloadFile(PatchServerPath + FilesNeeded[I], FolderForFiles + FilesNeeded[I]); }
                catch {  }
                //MessageBox.Show(FilesNeeded[I]);
            }
        }
    }
}