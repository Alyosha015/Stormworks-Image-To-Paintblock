using System;
using System.Text;
using System.Drawing;
using System.Collections.Generic;

namespace ImageConverter {
    class Generator {
        public struct Block {
            public Block(int x, int z, byte[][] pixels, string xmlColor, bool optimized, bool isAir) {
                X = x;
                Z = z;
                Optimized = optimized;
                XmlColor = xmlColor;
                Avg = GetPixelAverage(pixels);
                IsAir = isAir;
            }
            
            public int X { get; }
            public int Z { get; }
            public bool Optimized { get; }
            public string XmlColor { get; }
            public string Avg { get; }
            public bool IsAir { get; set; }
        }

        private static readonly string stormworksVehicleBeginingData = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><vehicle data_version=\"3\" bodies_id=\"0\"><authors/><bodies><body unique_id=\"0\"><components>";
        private static readonly string stormworksVehicleEndingData = "</components></body></bodies><logic_node_links/></vehicle>";
        private static readonly string microprocessorStart = "<c d=\"microprocessor\"><o r=\"1,0,0,0,0,-1,0,1,0\" sc=\"10\"><microprocessor_definition width=\"2\" length=\"1\" id_counter=\"3\" id_counter_node=\"2\"><nodes><n id=\"1\" component_id=\"1\"><node mode=\"1\"/></n><n id=\"2\" component_id=\"3\"><node><position x=\"1\"/></node></n></nodes><group><data><inputs/><outputs/></data><components/><components_bridge><c><object id=\"1\"><in1/><out1/></object></c><c type=\"1\"><object id=\"3\"><in1 component_id=\"1\"/><out1/></object></c></components_bridge><groups/></group></microprocessor_definition><vp ";
        private static readonly string microprocessorEnd = "/><logic_slots><slot/><slot/></logic_slots></o></c>";

        private int imageWidthBlocks;
        private int imageHeightBlocks;

        private StringBuilder output;
        private Block[][] blocks;

        private int bottomBorder;
        private int topBorder;
        private int rightBorder;
        private int leftBorder;

        private string bottomRightCorner = "";
        private string bottomLeftCorner = "";
        private string topRightCorner = "";
        private string topLeftCorner = "";

        //stores averaged color of border on white background for 0-8 pixels thick, to get the correct color for comparisons use the border thickness in pixels as an index
        private static readonly string[] borderColors = {
            "FFFFFF",
            "E2E2E2",
            "C6C6C6",
            "AAAAAA",
            "8D8D8D",
            "717171",
            "555555",
            "383838",
            "1C1C1C",
        };

        private StringBuilder logicLinks;
        private int onOffNodeX;
        private int onOffNodeZ;

        public string GenerateXML(Bitmap image, bool optimizePaintblocks, int optimizationThreshold, bool cutout, int bottomBorder, int topBorder, int rightBorder, int leftBorder) {
            output = new StringBuilder();

            this.bottomBorder = bottomBorder;
            this.topBorder = topBorder;
            this.rightBorder = rightBorder;
            this.leftBorder = leftBorder;

            imageWidthBlocks = image.Width / 9;
            imageHeightBlocks = image.Height / 9;

            if(cutout) {
                blocks = new Block[imageHeightBlocks][];
                for (int i = 0; i < imageHeightBlocks; i++) {
                    blocks[i] = new Block[imageWidthBlocks];
                }
            }

            output.Append(stormworksVehicleBeginingData);

            for (int blockX = 0; blockX < imageWidthBlocks; blockX++) {
                for (int blockY = 0; blockY < imageHeightBlocks; blockY++) {
                    byte[][] pixels = new byte[81][];
                    string xmlColorData = "";
                    for (int x = 0; x < 9; x++) {
                        for (int y = 0; y < 9; y++) {
                            Color pixel = image.GetPixel(Math.Abs((blockX * 9 + x + 1) - image.Width), Math.Abs((blockY * 9 + y + 1) - image.Height));
                            string pixelHex = ColorToHex(pixel, true);
                            pixels[x * 9 + y] = new byte[] { pixel.R, pixel.G, pixel.B };
                            xmlColorData += pixelHex;
                            if (x * 9 + y != 80) xmlColorData += ",";
                        }
                    }

                    if(!cutout) {
                        if (optimizePaintblocks && IsPixelDataInThreshold(pixels, optimizationThreshold)) {
                            AddBlockData(GetPixelAverage(pixels), blockY, blockX);
                        } else {
                            AddPaintableSignData(xmlColorData, blockY, blockX);
                        }
                    } else {
                        blocks[blockY][blockX] = new Block(blockY, blockX, pixels, xmlColorData, IsPixelDataInThreshold(pixels, optimizationThreshold), false);
                    }
                }
            }
            if (cutout) {
                bottomRightCorner = CalcCornerColor(bottomBorder, rightBorder);
                bottomLeftCorner = CalcCornerColor(bottomBorder, leftBorder);
                topRightCorner = CalcCornerColor(topBorder, rightBorder);
                topLeftCorner = CalcCornerColor(topBorder, leftBorder);

                for (int x = 0; x < imageWidthBlocks; x++) {
                    FloodFill(x, 0);
                    FloodFill(x, imageHeightBlocks - 1);
                }
                for (int y = 0; y < imageHeightBlocks; y++) {
                    FloodFill(0, y);
                    FloodFill(imageWidthBlocks - 1, y);
                }
                for (int blockX = 0; blockX < imageWidthBlocks; blockX++) {
                    for (int blockY = 0; blockY < imageHeightBlocks; blockY++) {
                        Block block = blocks[blockY][blockX];
                        if (block.Optimized && !block.IsAir) {
                            AddBlockData(block.Avg, blockY, blockX);
                        } else if(!block.IsAir) {
                            AddPaintableSignData(block.XmlColor, blockY, blockX);
                        }
                    }
                }
            }
            output.Append(stormworksVehicleEndingData);
            return output.ToString();
        }

        private string CalcCornerColor(int border1, int border2) {
            byte[][] pixels = new byte[81][];
            int blackPixels = (border1 * 9) + (border2 * (9 - border1));
            for (int i = 0; i < 81; i++) {
                if (i < blackPixels) pixels[i] = new byte[] { 0, 0, 0 };
                else pixels[i] = new byte[] { 255, 255, 255 };
            }
            return GetPixelAverage(pixels);
        }

        private void FloodFill(int xx,int yy) {
            if (blocks[yy][xx].IsAir) return;
            Queue<Point> q = new Queue<Point>();
            q.Enqueue(new Point(xx, yy));
            while(!(q.Count==0)) {
                Point p = q.Dequeue();
                int x = p.X;
                int y = p.Y;
                if (x < 0 || x >= imageWidthBlocks || y < 0 || y >= imageHeightBlocks || blocks[y][x].IsAir) continue;
                if (!((y == 0 && string.Equals(blocks[y][x].Avg, borderColors[bottomBorder])) ||
                    (y == imageHeightBlocks - 1 && string.Equals(blocks[y][x].Avg, borderColors[topBorder])) ||
                    (x == 0 && string.Equals(blocks[y][x].Avg, borderColors[rightBorder])) ||
                    (x == imageWidthBlocks - 1 && string.Equals(blocks[y][x].Avg, borderColors[leftBorder])) ||
                    (y == 0 && x == 0 && string.Equals(blocks[y][x].Avg, bottomRightCorner)) ||
                    (y == 0 && x == imageWidthBlocks - 1 && string.Equals(blocks[y][x].Avg, bottomLeftCorner)) ||
                    (y == imageHeightBlocks - 1 && x == 0 && string.Equals(blocks[y][x].Avg, topRightCorner)) ||
                    (y == imageHeightBlocks - 1 && x == imageWidthBlocks - 1 && string.Equals(blocks[y][x].Avg, topLeftCorner)))
                    && !string.Equals(blocks[y][x].Avg, "FFFFFF")) continue;

                blocks[y][x].IsAir = true;
                q.Enqueue(new Point(x + 1, y));
                q.Enqueue(new Point(x - 1, y));
                q.Enqueue(new Point(x, y + 1));
                q.Enqueue(new Point(x, y - 1));
            }
        }

        public string GenerateXML(Bitmap background, Bitmap glowBitmap, bool darken, bool backgroundSelected) {
            output = new StringBuilder();
            logicLinks = new StringBuilder();
            imageWidthBlocks = background.Width / 9;
            imageHeightBlocks = background.Height / 9;

            if (!backgroundSelected) {
                imageWidthBlocks = glowBitmap.Width / 9;
                imageHeightBlocks = glowBitmap.Height / 9;
            }

            output.Append(stormworksVehicleBeginingData);

            output.Append(microprocessorStart);
            output.Append("x=\"" + (-imageHeightBlocks / 2) + "\" z=\"" + (-imageWidthBlocks / 2 - 1) + "\"");
            output.Append(microprocessorEnd);

            onOffNodeX = (-imageHeightBlocks / 2 + 1);
            onOffNodeZ = (-imageWidthBlocks / 2 - 1);

            for (int blockX = 0; blockX < imageWidthBlocks; blockX++) {
                for (int blockY = 0; blockY < imageHeightBlocks; blockY++) {
                    string backXMLdata = "";
                    string glowXMLdata = "";
                    for (int x = 0; x < 9; x++) {
                        for (int y = 0; y < 9; y++) {
                            string bgHex = "";
                            if (backgroundSelected) {
                                Color bgPixel = background.GetPixel(Math.Abs((blockX * 9 + x + 1) - background.Width), Math.Abs((blockY * 9 + y + 1) - background.Height));
                                bgHex = ColorToHex(bgPixel, true);
                            }

                            Color glowPixel = glowBitmap.GetPixel(Math.Abs((blockX * 9 + x + 1) - glowBitmap.Width), Math.Abs((blockY * 9 + y + 1) - glowBitmap.Height));
                            int alpha = glowPixel.A;
                            glowPixel = Color.FromArgb(
                                (int)(255 - ((double)alpha / 255) * (255 - (double)glowPixel.R)),
                                (int)(255 - ((double)alpha / 255) * (255 - (double)glowPixel.G)),
                                (int)(255 - ((double)alpha / 255) * (255 - (double)glowPixel.B))
                            );
                            if (glowPixel.R == 255 && glowPixel.G == 255 && glowPixel.B == 255 && alpha == 0) glowPixel = Color.FromArgb(0, 0, 0);
                            if (darken) {
                                glowPixel = Color.FromArgb(
                                    (int)(glowPixel.R / Settings.darken),
                                    (int)(glowPixel.G / Settings.darken),
                                    (int)(glowPixel.B / Settings.darken)
                                );
                            }
                            string glowHex = ColorToHex(glowPixel, true);
                            if (string.Equals(glowHex, "")) glowHex = "0";

                            if (backgroundSelected) {
                                backXMLdata += bgHex;
                                if (x * 9 + y != 80) backXMLdata += ",";
                            }

                            glowXMLdata += glowHex;
                            if (x * 9 + y != 80) glowXMLdata += ",";
                        }
                    }
                    AddPaintableIndicatorData(backXMLdata, glowXMLdata, blockY, blockX);
                }
            }
            output.Append("</components></body></bodies><logic_node_links>");
            output.Append(logicLinks.ToString());
            output.Append("</logic_node_links></vehicle>");
            return output.ToString();
        }

        private void AddPaintableIndicatorData(string backgroundColorData, string additiveColorData, int x, int z) {
            x -= imageHeightBlocks / 2;
            z -= imageWidthBlocks / 2;
            output.Append("<c d=\"sign\"><o r=\"1,0,0,0,1,0,0,0,1\" sc=\"6\" gc=\"");
            output.Append(backgroundColorData + "\" gca=\"");
            output.Append(additiveColorData);
            if (x != 0 && z != 0) output.Append("\"><vp x=\"" + x + "\" z=\"" + z + "\"/><logic_slots><slot editor_connected=\"1\"/></logic_slots></o></c>");
            else if (x == 0 && z != 0) output.Append("\"><vp z=\"" + z + "\"/><logic_slots><slot editor_connected=\"1\"/></logic_slots></o></c>");
            else if (x != 0 && z == 0) output.Append("\"><vp x=\"" + x + "\"/><logic_slots><slot editor_connected=\"1\"/></logic_slots></o></c>");
            else if (x == 0 && z == 0) output.Append("\"><vp/><logic_slots><slot editor_connected=\"1\"/></logic_slots></o></c>");
            logicLinks.Append("<logic_node_link><voxel_pos_0 x=\"" + onOffNodeX + "\" z=\"" + onOffNodeZ + "\"/><voxel_pos_1 x=\"" + x + "\" z=\"" + z + "\"/></logic_node_link>");
            logicLinks.Append("<logic_node_link type=\"4\"><voxel_pos_0 x=\"" + (onOffNodeX - 1) + "\" z=\"" + (onOffNodeZ + 1) + "\"/><voxel_pos_1 x=\"" + x + "\" z=\"" + z + "\"/></logic_node_link>");
        }

        private void AddPaintableSignData(string colorData, int x, int z) {
            x -= imageHeightBlocks / 2;
            z -= imageWidthBlocks / 2;
            output.Append("<c d=\"sign_na\"><o r=\"1,0,0,0,1,0,0,0,1\" sc=\"6\" gc=\"");
            output.Append(colorData);
            if (x != 0 && z != 0) output.Append("\"><vp x=\"" + x + "\" z=\"" + z + "\"/><logic_slots><slot/></logic_slots></o></c>");
            else if (x == 0 && z != 0) output.Append("\"><vp z=\"" + z + "\"/><logic_slots><slot/></logic_slots></o></c>");
            else if (x != 0 && z == 0) output.Append("\"><vp x=\"" + x + "\"/><logic_slots><slot/></logic_slots></o></c>");
            else if (x == 0 && z == 0) output.Append("\"><vp/><logic_slots><slot/></logic_slots></o></c>");
        }

        private void AddBlockData(string color, int x, int z) {
            x -= imageHeightBlocks / 2;
            z -= imageWidthBlocks / 2;
            output.Append("<c><o r=\"1,0,0,0,1,0,0,0,1\" sc=\"6,x,x," + color + ",x,x,x");
            if (x != 0 && z != 0) output.Append("\"><vp x=\"" + x + "\" z=\"" + z + "\"/></o></c>");
            else if (x == 0 && z != 0) output.Append("\"><vp z=\"" + z + "\"/></o></c>");
            else if (x != 0 && z == 0) output.Append("\"><vp x=\"" + x + "\"/></o></c>");
            else if (x == 0 && z == 0) output.Append("\"><vp/></o></c>");
        }

        private static string ColorToHex(Color c, bool useWhiteSpecialCase) {
            byte r = c.R; byte g = c.G; byte b = c.B;
            if (r == 255 && g == 255 && b == 255 && useWhiteSpecialCase) return "x";
            if (r == 0 && g == 0 && b == 0) return "";
            if (r == 0 && g == 0) return ToHex(b);
            if (r == 0) return ToHex(g) + ToHex(b);
            return ToHex(r) + ToHex(g) + ToHex(b);
        }

        private static string ToHex(byte num) {
            string hex = num.ToString("X2");
            int len = hex.Length;
            for(int i=0;i<2-len;i++) {
                hex = "0" + hex; 
            }
            return hex;
        }

        private static bool IsPixelDataInThreshold(byte[][] pixels, int threshold) {
            if (GetGreatestValue(pixels, 0) - GetSmallestValue(pixels, 0) > threshold) return false;
            if (GetGreatestValue(pixels, 1) - GetSmallestValue(pixels, 1) > threshold) return false;
            if (GetGreatestValue(pixels, 2) - GetSmallestValue(pixels, 2) > threshold) return false;
            return true;
        }

        private static byte GetSmallestValue(byte[][] pixels, int subpixelNum) {
            byte min = pixels[0][subpixelNum];
            for (int i = 0; i < pixels.Length; i++) if (pixels[i][subpixelNum] < min) min = pixels[i][subpixelNum];
            return min;
        }

        private static byte GetGreatestValue(byte[][] pixels, int subpixelNum) {
            byte max = pixels[0][subpixelNum];
            for (int i = 0; i < pixels.Length; i++) if (pixels[i][subpixelNum] > max) max = pixels[i][subpixelNum];
            return max;
        }

        private static string GetPixelAverage(byte[][] pixels) {
            int rAverage = 0;
            int gAverage = 0;
            int bAverage = 0;
            for(int i=0;i<81;i++) {
                rAverage += pixels[i][0];
                gAverage += pixels[i][1];
                bAverage += pixels[i][2];
            }
            return ColorToHex(Color.FromArgb(rAverage/81, gAverage/81, bAverage/81), false);
        }
    }
}
