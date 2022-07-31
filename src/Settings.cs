using System;
using System.IO;
using System.Xml;
using System.Text;

namespace ImageToPaintBlockConverter {
    class Settings {
        public static string version = "v1.5.5";
        public static string windowTitle = "Converter " + version;

        public static string currentDirectory = Directory.GetCurrentDirectory();
        public static string backupsDirectory = currentDirectory + @"\backups";

        public static string settingsFilePath = "settings.xml";

        public static string vehicleFolderPath = @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\Stormworks\data\vehicles\";
        public static string vehicleOutputName = "Generated.xml";

        public static bool doBackups = false;
        public static int backupCount = 5;

        public static float scale = 1;

        public static double darken = 2.75;

        public static void LoadSettings() {
            XMLBackup.CheckForBackupsDirectory();
            if (!File.Exists(settingsFilePath)) GenerateNewSettingsFile();
            XmlDocument xml = new XmlDocument();
            try { xml.Load(settingsFilePath); } catch(Exception e) { GenerateNewSettingsFile(); }
            XmlNodeList oldSettings = xml.SelectNodes("/data/setting");
            if(oldSettings.Count!=0) LoadAsOldSettingsFormat();
            else {
                XmlNodeList settings = xml.SelectNodes("/xmldata");
                try {
                    foreach (XmlNode node in settings) {
                        vehicleFolderPath = node["vehicleFolderPath"].InnerText;
                        vehicleOutputName = node["vehicleOutputName"].InnerText;
                        doBackups = bool.Parse(node["doBackups"].InnerText);
                        backupCount = int.Parse(node["backupCount"].InnerText);
                        scale = float.Parse(node["scale"].InnerText);
                        darken = double.Parse(node["darken"].InnerText);
                    }
                } catch(Exception e) {
                    GenerateNewSettingsFile();
                }
            }
            if (scale == 0) scale = 1;
        }

        public static void SaveSettings() {
            using (XmlTextWriter writer = new XmlTextWriter(settingsFilePath,Encoding.UTF8)) {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartElement("xmldata");
                writer.WriteElementString("version", version);
                writer.WriteElementString("vehicleFolderPath", vehicleFolderPath);
                writer.WriteElementString("vehicleOutputName", vehicleOutputName);
                writer.WriteElementString("doBackups", doBackups.ToString());
                writer.WriteElementString("backupCount", backupCount.ToString());
                writer.WriteElementString("scale", scale.ToString());
                writer.WriteComment("darken stores what the r/g/b subpixel values are divided by to darken the image, so I stored it here incase you wanted to mess around with it. (default is 2.75)");
                writer.WriteElementString("darken", darken.ToString());
                writer.WriteEndElement();
            };
        }

        public static void GenerateNewSettingsFile() {
            using (XmlTextWriter writer = new XmlTextWriter(settingsFilePath, Encoding.UTF8)) {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartElement("xmldata");
                writer.WriteElementString("version", version);
                writer.WriteElementString("vehicleFolderPath", @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\Stormworks\data\vehicles\");
                writer.WriteElementString("vehicleOutputName", "Generated.xml");
                writer.WriteElementString("doBackups", false.ToString());
                writer.WriteElementString("backupCount", "5");
                writer.WriteElementString("scale", "1");
                writer.WriteComment("darken stores what the r/g/b subpixel values are divided by to darken the image, so I stored it here incase you wanted to mess around with it. (default is 2.75)");
                writer.WriteElementString("darken", "2.75");
                writer.WriteEndElement();
            };
        }

        public static void LoadAsOldSettingsFormat() {
            XmlDocument xml = new XmlDocument();
            xml.Load(settingsFilePath);
            XmlNodeList settings = xml.SelectNodes("/data/setting");
            try {
                vehicleFolderPath = settings[1].Attributes[0].Value;
                vehicleOutputName = settings[2].Attributes[0].Value;
                scale = float.Parse(settings[3].Attributes[0].Value.ToString());
            } catch (Exception e) {
                GenerateNewSettingsFile();
            }
            SaveSettings();
        }
    }
}