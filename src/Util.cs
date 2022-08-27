using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace ImageConverter {
    class Util {
        public static Bitmap ReadImage(string path) {
            using (var temp = new Bitmap(path)) {
                return new Bitmap(temp);
            }
        }

        public static string FileChooser(string title, string startingPath, string filter) {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = title;
            fileDialog.Filter = filter;
            if (!string.Equals(startingPath, "")) fileDialog.InitialDirectory = startingPath;
            if (fileDialog.ShowDialog() == DialogResult.OK) {
                return fileDialog.FileName;
            } else {
                return null;
            }
        }

        public static void SaveTextFile(string data, string path) {
            StreamWriter output = new StreamWriter(path);
            output.Write(data);
            output.Close();
        }

        public static string FileNameFromPath(string path) {
            int lenght = path.LastIndexOf(".") - path.LastIndexOf("\\");
            return path.Substring(path.LastIndexOf("\\"), lenght);
        }
    }
}
