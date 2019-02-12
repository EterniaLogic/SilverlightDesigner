using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Silverlight_Designer
{
    public partial class SpeedyView : Form
    {
        public SpeedyView(string XAML, string HTML, string JS, string createSilverlight)
        {
            InitializeComponent();
            zz3.Text = createSilverlight;
            zz.Text = XAML;
            zz1.Text = HTML;
            zz2.Text = JS;
        }

        private void Go_Tick(object sender, EventArgs e)
        {
            Go.Stop();
            webBrowser1.Url = new Uri(Silverlight_Designer.Properties.Resources.SDPATH + "\\Speedy View\\silverlight.html", System.UriKind.Absolute);
            panel1.Visible = false;            
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private RichTextBox zz = new RichTextBox();
        private RichTextBox zz1 = new RichTextBox();
        private RichTextBox zz2 = new RichTextBox();
        private RichTextBox zz3 = new RichTextBox();
        private void M_Tick(object sender, EventArgs e)
        {
            zz.SaveFile(Silverlight_Designer.Properties.Resources.SDPATH + "\\Speedy View\\main.xaml", RichTextBoxStreamType.PlainText);
            zz1.SaveFile(Silverlight_Designer.Properties.Resources.SDPATH + "\\Speedy View\\silverlight.html", RichTextBoxStreamType.PlainText);
            zz2.SaveFile(Silverlight_Designer.Properties.Resources.SDPATH + "\\Speedy View\\extra.js", RichTextBoxStreamType.PlainText);
            zz3.SaveFile(Silverlight_Designer.Properties.Resources.SDPATH + "\\Speedy View\\createSilverlight.js", RichTextBoxStreamType.PlainText);
        }
    }
}