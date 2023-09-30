using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ImageConverter {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            try {
                Settings.LoadSettings();
                Backups.CheckForBackupsDirectory();
                SetProcessDPIAware();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainWindow());
            } catch (Exception ex) {
                MessageBox.Show("Please report it and include 'error.txt'.", "An error occured.");
                StreamWriter sw = new StreamWriter("error.txt");
                sw.WriteLine(ex.Message);
                sw.WriteLine(ex.StackTrace);
                sw.Close();
            }            
        }

        [DllImport("user32.dll")]
        internal static extern bool SetProcessDPIAware();
    }
}
