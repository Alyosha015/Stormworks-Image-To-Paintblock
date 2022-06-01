using System;
using System.IO;
using System.Collections;

namespace ImageToPaintblockConverter {
    class ConverterMain {

        [STAThread]
        static void Main(string[] args) {
            Console.SetWindowSize(65,10);

            CheckForSettingsFile();

            Generator.GenerateVehicle();

            System.Threading.Thread.Sleep(1000);
        }

        static void CheckForSettingsFile() {
            if(File.Exists(Settings.settingsFileName)) {
                LoadSettings();
                return;
            } else {
                Console.WriteLine("Settings File Not Found. Generating File...");
                StreamWriter output = File.CreateText(Settings.settingsFileName);
                output.Write(settingsDefaultContents);
                output.Close();

                LoadSettings();
                
                Console.WriteLine("The settings file contains the filepath for the stormworks vehicles folder. If it is not \""+Settings.vehicleFilePath+"\" close the program and change it.");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                return;
            }
        }

        static void LoadSettings() {
            //**
            //Settings Order:
            // vehicle path (string)
            // vehicle name (string)
            // overwrite file (bool)
            //*/
            String[] fileContents = File.ReadAllLines(Settings.settingsFileName);
            ArrayList settings = new ArrayList();
            for(int i=0; i<fileContents.Length; i++) {
                String line = fileContents[i];
                if(line.Contains("SETTING:")) {
                    line = line.Substring(line.IndexOf("\"")+1);
                    line = line.Substring(0,line.IndexOf("\""));
                    settings.Add(line);
                }
            }

            Settings.vehicleFilePath = settings[0].ToString();
            if(String.Equals(settings[2].ToString(),"true")) {
                Settings.vehicleName = settings[1].ToString();
            } else {
                Settings.vehicleName = settings[1].ToString()+DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
            }
        }

        static String settingsDefaultContents =
            "SETTING: VehicleFileOutputPath=\"C:\\Users\\" + Environment.UserName + "\\AppData\\Roaming\\Stormworks\\data\\vehicles\\\"\n" +
            "SETTING: VehicleFileOutputName=\"ImageConverterGenerated.xml\"\n" +
            "\n" +
            "If \"OverwriteVehicleFile\" is set to \"false\", the program adds a unix timestamp to the end of the a generated vehicles filename to make sure no two files are named the same. Make sure to enter either \"true\" or \"false\" in the setting\n" +
            "SETTING: OverwriteVehicleFile=\"true\"\n" +
            "";
    }
}