using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Resources;
using System.Collections;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Net;
using System.Runtime.InteropServices;
using SD_Shortcut;

namespace SD_installer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private ShellShortcut m_Shortcut;
        private int Step = 1;

        private string fileHost = "file://G:/CSharp/Projects/Silverlight Designer/Silverlight Designer/bin/Release/"; //private string fileHost = "http://silverldesigner.sourceforge.net/dl/";

        private string[] Files = { "Silverlight Designer.exe", // The primary file, that you would want the user to start with, is Files[0].
            "SDhelp.chm",
            "Silverlight Designer AutoPatcher.exe",
            "Speedy View/",
            "Speedy View/extra.js",
            "Speedy View/Silverlight.js",
            "Speedy View/createSilverlight.js",
            "Speedy View/Silverlight.html",
            "Speedy View/main.xaml" };
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Step++;
            if (Step == 2)
            {
                Back.Enabled = true;
                panel1.Visible = false;
                panel2.Left = panel1.Left;
                panel2.Top = panel1.Top;
                panel2.Visible = true;
                rad2.Checked = true;
                label1.BackColor = Color.Transparent;
                label2.BackColor = Color.FromArgb(70, 255, 255, 255);
            }
            if (Step == 3)
            {
                if (!Directory.Exists(textBox1.Text))
                {
                    if (MessageBox.Show("The Directory does not exist. Create one?") == DialogResult.OK)
                    {
                        if (Directory.CreateDirectory(textBox1.Text).Exists)
                        {
                            Back.Enabled = false;
                            Next.Enabled = false;
                            panel2.Visible = false;
                            panel3.Left = panel1.Left;
                            panel3.Top = panel1.Top;
                            panel3.Visible = true;
                            rad3.Checked = true;
                            label2.BackColor = Color.Transparent;
                            label3.BackColor = Color.FromArgb(70, 255, 255, 255);
                            Install();
                        }
                    }
                    else { Step = 2; }
                }
                else
                {
                    Directory.Delete(textBox1.Text, true);
                    if (Directory.CreateDirectory(textBox1.Text).Exists)
                    {
                        panel2.Visible = false;
                        panel3.Left = panel1.Left;
                        panel3.Top = panel1.Top;
                        panel3.Visible = true;
                        rad3.Checked = true;
                        label2.BackColor = Color.Transparent;
                        label3.BackColor = Color.FromArgb(70, 255, 255, 255);
                        Install();
                    }
                }
            }
            if (Step == 4)
            {
                if (checkBox2.Checked == true)
                {

                    string deskDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string app = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    string icon = app.Replace('\\', '/');
                    m_Shortcut.Path = "%SystemRoot%\\explorer.exe";
                    m_Shortcut.WorkingDirectory = "C:\\";
                    m_Shortcut.Arguments = "";
                    m_Shortcut.Description = "Silverlight Designer is used to make designing Silverlight applications easier.";
                    m_Shortcut.IconPath = icon;
                    m_Shortcut.IconIndex = 0;
                    m_Shortcut.WindowStyle = ProcessWindowStyle.Normal;
                    m_Shortcut.Save();
                }
                if (checkBox1.Checked == true)
                {
                    System.Diagnostics.Process.Start(textBox1.Text+Files[0]);
                }
                Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog zz = new OpenFileDialog();
            if (zz.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = zz.FileName;
            }
        }

        private void Install()
        {
            Ping pingSender = new Ping();
            IPAddress address = IPAddress.Parse("127.0.0.1");
            PingReply reply = pingSender.Send(address);
            if (reply.Status == IPStatus.Success)
            {
                for (int I = 0; I <= Files.Length; I++)
                {
                    if (!(I == Files.Length))
                    {
                        string xFile = Files[I];
                        if (xFile.EndsWith("/") == true)
                        {
                            Directory.CreateDirectory(textBox1.Text + Files[I].Replace("(Dir)", ""));
                            InstallFile.Text = Files[I];
                            progressBar1.Value = 100 / (Files.Length - I);
                        }
                        else
                        {
                            try
                            {
                                string[] x1File = xFile.Split('/');
                                string FilePath = textBox1.Text + xFile;
                                InstallFile.Text = x1File[x1File.Length];


                                System.Net.WebClient zz = new System.Net.WebClient();
                                zz.DownloadFile(fileHost + xFile, FilePath);

                            }
                            catch
                            {
                                string FilePath = textBox1.Text + xFile;
                                InstallFile.Text = xFile;


                                System.Net.WebClient zz = new System.Net.WebClient();
                                zz.DownloadFile(fileHost + xFile, FilePath);

                            }
                        }
                    }
                    else
                    {
                        label10.Text = "Completed";
                        InstallFile.Text = "";
                        Back.Enabled = false;
                        Next.Enabled = true;
                        panel3.Visible = false;
                        panel4.Left = panel1.Left;
                        panel4.Top = panel1.Top;
                        panel4.Visible = true;

                        label3.BackColor = Color.Transparent;
                        label4.BackColor = Color.FromArgb(70, 255, 255, 255);
                        Next.Text = "&Finish";
                        rad4.Checked = true;
                    }
                }
            }
            else
            {
                panel3.Visible = false;
                label9.Text = "Abort...";
                label11.Text = "You are not connected to the internet. \nPress back, to restart.";
                panel4.Left = panel1.Left;
                panel4.Top = panel1.Top;
                Next.Text = "&Finish";
                Next.Enabled = false;
                panel4.Visible = true;
                rad4.Checked = true;
                checkBox1.Visible = false;
                checkBox1.Checked = false;
                rad3.Checked = false;
                rad3.BackColor = Color.DarkRed;
                label3.BackColor = Color.Transparent;
                label4.BackColor = Color.FromArgb(70, 255, 255, 255);
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            StepBack();
        }

        private void StepBack()
        {
            Step--;
            if (Step == 1)
            {
                Back.Enabled = false;
                panel2.Visible = false;
                panel1.Left = panel1.Left;
                panel1.Top = panel1.Top;
                panel1.Visible = true;
                rad1.Checked = true;
                rad2.Checked = false;
                label2.BackColor = Color.Transparent;
                label1.BackColor = Color.FromArgb(70, 255, 255, 255);
            }
            if (Step == 2)
            {
                Back.Enabled = true;
                panel1.Visible = false;
                panel2.Left = panel1.Left;
                panel2.Top = panel1.Top;
                panel2.Visible = true;
                rad2.Checked = true;
                rad3.Checked = false;
                rad4.Checked = false;
                label4.BackColor = Color.Transparent;
                label2.BackColor = Color.FromArgb(70, 255, 255, 255);
            }
            if (Step == 3) 
            { 
                StepBack(); 
            }
        }
}
}