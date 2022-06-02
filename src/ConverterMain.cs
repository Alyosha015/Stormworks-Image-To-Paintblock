using System;

namespace ImageToPaintblockConverter {
    class ConverterMain {

        [STAThread]
        static void Main(string[] args) {
            Console.Title = Settings.consoleTitle;
            Console.SetWindowSize(65,10);

            Settings.CheckForSettingsFile();
            Generator.GenerateVehicle();

            System.Threading.Thread.Sleep(1000);
        }
    }
}