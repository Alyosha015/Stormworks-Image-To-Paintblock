using System;
using System.IO;
using System.Diagnostics;

namespace ImageToPaintBlockConverter {
    class XMLBackup {
        public static void OpenBackups() {
            CheckForBackupsDirectory();
            Process.Start(Settings.backupsDirectory);
        }

        public static void CheckForBackupsDirectory() {
            if(!Directory.Exists(Settings.backupsDirectory)) {
                Directory.CreateDirectory(Settings.backupsDirectory);
            }
        }

        public static void AddBackup(string path) {
            CheckForBackupsDirectory();
            FileInfo file = new FileInfo(path);
            if(file.Exists) {
                //delete oldest backup
                string oldestBackupPath = Settings.backupsDirectory + @"\backup" + (Settings.backupCount - 1) + ".xml";
                if(File.Exists(oldestBackupPath)) {
                    File.Delete(oldestBackupPath);
                }
                //move index in filenames by 1, so there is no backup0.xml
                for(int i = Settings.backupCount - 2; i > -1; i--) {
                    FileInfo oldBackupFile = new FileInfo(Settings.backupsDirectory+@"\backup"+i+".xml");
                    if(oldBackupFile.Exists) oldBackupFile.MoveTo(Settings.backupsDirectory + @"\backup" + (i+1) + ".xml");
                    if (File.Exists(Settings.backupsDirectory + @"\backup" + i + ".xml")) File.Delete(Settings.backupsDirectory + @"\backup" + i + ".xml");
                }
                //save new backup
                file.MoveTo(Settings.backupsDirectory + @"\backup0.xml");
            }
        }
    }
}