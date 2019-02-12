using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SLDAP
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]        
        static void Main(string[] args)
        {
            if (args[0] == "-Patch")
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1(args[1]));
            }
            else { Application.Exit(); }
        }
    }
}