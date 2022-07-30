﻿using System;
using System.Text;
using System.Drawing;

namespace ImageToPaintBlockConverter {
    class GenerateVehicle {
        static string stormworksVehicleBeginingData = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><vehicle data_version=\"3\" bodies_id=\"0\"><authors/><bodies><body unique_id=\"0\"><components>";
        static string stormworksVehicleEndingData = "</components></body></bodies><logic_node_links/></vehicle>";
        static string microprocessorStart = "<c d=\"microprocessor\"><o r=\"1,0,0,0,0,-1,0,1,0\" sc=\"10\"><microprocessor_definition width=\"2\" length=\"1\" id_counter=\"3\" id_counter_node=\"2\"><nodes><n id=\"1\" component_id=\"1\"><node mode=\"1\"/></n><n id=\"2\" component_id=\"3\"><node><position x=\"1\"/></node></n></nodes><group><data><inputs/><outputs/></data><components/><components_bridge><c><object id=\"1\"><in1/><out1/></object></c><c type=\"1\"><object id=\"3\"><in1 component_id=\"1\"/><out1/></object></c></components_bridge><groups/></group></microprocessor_definition><vp ";
        static string microprocessorEnd = "/><logic_slots><slot/><slot/></logic_slots></o></c>";
        static int imageWidthBlocks;
        static int imageHeightBlocks;
        static StringBuilder output;
        
        static StringBuilder logicLinks;
        static int onOffNodeX;
        static int onOffNodeZ;
        //generates paintable sign images
        public static string GenerateXML(Bitmap image,bool optimizePaintblocks,int optimizationThreshold) {
            output = new StringBuilder();

            imageWidthBlocks = image.Width / 9;
            imageHeightBlocks = image.Height / 9;

            output.Append(stormworksVehicleBeginingData);

            for (int blockX=0;blockX<imageWidthBlocks;blockX++) {
                for (int blockY=0;blockY<imageHeightBlocks;blockY++) {
                    int[][] pixels = new int[81][];
                    string xmlColorData = "";
                    for(int x=0;x<9;x++) {
                        for(int y=0;y<9;y++) {
                            Color pixel=image.GetPixel(Math.Abs((blockX * 9 + x + 1) - image.Width), Math.Abs((blockY * 9 + y + 1) - image.Height));
                            string pixelHex = ColorToHex(pixel, true);
                            pixels[x * 9 + y] = new int[] { pixel.R, pixel.G, pixel.B };
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

        //generates paintable indicator images
        public static string GenerateXML(Bitmap background, Bitmap glowBitmap, bool darken, bool backgroundSelected) {
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
            output.Append("x=\"" + (-imageHeightBlocks/2) + "\" z=\"" + (-imageWidthBlocks/2-1) + "\"");
            output.Append(microprocessorEnd);

            onOffNodeX = (-imageHeightBlocks / 2 + 1);
            onOffNodeZ = (-imageWidthBlocks / 2 - 1);

            for (int blockX = 0; blockX < imageWidthBlocks; blockX++) {
                for (int blockY = 0; blockY < imageHeightBlocks; blockY++) {
                    int[][] backPixels = new int[81][];
                    int[][] glowPixels = new int[81][];
                    string backXMLdata = "";
                    string glowXMLdata = "";
                    for (int x = 0; x < 9; x++) {
                        for (int y = 0; y < 9; y++) {
                            string bgHex = "";
                            Color bgPixel = Color.FromArgb(0,0,0);
                            if(backgroundSelected) {
                                bgPixel = background.GetPixel(Math.Abs((blockX * 9 + x + 1) - background.Width), Math.Abs((blockY * 9 + y + 1) - background.Height));
                                bgHex = ColorToHex(bgPixel,true);
                            }

                            Color glowPixel = glowBitmap.GetPixel(Math.Abs((blockX * 9 + x + 1) - glowBitmap.Width), Math.Abs((blockY * 9 + y + 1) - glowBitmap.Height));
                            int alpha = glowPixel.A;
                            //calculates rgb for transparent pixels on white background
                            glowPixel = Color.FromArgb(
                                (int)(255 - ((double)alpha / 255) * (255 - (double)glowPixel.R)),
                                (int)(255 - ((double)alpha / 255) * (255 - (double)glowPixel.G)),
                                (int)(255 - ((double)alpha / 255) * (255 - (double)glowPixel.B))
                            );
                            //special case for transparency calculation
                            if (glowPixel.R == 255 && glowPixel.G == 255 && glowPixel.B == 255 && alpha == 0) glowPixel = Color.FromArgb(0, 0, 0);
                            if(darken) {
                                glowPixel = Color.FromArgb(
                                    (int)((double)glowPixel.R / Settings.darken),
                                    (int)((double)glowPixel.G / Settings.darken),
                                    (int)((double)glowPixel.B / Settings.darken)
                                );
                            }
                            string glowHex = ColorToHex(glowPixel, true);
                            if (string.Equals(glowHex, "")) glowHex = "0";

                            if (backgroundSelected) {
                                backPixels[x * 9 + y] = new int[] { bgPixel.R, bgPixel.G, bgPixel.B };
                                backXMLdata += bgHex;
                            }
                            if (x * 9 + y != 80) backXMLdata += ",";
                            glowPixels[x * 9 + y] = new int[] { bgPixel.R, bgPixel.G, bgPixel.B };
                            glowXMLdata += glowHex;
                            if (x * 9 + y != 80) glowXMLdata += ",";
                        }
                    }
                    AddPaintableIndicatorData(backXMLdata,glowXMLdata,blockY,blockX);
                }
            }
            output.Append("</components></body></bodies><logic_node_links>");
            output.Append(logicLinks.ToString());
            output.Append("</logic_node_links></vehicle>");
            return output.ToString();
        }

        static void AddPaintableIndicatorData(string backgroundColorData,string additiveColorData,int x,int z) {
            x -= imageHeightBlocks / 2;
            z -= imageWidthBlocks / 2;
            output.Append("<c d=\"sign\"><o r=\"1,0,0,0,1,0,0,0,1\" sc=\"6\" gc=\"");
            output.Append(backgroundColorData+ "\" gca=\"");
            output.Append(additiveColorData);
            if (x != 0 && z != 0) output.Append("\"><vp x=\"" + x + "\" z=\"" + z + "\"/><logic_slots><slot editor_connected=\"1\"/></logic_slots></o></c>");
            else if (x == 0 && z != 0) output.Append("\"><vp z=\"" + z + "\"/><logic_slots><slot editor_connected=\"1\"/></logic_slots></o></c>");
            else if (x != 0 && z == 0) output.Append("\"><vp x=\"" + x + "\"/><logic_slots><slot editor_connected=\"1\"/></logic_slots></o></c>");
            else if (x == 0 && z == 0) output.Append("\"><vp/><logic_slots><slot editor_connected=\"1\"/></logic_slots></o></c>");
            logicLinks.Append("<logic_node_link><voxel_pos_0 x=\"" + onOffNodeX + "\" z=\"" + onOffNodeZ + "\"/><voxel_pos_1 x=\"" + x + "\" z=\"" + z + "\"/></logic_node_link>");
            logicLinks.Append("<logic_node_link type=\"4\"><voxel_pos_0 x=\"" + (onOffNodeX-1) + "\" z=\"" + (onOffNodeZ+1) + "\"/><voxel_pos_1 x=\"" + x + "\" z=\"" + z + "\"/></logic_node_link>");
        }

        static void AddPaintBlockData(string colorData, int x, int z) {
            x -= imageHeightBlocks / 2;
            z -= imageWidthBlocks / 2;
            output.Append("<c d=\"sign_na\"><o r=\"1,0,0,0,1,0,0,0,1\" sc=\"6\" gc=\"");
            output.Append(colorData);
            if (x != 0 && z != 0) output.Append("\"><vp x=\"" + x + "\" z=\"" + z + "\"/><logic_slots><slot/></logic_slots></o></c>");
            else if (x == 0 && z != 0) output.Append("\"><vp z=\"" + z + "\"/><logic_slots><slot/></logic_slots></o></c>");
            else if (x != 0 && z == 0) output.Append("\"><vp x=\"" + x + "\"/><logic_slots><slot/></logic_slots></o></c>");
            else if (x == 0 && z == 0) output.Append("\"><vp/><logic_slots><slot/></logic_slots></o></c>");
        }

        static void AddBlockData(string color, int x, int z) {
            x -= imageHeightBlocks / 2;
            z -= imageWidthBlocks / 2;
            output.Append("<c><o r=\"1,0,0,0,1,0,0,0,1\" sc=\"6,x,x," + color + ",x,x,x");
            if (x != 0 && z != 0) output.Append("\"><vp x=\"" + x + "\" z=\"" + z + "\"/></o></c>");
            else if (x == 0 && z != 0) output.Append("\"><vp z=\"" + z + "\"/></o></c>");
            else if (x != 0 && z == 0) output.Append("\"><vp x=\"" + x + "\"/></o></c>");
            else if (x == 0 && z == 0) output.Append("\"><vp/></o></c>");
        }

        static string ColorToHex(Color c, bool useWhiteSpecialCase) {
            int r = c.R; int g = c.G; int b = c.B;
            if (r == 255 && g == 255 && b == 255 && useWhiteSpecialCase) return "x";
            if (r == 0 && g == 0 && b == 0) return "";
            string hex = r.ToString("X2") + g.ToString("X2") + b.ToString("X2");
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

        static string GetPixelAverage(int[][] pixels) {
            int rAverage = (GetGreatestValue(pixels, 0) + GetSmallestValue(pixels, 0)) / 2;
            int gAverage = (GetGreatestValue(pixels, 1) + GetSmallestValue(pixels, 1)) / 2;
            int bAverage = (GetGreatestValue(pixels, 2) + GetSmallestValue(pixels, 2)) / 2;
            return ColorToHex(Color.FromArgb(rAverage, gAverage, bAverage), false);
        }
    }
}