using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageConverter {
    internal class Generator_ByteImage {
        public int Width { get; set; }
        public int Height { get; set; }
        public byte[] Data { get; set; }
        
        public Generator_ByteImage(Bitmap image) {
            Width = image.Width;
            Height = image.Height;
            
            Data = new byte[Width * Height * 4];
            
            BitmapData bitmapData = image.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            IntPtr ptr = bitmapData.Scan0;

            Marshal.Copy(ptr, Data, 0, Data.Length);

            image.UnlockBits(bitmapData);
        }

        public void AdjustContrast(double contrast) {
            if (contrast == 1) {
                return;
            }

            for (int i = 0; i < Data.Length / 4; i++) {
                Data[i * 4 + 2] = (byte)Util.Clamp((int)(contrast * (Data[i * 4 + 2] - 128) + 128), 0, 255);
                Data[i * 4 + 1] = (byte)Util.Clamp((int)(contrast * (Data[i * 4 + 1] - 128) + 128), 0, 255);
                Data[i * 4] = (byte)Util.Clamp((int)(contrast * (Data[i * 4] - 128) + 128), 0, 255);
            }
        }

        public byte GetR(int x, int y) {
            return Data[(y * Width + x) * 4 + 2];
        }

        public byte GetG(int x, int y) {
            return Data[(y * Width + x) * 4 + 1];
        }

        public byte GetB(int x, int y) {
            return Data[(y * Width + x) * 4];
        }

        public byte GetA(int x, int y) {
            return Data[(y * Width + x) * 4 + 3];
        }

        public void SetR(int x, int y, byte c) {
            Data[(y * Width + x) * 4 + 2] = c;
        }

        public void SetG(int x, int y, byte c) {
            Data[(y * Width + x) * 4 + 1] = c;
        }

        public void SetB(int x, int y, byte c) {
            Data[(y * Width + x) * 4] = c;
        }

        public void SetA(int x, int y, byte c) {
            Data[(y * Width + x) * 4 + 3] = c;
        }
    }
}
