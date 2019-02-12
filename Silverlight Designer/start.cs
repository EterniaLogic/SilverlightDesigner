using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Net.NetworkInformation;
using System.Threading;

namespace Silverlight_Designer
{
    public partial class start : Form
    {
        public start()
        {
            InitializeComponent();
            SD1.CreateControl();
            SD1.CreateGraphics();
            SD1.InitializeComponent();
            Patch();
        }

        private bool DEVELOPING = false; // Set to true, if you need to develop with quickness.
        public class INF
        {
            public static readonly string Version = "0.3"; // Sets the current version of SLD
        }
        private string PatchHost = "66.35.250.203"; //private string PatchHost = "66.35.250.203";
        private string PatchFileServer = "http://silverldesigner.sourceforge.net/Patch/"; //private string PatchFileServer = "http://silverldesigner.sourceforge.net/patch/";
        
        private SD SD1 = new SD(); // Buffing the form...
        private void Go_Tick(object sender, EventArgs e)
        {
            Go.Stop();
            timer1.Stop();
            this.Hide(); // Hiding this form           
            SD1.ShowDialog(); // Opening the form
            /*PickVersion PV = new PickVersion();
            PV.ShowDialog();*/
            this.Close();
        }

        private void Developing(bool DevModeOn)
        {
            if (DevModeOn == true)
            {
                timer1.Start();
            }
            if (DevModeOn == false) 
            {
                Go.Start();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide(); // Hiding this form           
            SD1.ShowDialog(); // Opening the form
            this.Close();
        }

        private void Patch()
        {
            label1.Visible = true;
            try
            {
                Ping kk = new Ping();
                PingReply l = kk.Send(PatchHost);
                if (l.Status == IPStatus.Success)
                {
                    WebClient oo = new WebClient();
                    if (INF.Version == oo.DownloadString(PatchFileServer + "Version.txt"))
                    {
                        Developing(DEVELOPING);
                        label1.Location = new Point(12, 278);
                        label1.Text = "No new patches.";
                    }
                    else
                    {
                        label1.Text = "New Patch Found.";
                        label1.Location = new Point(106, 106);
                        panel1.Visible = true;
                    }
                }
                else
                {
                    Developing(DEVELOPING);
                    label1.Location = new Point(12, 278);
                    label1.Text = "Patch host unreachable.";
                }
            }
            catch
            {
                Developing(DEVELOPING);
                label1.Location = new Point(12, 278);
                label1.Text = "Patch host unreachable.";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("SLDAP.exe", "-Patch "+PatchFileServer);
            Close();
        }
    }
}