using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace ImageToPaintblockConverter {
    class Generator {
        private static StreamWriter output = File.CreateText(Settings.vehicleFilePath+Settings.vehicleName);

        private static bool removeUnnecessaryBlocks = false;
        private static int threshold = 0;

        private static int blockWidth = 0;
        private static int blockHeight = 0;

        public static void GenerateVehicle() {
            String filePath = GetFilePath();

            int desiredWidthBlocks = Util.ConsoleReadNumber("Desired Width (In Blocks): ");
            removeUnnecessaryBlocks = Util.ConsoleReadBool("Remove Unnecessary Paintblocks (y/n): ");
            if (removeUnnecessaryBlocks) threshold = Util.ConsoleReadNumber("Threshold For Considering Block Of Pixels One Color: ");
            Bitmap image = GetResizedImage(filePath, desiredWidthBlocks * 9);

            long startTime = DateTime.Now.Ticks;
            GenerateXML(image);
            long endTime = DateTime.Now.Ticks;
            Console.Write("Finished! (Generation Time: " + (endTime - startTime) / 10000 + " ms)");
        }

        static void GenerateXML(Bitmap image) {
            Console.WriteLine("Generating XML...");
            output.Write("<?xml version=\"1.0\" encoding=\"UTF-8\"?><vehicle data_version=\"3\" bodies_id=\"0\"><authors/><bodies><body unique_id=\"0\"><components>");
            blockWidth = image.Width / 9;
            blockHeight = image.Height / 9;
            for (int blockX = 0; blockX < image.Width / 9; blockX++) {
                for (int blockY = 0; blockY < image.Height / 9; blockY++) {
                    int[][] colors = new int[81][];
                    String[] colorsHex = new string[81];
                    String colorData = "";
                    for (int x = 0; x < 9; x++) {
                        for (int y = 0; y < 9; y++) {
                            Color pixel = image.GetPixel(Math.Abs((blockX * 9 + x + 1) - image.Width), Math.Abs((blockY * 9 + y + 1) - image.Height));
                            String pixelHex = ColorToHex(pixel, true);
                            colors[x * 9 + y] = new int[] { pixel.R, pixel.G, pixel.B };
                            colorsHex[x * 9 + y] = pixelHex;
                            colorData += pixelHex;
                            if (x * 9 + y != 80) colorData += ",";
                        }
                    }

                    if(removeUnnecessaryBlocks) {
                        if(IsPixelDataInThreshold(colors, threshold)) {
                            AddBlockData(GetPixelAverage(colors), blockY, blockX);
                        } else {
                            AddPaintBlockData(colorData, blockY, blockX);
                        }
                    } else {
                        AddPaintBlockData(colorData, blockY, blockX);
                    }
                }
            }
            output.Write("</components></body></bodies><logic_node_links/></vehicle>");
            output.Close();
        }

        static void AddPaintBlockData(String colorData, int x, int z) {
            x -= blockHeight / 2;
            z -= blockWidth / 2;
            output.Write("<c d=\"sign_na\"><o r=\"1,0,0,0,1,0,0,0,1\" sc=\"6\" gc=\"");
            output.Write(colorData);
            if (x != 0 && z != 0) output.Write("\"><vp x=\"" + x + "\" z=\"" + z + "\"/><logic_slots><slot/></logic_slots></o></c>");
            else if (x == 0 && z != 0) output.Write("\"><vp z=\"" + z + "\"/><logic_slots><slot/></logic_slots></o></c>");
            else if (x != 0 && z == 0) output.Write("\"><vp x=\"" + x + "\"/><logic_slots><slot/></logic_slots></o></c>");
            else if (x == 0 && z == 0) output.Write("\"><vp/><logic_slots><slot/></logic_slots></o></c>");
        }

        static void AddBlockData(String color, int x, int z) {
            x -= blockHeight / 2;
            z -= blockWidth / 2;
            output.Write("<c><o r=\"1,0,0,0,1,0,0,0,1\" sc=\"6,x,x," + color + ",x,x,x");
            if (x != 0 && z != 0) output.Write("\"><vp x=\"" + x + "\" z=\"" + z + "\"/></o></c>");
            else if (x == 0 && z != 0) output.Write("\"><vp z=\"" + z + "\"/></o></c>");
            else if (x != 0 && z == 0) output.Write("\"><vp x=\"" + x + "\"/></o></c>");
            else if (x == 0 && z == 0) output.Write("\"><vp/></o></c>");
        }

        static String ColorToHex(Color c, bool isForVehicleFile) {
            int r = c.R; int g = c.G; int b = c.B;
            String hex = r.ToString("X2") + g.ToString("X2") + b.ToString("X2");
            if (r == 255 && g == 255 && b == 255 && isForVehicleFile) return "x";
            if (r == 0 && g == 0 && b == 0) return "";
            if (r == 0 && g == 0) return hex.Substring(3);
            if (r == 0) return hex.Substring(1);
            return hex;
        }

        static bool IsPixelDataInThreshold(int[][] pixels, int threshold) {
            if (GetGreatestValue(pixels, 0) - GetSmallestValue(pixels, 0) > threshold) return false;
            if (GetGreatestValue(pixels, 1) - GetSmallestValue(pixels, 1) > threshold) return false;
            if (GetGreatestValue(pixels, 2) - GetSmallestValue(pixels, 2) > threshold) return false;
            return true;
        }

        static int GetSmallestValue(int[][] pixels, int subpixelNum) {
            int min = pixels[0][subpixelNum];
            for (int i = 0; i < pixels.Length; i++) if (pixels[i][subpixelNum] < min) min = pixels[i][subpixelNum];
            return min;
        }

        static int GetGreatestValue(int[][] pixels, int subpixelNum) {
            int max = pixels[0][subpixelNum];
            for (int i = 0; i < pixels.Length; i++) if (pixels[i][subpixelNum] > max) max = pixels[i][subpixelNum];
            return max;
        }

        static String GetPixelAverage(int[][] pixels) {
            int rAverage = (GetGreatestValue(pixels, 0) + GetSmallestValue(pixels, 0)) / 2;
            int gAverage = (GetGreatestValue(pixels, 1) + GetSmallestValue(pixels, 1)) / 2;
            int bAverage = (GetGreatestValue(pixels, 2) + GetSmallestValue(pixels, 2)) / 2;
            return ColorToHex(Color.FromArgb(rAverage, gAverage, bAverage), false);
        }

        static String GetFilePath() {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Image Chooser";
            if (fileDialog.ShowDialog() == DialogResult.OK) {
                return fileDialog.FileName;
            }
            else {
                Console.WriteLine("No File Selected. Exiting Program.");
                System.Threading.Thread.Sleep(1000);
                Environment.Exit(0);
                return null;
            }
        }

        static Bitmap GetResizedImage(String filePath, int resizedWidth) {
            Console.WriteLine("Opening Image File...");
            Bitmap image = new Bitmap(filePath);
            double aspectRatio = (double)image.Height / (double)image.Width;
            int newY = (int)(aspectRatio * resizedWidth);
            Console.WriteLine("Resizing image to " + resizedWidth + "x" + newY + "...");
            Bitmap resizedImage = new Bitmap(image, new Size(resizedWidth, newY));
            Bitmap correctedImage = new Bitmap(resizedWidth, newY + 9);
            Graphics g = Graphics.FromImage(correctedImage);
            g.DrawImage(resizedImage, 0, newY % 9);
            if (resizedImage.Height % 9 == 0) return resizedImage;
            else return correctedImage;
        }
    }
}
