using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sharpie
{
    class ImageFinder
    {
        private static bool GetColorTolerance(Color c1, Color c2, int tolerance)
        {
            return Math.Abs(c1.R - c2.R) + Math.Abs(c1.G - c2.G) + Math.Abs(c1.B - c2.B) < tolerance;
        }

        private static bool AreColorsSimilar(Color c1, Color c2, int tolerance)
        {
            return Math.Abs(c1.R - c2.R) < tolerance && Math.Abs(c1.G - c2.G) < tolerance && Math.Abs(c1.B - c2.B) < tolerance;
        }

        public static Bitmap ScreenShot()
        {
            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(0, 0, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);
            return bmp;
        }

        public static bool FindBitmap(Bitmap bmpNeedle, Bitmap bmpHaystack, int tolerance, out Point location)
        {
            int width = bmpHaystack.Width - bmpNeedle.Width;
            int height = bmpHaystack.Height - bmpNeedle.Height;
            for (int outerX = 0; outerX < width; ++outerX)
            {
                for (int outerY = 0; outerY < height; ++outerY)
                {
                    for (int innerX = 0; innerX < bmpNeedle.Width; ++innerX)
                    {
                        for (int innerY = 0; innerY < bmpNeedle.Height; ++innerY)
                        {
                            Color cNeedle = bmpNeedle.GetPixel(innerX, innerY);
                            Color cHaystack = bmpHaystack.GetPixel(innerX + outerX, innerY + outerY);
                            if (!AreColorsSimilar(cNeedle, cHaystack, tolerance))
                            {
                                goto notFound;
                            }
                        }
                    }
                    location = new Point(outerX, outerY);
                    return true;
                notFound:
                    continue;
                }
            }
            location = Point.Empty;
            return false;
        }

        public static bool FindBitmapLockbits(Bitmap bmpNeedle, Bitmap bmpHaystack, int tolerance, out Point location)
        {
            LockBitmap lockNeedle = new LockBitmap(bmpNeedle);
            LockBitmap lockHaystack = new LockBitmap(bmpHaystack);
            lockNeedle.LockBits();
            lockHaystack.LockBits();
            int width = bmpHaystack.Width - bmpNeedle.Width;
            int height = bmpHaystack.Height - bmpNeedle.Height;
            for (int outerX = 0; outerX < lockHaystack.Width; ++outerX)
            {
                for (int outerY = 0; outerY < lockHaystack.Height; ++outerY)
                {
                    for (int innerX = 0; innerX < bmpNeedle.Width; ++innerX)
                    {
                        for (int innerY = 0; innerY < bmpNeedle.Height; ++innerY)
                        {
                            Color cNeedle = lockNeedle.GetPixel(innerX, innerY);
                            Color cHaystack = lockHaystack.GetPixel(innerX + outerX, innerY + outerY);

                            if (!AreColorsSimilar(cNeedle, cHaystack, tolerance))
                            {
                                goto notFound;
                            }

                        }
                    }
                    location = new Point(outerX, outerY);
                    return true;
                notFound:
                    continue;
                }
            }
            location = Point.Empty;
            lockNeedle.UnlockBits();
            lockHaystack.UnlockBits();
            return false;
        }

    }
}
