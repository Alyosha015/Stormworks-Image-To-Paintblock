using System;
using System.Text;
using System.Drawing;

namespace ImageToPaintBlockConverter {
    class GenerateVehicle {
        static String stormworksVehicleBeginingData = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><vehicle data_version=\"3\" bodies_id=\"0\"><authors/><bodies><body unique_id=\"0\"><components>";
        static String stormworksVehicleEndingData = "</components></body></bodies><logic_node_links/></vehicle>";
        static int imageWidthBlocks;
        static int imageHeightBlocks;
        static StringBuilder output;
        public static String GenerateXML(Bitmap image,bool optimizePaintblocks,int optimizationThreshold) {
            output = new StringBuilder();

            imageWidthBlocks = image.Width / 9;
            imageHeightBlocks = image.Height / 9;

            output.Append(stormworksVehicleBeginingData);

            for(int blockX=0;blockX<imageWidthBlocks;blockX++) {
                for(int blockY=0;blockY<imageHeightBlocks;blockY++) {
                    int[][] pixels = new int[81][];
                    String[] pixelsHex = new string[81];
                    String xmlColorData = "";
                    for(int x=0;x<9;x++) {
                        for(int y=0;y<9;y++) {
                            Color pixel = image.GetPixel(Math.Abs((blockX * 9 + x + 1) - image.Width), Math.Abs((blockY * 9 + y + 1) - image.Height));
                            String pixelHex = ColorToHex(pixel, true);
                            pixels[x * 9 + y] = new int[] { pixel.R, pixel.G, pixel.B };
                            pixelsHex[x * 9 + y] = pixelHex;
                            xmlColorData += pixelHex;
                            if (x * 9 + y != 80) xmlColorData += ",";
                        }
                    }

                    if(optimizePaintblocks) {
                        if(IsPixelDataInThreshold(pixels, optimizationThreshold)) {
                            AddBlockData(GetPixelAverage(pixels), blockY, blockX);
                        } else {
                            AddPaintBlockData(xmlColorData, blockY, blockX);
                        }
                    } else {
                        AddPaintBlockData(xmlColorData, blockY, blockX);
                    }
                }
            }

            output.Append(stormworksVehicleEndingData);
            return output.ToString();
        }

        static void AddPaintBlockData(String colorData, int x, int z) {
            x -= imageHeightBlocks / 2;
            z -= imageWidthBlocks / 2;
            output.Append("<c d=\"sign_na\"><o r=\"1,0,0,0,1,0,0,0,1\" sc=\"6\" gc=\"");
            output.Append(colorData);
            if (x != 0 && z != 0) output.Append("\"><vp x=\"" + x + "\" z=\"" + z + "\"/><logic_slots><slot/></logic_slots></o></c>");
            else if (x == 0 && z != 0) output.Append("\"><vp z=\"" + z + "\"/><logic_slots><slot/></logic_slots></o></c>");
            else if (x != 0 && z == 0) output.Append("\"><vp x=\"" + x + "\"/><logic_slots><slot/></logic_slots></o></c>");
            else if (x == 0 && z == 0) output.Append("\"><vp/><logic_slots><slot/></logic_slots></o></c>");
        }

        static void AddBlockData(String color, int x, int z) {
            x -= imageHeightBlocks / 2;
            z -= imageWidthBlocks / 2;
            output.Append("<c><o r=\"1,0,0,0,1,0,0,0,1\" sc=\"6,x,x," + color + ",x,x,x");
            if (x != 0 && z != 0) output.Append("\"><vp x=\"" + x + "\" z=\"" + z + "\"/></o></c>");
            else if (x == 0 && z != 0) output.Append("\"><vp z=\"" + z + "\"/></o></c>");
            else if (x != 0 && z == 0) output.Append("\"><vp x=\"" + x + "\"/></o></c>");
            else if (x == 0 && z == 0) output.Append("\"><vp/></o></c>");
        }

        static String ColorToHex(Color c, bool useWhiteSpecialCase) {
            int r = c.R; int g = c.G; int b = c.B;
            String hex = r.ToString("X2") + g.ToString("X2") + b.ToString("X2");
            if (r == 255 && g == 255 && b == 255 && useWhiteSpecialCase) return "x";
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

        static Color GetPixelAverageRGB(int[][] pixels) {
            int rAverage = (GetGreatestValue(pixels, 0) + GetSmallestValue(pixels, 0)) / 2;
            int gAverage = (GetGreatestValue(pixels, 1) + GetSmallestValue(pixels, 1)) / 2;
            int bAverage = (GetGreatestValue(pixels, 2) + GetSmallestValue(pixels, 2)) / 2;
            return Color.FromArgb(rAverage, gAverage, bAverage);
        }
    }
}