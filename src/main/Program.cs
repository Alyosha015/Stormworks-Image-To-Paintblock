using System;
using System.Windows.Forms;

namespace ImageToPaintBlockConverter {
    static class Program {
        [STAThread]
        static void Main() {
            Settings.LoadSettings();
            if (Environment.OSVersion.Version.Major >= 6) SetProcessDPIAware(); //stops blurry text
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Window());
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}