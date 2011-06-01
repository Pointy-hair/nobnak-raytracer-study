using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace StudyDiffuseShading.Model {
    public class Screen {
        public const double DPI = 96;

        public readonly PixelFormat pf = PixelFormats.Bgra32;

        public readonly int width;
        public readonly int height;
        public readonly int stride;
        public readonly byte[] pixels;
        public readonly int bytesPerPixel;

        public Screen(int width, int height) {
            this.width = width;
            this.height = height;
            this.bytesPerPixel = pf.BitsPerPixel / 8;
            this.stride = width * (bytesPerPixel);
            this.pixels = new byte[stride * height];
        }

        public void setPixel(int row, int col, Color pixel) {
            var index = row * stride + col * bytesPerPixel;
            pixels[index] = pixel.B;
            pixels[index + 1] = pixel.G;
            pixels[index + 2] = pixel.R;
            pixels[index + 3] = byte.MaxValue;
        }

        public BitmapSource getImage() {
            return BitmapSource.Create(width, height, DPI, DPI, pf, null, pixels, stride);
        }
    }
}
