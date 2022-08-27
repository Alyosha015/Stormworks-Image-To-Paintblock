using System.IO;
using System.Diagnostics;

namespace ImageConverter {
    class Backups {
        public static void OpenBackups() {
            CheckForBackupsDirectory();
            Process.Start(Settings.backupDir);
        }

        public static void CheckForBackupsDirectory() {
            if (!Directory.Exists(Settings.backupDir)) {
                Directory.CreateDirectory(Settings.backupDir);
            }
        }

        public static void AddBackup(string path) {
            CheckForBackupsDirectory();
            FileInfo file = new FileInfo(path);
            if (file.Exists) {
                string oldestBackupPath = Settings.backupDir + @"\backup" + (Settings.backupCount - 1) + ".xml";
                if (File.Exists(oldestBackupPath)) File.Delete(oldestBackupPath);
                for (int i = Settings.backupCount - 2; i > -1; i--) {
                    FileInfo oldBackupFile = new FileInfo(Settings.backupDir + @"\backup" + i + ".xml");
                    if (oldBackupFile.Exists) oldBackupFile.MoveTo(Settings.backupDir + @"\backup" + (i + 1) + ".xml");
                    if (File.Exists(Settings.backupDir + @"\backup" + i + ".xml")) File.Delete(Settings.backupDir + @"\backup" + i + ".xml");
                }
                file.MoveTo(Settings.backupDir + @"\backup0.xml");
            }
        }
    }
}
