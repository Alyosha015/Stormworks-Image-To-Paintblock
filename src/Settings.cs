using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Windows.Forms;

namespace ImageConverter {
    class Settings {
        public static string version = "v1.8.0";
        public static string tag = "v1.8.0.1";

        public static string settingsPath = "settings.xml";
        public static string thumbnailPath = "thumbnail.png";
        public static string backupDir = Directory.GetCurrentDirectory() + @"\backups";
        public static string vehicleFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Stormworks\data\vehicles\";

        public static string vehicleOutputName = "Generated.xml";
        public static bool useImageNameAsVehicleName = false;
        public static bool doBackups = false;
        public static int backupCount = 5;
        public static double darken = 0.4;

        public static bool saveAndLoadPos = false;
        public static int currentMonitor = 1;
        public static int xPos = Screen.AllScreens[currentMonitor - 1].WorkingArea.Width / 2;
        public static int yPos = Screen.AllScreens[currentMonitor - 1].WorkingArea.Height / 2;

        public static void LoadSettings() {
            if (!File.Exists(settingsPath)) {
                GenerateNewSettings();
            }

            XmlDocument xml = new XmlDocument();
            try {
                xml.Load(settingsPath);
            } catch(Exception) {
                GenerateNewSettings();
                CalculateWindowPos();
                return;
            }

            XmlNodeList settings = xml.SelectNodes("/settings");
            foreach(XmlNode setting in settings) {
                if (setting["vehicleFolderPath"] != null) vehicleFolderPath = setting["vehicleFolderPath"].InnerText;
                if (setting["useImageNameAsVehicleName"] != null) useImageNameAsVehicleName = bool.Parse(setting["useImageNameAsVehicleName"].InnerText);
                if (setting["vehicleOutputName"] != null) vehicleOutputName = setting["vehicleOutputName"].InnerText;
                if (setting["doBackups"] != null) doBackups = bool.Parse(setting["doBackups"].InnerText);
                if (setting["backupCount"] != null) backupCount = int.Parse(setting["backupCount"].InnerText);
                if (setting["darken"] != null) darken = double.Parse(setting["darken"].InnerText);
                if (setting["saveAndLoadWindowPos"] != null) saveAndLoadPos = bool.Parse(setting["saveAndLoadWindowPos"].InnerText);
                if (setting["monitorNum"] != null) currentMonitor = int.Parse(setting["monitorNum"].InnerText);
                if (setting["xPos"] != null) xPos = int.Parse(setting["xPos"].InnerText);
                if (setting["yPos"] != null) yPos = int.Parse(setting["yPos"].InnerText);
            }
            CalculateWindowPos();
        }

        private static void CalculateWindowPos() {
            if (!saveAndLoadPos || Screen.AllScreens.Length < currentMonitor) {
                if (Screen.AllScreens.Length < currentMonitor) {
                    currentMonitor = 1;
                }
                Screen s = Screen.AllScreens[currentMonitor - 1];
                xPos = Math.Max(s.WorkingArea.X, s.WorkingArea.X + (s.WorkingArea.Width - 403) / 2);
                yPos = Math.Max(s.WorkingArea.Y, s.WorkingArea.Y + (s.WorkingArea.Height - 220) / 2);
            }
        }

        public static void SaveOnClose() {
            XmlDocument xml = new XmlDocument();
            xml.Load(settingsPath);
            XmlNodeList settings = xml.SelectNodes("/settings");
            foreach (XmlNode setting in settings) {
                if (setting["monitorNum"] != null) setting["monitorNum"].InnerText = currentMonitor.ToString();
                if (setting["xPos"] != null) setting["xPos"].InnerText = xPos.ToString();
                if (setting["yPos"] != null) setting["yPos"].InnerText = yPos.ToString();
            }
            xml.Save(settingsPath);
        }

        public static void SaveSettings() {
            using (XmlTextWriter writer = new XmlTextWriter(settingsPath, Encoding.UTF8)) {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartElement("settings");
                writer.WriteElementString("version", tag);
                writer.WriteElementString("vehicleFolderPath", vehicleFolderPath);
                writer.WriteElementString("useImageNameAsVehicleName", useImageNameAsVehicleName.ToString());
                writer.WriteElementString("vehicleOutputName", vehicleOutputName);
                writer.WriteElementString("doBackups", doBackups.ToString());
                writer.WriteElementString("backupCount", backupCount.ToString());
                writer.WriteElementString("darken", darken.ToString());
                writer.WriteElementString("saveAndLoadWindowPos", saveAndLoadPos.ToString());
                writer.WriteElementString("monitorNum", currentMonitor.ToString());
                writer.WriteElementString("xPos", xPos.ToString());
                writer.WriteElementString("yPos", yPos.ToString());
                writer.WriteEndElement();
            }
        }

        public static void GenerateNewSettings() {
            using (XmlTextWriter writer = new XmlTextWriter(settingsPath, Encoding.UTF8)) {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartElement("settings");
                writer.WriteElementString("version", tag);
                writer.WriteElementString("vehicleFolderPath", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Stormworks\data\vehicles\");
                writer.WriteElementString("useImageNameAsVehicleName", false.ToString());
                writer.WriteElementString("vehicleOutputName", "Generated.xml");
                writer.WriteElementString("doBackups", false.ToString());
                writer.WriteElementString("backupCount", "5");
                writer.WriteElementString("darken", "0.4");
                writer.WriteElementString("saveAndLoadWindowPos", false.ToString());
                writer.WriteElementString("monitorNum","1");
                writer.WriteElementString("xPos", "0");
                writer.WriteElementString("yPos", "0");
                writer.WriteEndElement();
            }
        }
    }
}
