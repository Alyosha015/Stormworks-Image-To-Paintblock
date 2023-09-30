using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageConverter {
    internal class Generator_Block {
        public byte[] Pixels { get; set; }
        public byte[] GlowPixels { get; set; }
        
        public bool IsAir { get; set; }
        public bool IsInThreshold { get; private set; }
        
        public string XmlData { get; private set; }
        public string GlowXmlData { get; private set; }

        public Generator_Block() {

        }

        public void CalculateIfInOptimizationThreshold(int threshold) {
            byte rDifference = (byte)(GetMaxRGBValue(0) - GetMinRGBValue(0));
            byte gDifference = (byte)(GetMaxRGBValue(1) - GetMinRGBValue(1));
            byte bDifference = (byte)(GetMaxRGBValue(2) - GetMinRGBValue(2));
            IsInThreshold = rDifference <= threshold && gDifference <= threshold && bDifference <= threshold;
            if (IsInThreshold) {
                CalculateAvg();
            }
        }

        public void CalculateAvg() {
            byte rAvg = (byte)(GetTotalOfRGBValue(0) / 81);
            byte gAvg = (byte)(GetTotalOfRGBValue(1) / 81);
            byte bAvg = (byte)(GetTotalOfRGBValue(2) / 81);
            XmlData = RGB2Hex(rAvg, gAvg, bAvg);
        }

        public void GeneratePixelDataStr() {
            XmlData = string.Empty;
            for (int i = 0; i < 81; i++) {
                XmlData += RGB2Hex(Pixels[i * 4], Pixels[i * 4 + 1], Pixels[i * 4 + 2]) + (i != 80 ? "," : string.Empty);
            }
        }

        public void CalcGlowPixelData(bool darken) {
            for (int i = 0; i < 81; i++) {
                byte r = GlowPixels[i * 4];
                byte g = GlowPixels[i * 4 + 1];
                byte b = GlowPixels[i * 4 + 2];
                byte a = GlowPixels[i * 4 + 3];

                if (r == 255 && g == 255 && b == 255 && a == 0) {
                    r = 0;
                    g = 0;
                    b = 0;
                } else if (darken) {
                    r = (byte)(Math.Pow(r, Settings.darken) / Math.Pow(255, Settings.darken) * r);
                    g = (byte)(Math.Pow(g, Settings.darken) / Math.Pow(255, Settings.darken) * g);
                    b = (byte)(Math.Pow(b, Settings.darken) / Math.Pow(255, Settings.darken) * b);
                }

                GlowPixels[i * 4] = r;
                GlowPixels[i * 4 + 1] = g;
                GlowPixels[i * 4 + 2] = b;
            }
        }

        public void GenerateGlowPixelDataStr() {
            GlowXmlData = string.Empty;
            for (int i = 0; i < 81; i++) {
                GlowXmlData += RGB2Hex(GlowPixels[i * 4], GlowPixels[i * 4 + 1], GlowPixels[i * 4 + 2], true) + (i != 80 ? "," : string.Empty);
            }
        }

        //subpixel: 0 = R, 1 = G, B = 2, A = 3
        private byte GetMaxRGBValue(int subPixel) {
            byte max = 0;
            for (int i = 0; i < 81; i++) {
                byte value = Pixels[i * 4 + subPixel];
                if (value > max) {
                    max = value;
                }
            }
            return max;
        }

        private byte GetMinRGBValue(int subPixel) {
            byte min = 255;
            for (int i = 0; i < 81; i++) {
                byte value = Pixels[i * 4 + subPixel];
                if (value < min) {
                    min = value;
                }
            }
            return min;
        }

        private int GetTotalOfRGBValue(int subPixel) {
            int total = 0;
            for (int i = 0; i < 81; i++) {
                total += Pixels[i * 4 + subPixel];
            }
            return total;
        }

        private string RGB2Hex(byte r, byte g, byte b, bool glow = false) {
            if (r == 255 && g == 255 && b == 255 && !IsInThreshold) { return "x"; }
            if (r == 0 && g == 0 && b == 0) { return glow ? "0": string.Empty; }
            if (r == 0 && g == 0) { return b.ToString("X2"); }
            if (r == 0) { return g.ToString("X2") + b.ToString("X2"); }
            return r.ToString("X2") + g.ToString("X2") + b.ToString("X2");
        }
    }
}
