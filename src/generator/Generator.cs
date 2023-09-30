using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageConverter {
    internal class Generator {
        private static readonly string[] borderEdges = new string[] {
            "FFFFFF",
            "E2E2E2",
            "C6C6C6",
            "AAAAAA",
            "8D8D8D",
            "717171",
            "555555",
            "383838",
            "1C1C1C",
            "E2E2E2",
            "C9C9C9",
            "B0B0B0",
            "979797",
            "7D7D7D",
            "646464",
            "4B4B4B",
            "323232",
            "191919",
            "C6C6C6",
            "B0B0B0",
            "9A9A9A",
            "848484",
            "6E6E6E",
            "585858",
            "424242",
            "2C2C2C",
            "161616",
            "AAAAAA",
            "979797",
            "848484",
            "717171",
            "5E5E5E",
            "4B4B4B",
            "383838",
            "252525",
            "121212",
            "8D8D8D",
            "7D7D7D",
            "6E6E6E",
            "5E5E5E",
            "4E4E4E",
            "3E3E3E",
            "2F2F2F",
            "1F1F1F",
            "0F0F0F",
            "717171",
            "646464",
            "585858",
            "4B4B4B",
            "3E3E3E",
            "323232",
            "252525",
            "191919",
            "0C0C0C",
            "555555",
            "4B4B4B",
            "424242",
            "383838",
            "2F2F2F",
            "252525",
            "1C1C1C",
            "121212",
            "090909",
            "383838",
            "323232",
            "2C2C2C",
            "252525",
            "1F1F1F",
            "191919",
            "121212",
            "0C0C0C",
            "060606",
            "1C1C1C",
            "191919",
            "161616",
            "121212",
            "0F0F0F",
            "0C0C0C",
            "090909",
            "060606",
            "030303",
        };


        private static readonly string stormworksVehicleBeginingData = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><vehicle data_version=\"3\" bodies_id=\"0\"><authors/><bodies><body unique_id=\"0\"><components>";
        private static readonly string stormworksVehicleEndingData = "</components></body></bodies><logic_node_links/></vehicle>";
        private static readonly string microprocessorStart = "<c d=\"microprocessor\"><o r=\"1,0,0,0,0,-1,0,1,0\" sc=\"10\"><microprocessor_definition width=\"2\" length=\"1\" id_counter=\"3\" id_counter_node=\"2\"><nodes><n id=\"1\" component_id=\"1\"><node mode=\"1\"/></n><n id=\"2\" component_id=\"3\"><node><position x=\"1\"/></node></n></nodes><group><data><inputs/><outputs/></data><components/><components_bridge><c><object id=\"1\"><in1/><out1/></object></c><c type=\"1\"><object id=\"3\"><in1 component_id=\"1\"/><out1/></object></c></components_bridge><groups/></group></microprocessor_definition><vp ";
        private static readonly string microprocessorEnd = "/><logic_slots><slot/><slot/></logic_slots></o></c>";

        private int bottomBorder;
        private int topBorder;
        private int leftBorder;
        private int rightBorder;

        private int widthBlocks;
        private int heightBlocks;

        private Generator_Block[] imageBlockData;

        private StringBuilder output;
        private StringBuilder logicLinks;

        private int logicNodeX;
        private int logicNodeZ;

        public string GeneratePaintableSign(Bitmap bitmap, double pixelContrast, bool doOptimize, int threshold, bool doCutout, int bottomBorderSize, int topBorderSize, int leftBorderSize, int rightBorderSize) {

            output = new StringBuilder();
            output.Append(stormworksVehicleBeginingData);

            bottomBorder = bottomBorderSize;
            topBorder = topBorderSize;
            leftBorder = leftBorderSize;
            rightBorder = rightBorderSize;

            Generator_ByteImage image = new Generator_ByteImage(bitmap);
            image.AdjustContrast(pixelContrast);
            
            widthBlocks = image.Width / 9;
            heightBlocks = image.Height / 9;
            
            if (doCutout) {
                imageBlockData = new Generator_Block[widthBlocks * heightBlocks];
            }

            for (int bX = 0; bX < widthBlocks; bX++) {
                for (int bY = 0; bY < heightBlocks; bY++) {
                    Generator_Block block = new Generator_Block();
                    byte[] pixels = new byte[324];

                    for (int x = 0; x < 9; x++) {
                        for (int y = 0; y < 9; y++) {
                            int pixelX = -(bX * 9 + x + 1 - image.Width);
                            int pixelY = -(bY * 9 + y + 1 - image.Height);

                            int index = (x * 9 + y) * 4;

                            pixels[index] = image.GetR(pixelX, pixelY);
                            pixels[index + 1] = image.GetG(pixelX, pixelY);
                            pixels[index + 2] = image.GetB(pixelX, pixelY);
                            pixels[index + 3] = image.GetA(pixelX, pixelY);
                        }
                    }

                    block.Pixels = pixels;

                    if (doOptimize) {
                        block.CalculateIfInOptimizationThreshold(threshold);
                    }

                    if (doCutout) {
                        imageBlockData[bX * heightBlocks + bY] = block;
                    } else if (block.IsInThreshold) {
                        AddBlockData(bX, bY, block);
                    } else {
                        block.GeneratePixelDataStr();
                        AddPaintableSignData(bX, bY, block);
                    }
                }
            }

            if (doCutout) {
                for (int x = 0; x < widthBlocks; x++) {
                    FloodFill(x, 0);
                    FloodFill(x, heightBlocks - 1);
                }

                for (int y = 0; y < heightBlocks; y++) {
                    FloodFill(0, y);
                    FloodFill(widthBlocks - 1, y);
                }

                for (int bX = 0; bX < widthBlocks; bX++) {
                    for (int bY = 0; bY < heightBlocks; bY++) {
                        Generator_Block block = imageBlockData[bX * heightBlocks + bY];

                        if(block.IsAir) {
                            continue;
                        }

                        if (block.IsInThreshold) {
                            AddBlockData(bX, bY, block);
                        } else {
                            block.GeneratePixelDataStr();
                            AddPaintableSignData(bX, bY, block);
                        }
                    }
                }
            }

            output.Append(stormworksVehicleEndingData);
            return output.ToString();
        }

        private void FloodFill(int bX, int bY) {
            int currentBlockIndex = bX * heightBlocks + bY;

            if (imageBlockData[currentBlockIndex].IsAir) {
                return;
            }
            
            Queue<int> q = new Queue<int>();
            q.Enqueue(currentBlockIndex);
            
            while(q.Count > 0) {
                int blockIndex = q.Dequeue();
                
                int x = blockIndex / heightBlocks;
                int y = blockIndex % heightBlocks;

                //check if index for block data array is out of bounds
                if (x < 0 || x > widthBlocks - 1 || y < 0 || y > heightBlocks - 1) {
                    continue;
                }

                Generator_Block block = imageBlockData[blockIndex];

                if (block.IsAir) {
                    continue;
                }

                bool minX = x == 0;
                bool maxX = x == widthBlocks - 1;
                bool minY = y == 0;
                bool maxY = y == heightBlocks - 1;

                if(minX || maxX || minY || maxY) {
                    block.CalculateAvg();
                }

                if (!(
                    (minX && string.Equals(block.XmlData, borderEdges[leftBorder])) ||
                    (maxX && string.Equals(block.XmlData, borderEdges[rightBorder])) ||
                    (minY && string.Equals(block.XmlData, borderEdges[bottomBorder])) ||
                    (maxY && string.Equals(block.XmlData, borderEdges[topBorder])) ||
                    (minX && minY && string.Equals(block.XmlData, borderEdges[bottomBorder * 9 + leftBorder])) ||
                    (maxX && minY && string.Equals(block.XmlData, borderEdges[bottomBorder * 9 + rightBorder])) ||
                    (minX && maxY && string.Equals(block.XmlData, borderEdges[topBorder * 9 + leftBorder])) ||
                    (maxX && maxY && string.Equals(block.XmlData, borderEdges[topBorder * 9 + rightBorder]))) &&
                    !string.Equals(block.XmlData, "FFFFFF")) {
                    continue;
                }

                block.IsAir = true;
                q.Enqueue(blockIndex + heightBlocks);   //x + 1, y
                q.Enqueue(blockIndex - heightBlocks);   //x - 1, y
                q.Enqueue(blockIndex + 1);              //x, y + 1
                q.Enqueue(blockIndex - 1);              //x, y - 1
            }
        }

        public string GeneratePaintableIndicator(Bitmap backgroundBitmap, Bitmap glowBitmap, double pixelContrast, bool darken, bool hasBackground) {
            output = new StringBuilder();
            logicLinks = new StringBuilder();

            output.Append(stormworksVehicleBeginingData);

            Generator_ByteImage background = new Generator_ByteImage(backgroundBitmap);
            Generator_ByteImage glow = new Generator_ByteImage(glowBitmap);
            background.AdjustContrast(pixelContrast);

            widthBlocks = glowBitmap.Width / 9;
            heightBlocks = glowBitmap.Height / 9;

            output.Append(microprocessorStart);
            output.Append($"x=\"{-heightBlocks / 2}\" z=\"{-widthBlocks / 2 - 1}\"");
            output.Append(microprocessorEnd);

            logicNodeX = -heightBlocks / 2 + 1;
            logicNodeZ = -widthBlocks / 2 - 1;

            for (int bX = 0; bX < widthBlocks; bX++) {
                for (int bY = 0; bY < heightBlocks; bY++) {
                    Generator_Block block = new Generator_Block();
                    byte[] backgroundPixels = new byte[324];
                    byte[] glowPixels = new byte[324];

                    for (int x = 0; x < 9; x++) {
                        for (int y = 0; y < 9; y++) {
                            int pixelX = -(bX * 9 + x + 1 - glowBitmap.Width);
                            int pixelY = -(bY * 9 + y + 1 - glowBitmap.Height);

                            int index = (x * 9 + y) * 4;

                            if (hasBackground) {
                                backgroundPixels[index] = background.GetR(pixelX, pixelY);
                                backgroundPixels[index + 1] = background.GetG(pixelX, pixelY);
                                backgroundPixels[index + 2] = background.GetB(pixelX, pixelY);
                                backgroundPixels[index + 3] = background.GetA(pixelX, pixelY);
                            }

                            glowPixels[index] = glow.GetR(pixelX, pixelY);
                            glowPixels[index + 1] = glow.GetG(pixelX, pixelY);
                            glowPixels[index + 2] = glow.GetB(pixelX, pixelY);
                            glowPixels[index + 3] = glow.GetA(pixelX, pixelY);
                        }
                    }

                    block.Pixels = backgroundPixels;
                    block.GeneratePixelDataStr();

                    block.GlowPixels = glowPixels;
                    block.CalcGlowPixelData(darken);
                    block.GenerateGlowPixelDataStr();

                    AddPaintableIndicatorData(bX, bY, block);
                }
            }

            output.Append("</components></body></bodies><logic_node_links>");
            output.Append(logicLinks.ToString());
            output.Append("</logic_node_links></vehicle>");
            return output.ToString();
        }

        private void AddPaintableSignData(int z, int x, Generator_Block block) {
            x -= heightBlocks / 2;
            z -= widthBlocks / 2;
            output.Append($"<c d=\"sign_na\"><o r=\"1,0,0,0,1,0,0,0,1\" sc=\"6\" gc=\"{block.XmlData}\">");
            if (x != 0 && z != 0) output.Append($"<vp x=\"{x}\" z=\"{z}\"/><logic_slots><slot/></logic_slots></o></c>");
            else if (x == 0 && z != 0) output.Append($"<vp z=\"{z}\"/><logic_slots><slot/></logic_slots></o></c>");
            else if (x != 0 && z == 0) output.Append($"<vp x=\"{x}\"/><logic_slots><slot/></logic_slots></o></c>");
            else if (x == 0 && z == 0) output.Append($"<vp/><logic_slots><slot/></logic_slots></o></c>");
        }

        private void AddBlockData(int z, int x, Generator_Block block) {
            x -= heightBlocks / 2;
            z -= widthBlocks / 2;
            output.Append($"<c><o r=\"1,0,0,0,1,0,0,0,1\" sc=\"6,x,x,{block.XmlData},x,x,x");
            if (x != 0 && z != 0) output.Append($"\"><vp x=\"{x}\" z=\"{z}\"/></o></c>");
            else if (x == 0 && z != 0) output.Append($"\"><vp z=\"{z}\"/></o></c>");
            else if (x != 0 && z == 0) output.Append($"\"><vp x=\"{x}\"/></o></c>");
            else if (x == 0 && z == 0) output.Append($"\"><vp/></o></c>");
        }

        private void AddPaintableIndicatorData(int z, int x, Generator_Block block) {
            x -= heightBlocks / 2;
            z -= widthBlocks / 2;
            output.Append($"<c d=\"sign\"><o r=\"1,0,0,0,1,0,0,0,1\" sc=\"6\" gc=\"{block.XmlData}\" gca=\"{block.GlowXmlData}\">");
            if (x != 0 && z != 0) output.Append($"<vp x=\"{x}\" z=\"{z}\"/><logic_slots><slot editor_connected=\"1\"/></logic_slots></o></c>");
            else if (x == 0 && z != 0) output.Append($"<vp z=\"{z}\"/><logic_slots><slot editor_connected=\"1\"/></logic_slots></o></c>");
            else if (x != 0 && z == 0) output.Append($"<vp x=\"{x}\"/><logic_slots><slot editor_connected=\"1\"/></logic_slots></o></c>");
            else if (x == 0 && z == 0) output.Append("<vp/><logic_slots><slot editor_connected=\"1\"/></logic_slots></o></c>");
            logicLinks.Append($"<logic_node_link><voxel_pos_0 x=\"{logicNodeX}\" z=\"{logicNodeZ}\"/><voxel_pos_1 x=\"{x}\" z=\"{z}\"/></logic_node_link><logic_node_link type=\"4\"><voxel_pos_0 x=\"{logicNodeX - 1}\" z=\"{logicNodeZ + 1}\"/><voxel_pos_1 x=\"{x}\" z=\"{z}\"/></logic_node_link>");
        }
    }
}
