using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sharpie
{
    unsafe public class LockBitmap
    {
        Bitmap source = null;
        byte* Pixels = null;
        BitmapData bitmapData = null;

        public int Depth { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int PixelSize { get; private set; }
        public int PixelCount { get; private set; }

        public LockBitmap(Bitmap source)
        {
            this.source = source;
        }

        public void LockBits()
        {
            Width = source.Width;
            Height = source.Height;
            PixelCount = Width * Height;
            Depth = System.Drawing.Bitmap.GetPixelFormatSize(source.PixelFormat);
            bitmapData = source.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, source.PixelFormat);
            PixelSize = Depth / 8;
            Pixels = (byte*)bitmapData.Scan0;
        }

        public void UnlockBits()
        {
            *Pixels = 0;
            source.UnlockBits(bitmapData);
            bitmapData = null;
        }

        public Color GetPixel(int x, int y)
        {
            byte* row = Pixels + (y * bitmapData.Stride);
            return Color.FromArgb(row[(x * PixelSize) + 3], row[(x * PixelSize) + 2], row[(x * PixelSize) + 1], row[(x * PixelSize)]);
        }

        public void SetPixel(int x, int y, Color color)
        {
            byte* row = Pixels + (y * bitmapData.Stride);
            row[(x * PixelSize) + 3] = color.A;
            row[(x * PixelSize) + 2] = color.R;
            row[(x * PixelSize) + 1] = color.G;
            row[(x * PixelSize)] = color.B;
        }
    }
}
