using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Silverlight_Designer.Properties;
using System.Text.RegularExpressions;

namespace Silverlight_Designer
{
    public partial class Editor : Form
    {
        public Editor(string CONTENT, string FILE)
        {
            InitializeComponent();
            richTextBox1.Text = CONTENT;
            FILE1 = FILE;
            if (FILE == "html")
            {
                TOP.Text = "";
                Bottom.Text = "";
            }
            if (FILE == "xaml")
            {
                TOP.Text = "<Canvas xmlns=\"http://schemas.microsoft.com/client/2007\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\">";
                Bottom.Text = "</Canvas>";
            }
            if (FILE == "ejs")
            {
                TOP.Text = "";
                Bottom.Text = "";
            }
        }
        private string FILE1;
        private void button1_Click(object sender, EventArgs e)
        {
            string pp = richTextBox1.Text;

            if (FILE1 == "html")
            {
                Settings.Default.HTML = pp; Editor.ActiveForm.Close();
            }
            if (FILE1 == "xaml")
            {
                Settings.Default.XAML = pp; Editor.ActiveForm.Close();
            }
            if (FILE1 == "ejs")
            {
                Settings.Default.EJS = pp; Editor.ActiveForm.Close();
            }
        }

        public void gotoLineWithNAME(string NAME) 
        {
            //richTextBox1.Find("x:Name=\""+NAME+"\"");
        }
    }
    
}


