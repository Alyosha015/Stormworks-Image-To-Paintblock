﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;

namespace ImageToPaintBlockConverter {
    class Settings {
        public static String version = "v1.4.0";
        public static String windowTitle = "Converter " + version;

        public static int windowWidth = 400;
        public static int windowHeight = 215;

        public static String settingsFilePath = "PaintblockConverterSettings.xml";

        public static String vehicleFolderPath = "";
        public static String vehicleOutputName = "";

        public static void LoadSettings() {
            if(!File.Exists(settingsFilePath)) GenerateNewSettings();
            XmlDocument xml = new XmlDocument();
            xml.Load(settingsFilePath);
            XmlNodeList settings = xml.SelectNodes("/data/setting");
            if(!String.Equals(settings[0].Attributes[0].Value,version)) { GenerateNewSettings();LoadSettings(); }
            vehicleFolderPath = settings[1].Attributes[0].Value;
            vehicleOutputName = settings[2].Attributes[0].Value;
        }

        public static void UpdateSettings(int settingIndex, String value) {
            XmlDocument xml = new XmlDocument();
            xml.Load(settingsFilePath);
            XmlNodeList settings = xml.SelectNodes("/settings/setting");
            settings[settingIndex].Attributes[0].Value = value;
            xml.Save(settingsFilePath);
        }

        public static void GenerateNewSettings() {
            XmlDocument settings = new XmlDocument();
            XmlNode rootNode = settings.CreateElement("data");
            settings.AppendChild(rootNode);

            AddXMLNode("setting", "version", version);
            AddXMLNode("setting","vehicleFolderPath", @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\Stormworks\data\vehicles\");
            AddXMLNode("setting", "vehicleOutputName","Generated.xml");

            settings.Save(settingsFilePath);

            void AddXMLNode(String element,String name,String value) {
                XmlNode node = settings.CreateElement(element);
                XmlAttribute attribute = settings.CreateAttribute(name);
                attribute.Value = value;
                node.Attributes.Append(attribute);
                rootNode.AppendChild(node);
            }
        }
    }
}