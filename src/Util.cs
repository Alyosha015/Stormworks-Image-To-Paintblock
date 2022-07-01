using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace ImageToPaintBlockConverter {
    class Util {
        public static Bitmap ReadImage(String path) {
            using (var temp = new Bitmap(path)) {
                return new Bitmap(temp);
            }
        }

        public static String FileChooser(String title, String startingPath, String filter) {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = title;
            fileDialog.Filter = filter;
            if(!String.Equals(startingPath, "")) fileDialog.InitialDirectory = startingPath;
            if(fileDialog.ShowDialog() == DialogResult.OK) {
                return fileDialog.FileName;
            } else {
                return null;
            }
        }

        public static void SaveFile(String data,String path) {
            StreamWriter output = new StreamWriter(path);
            output.Write(data);
            output.Close();
        }
    }
}