using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net.NetworkInformation;
using System.Net;

namespace Silverlight_Designer_AutoPatcher
{
    public partial class SLDAP : Form
    {
        public SLDAP()
        {
            InitializeComponent();
        }
        private string ll;

        private void Form1_Load(object sender, EventArgs e)
        {
            Ping zz = new Ping();
            try
            {
                byte[] zz3 = { 127, 0, 0, 1 };
                IPAddress zz2 = new IPAddress(zz3);
                PingReply zz1 = zz.Send(zz2.ToString());
                
                if (zz1.Status == IPStatus.Success)
                {
                    WebBrowser kk = new WebBrowser();
                    kk.Url = new Uri("http://localhost/z.txt");
                    kk.Navigated += new WebBrowserNavigatedEventHandler(kk_Navigated);
                }
            }
            catch 
            {
                Hide();
                MessageBox.Show("Cannot Connect to the patch server");
                Close();
            }
            
        }

        private void kk_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            WebBrowser kk = (WebBrowser)sender;
            string TEXT = kk.DocumentText;
            string[] k1 = TEXT.Split('$');
            string[] k2 = k1[1].Split('|');

            string[] k3 = TEXT.Split('*');
            string[] k4 = k1[1].Split('|');

            for (int I = 0; I < k2.Length; I++)
            {
                listBox1.Items.Add(k4[I]);
                listBox2.Items.Add("Writing...");
                
                WebBrowser oo = new WebBrowser();
                oo.Url = new Uri(k2[I]);
                oo.Navigated += new WebBrowserNavigatedEventHandler(oo_Navigated);
                ll = k4[I];
            }
        }

        private void oo_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            RichTextBox rc1 = new RichTextBox();
            rc1.SaveFile(ll);
            listBox2.Items[0] = "Done";
            listBox2.Items[1] = "Done";
            button1.Enabled = true;
            label1.Text = "Done";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}