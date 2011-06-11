using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using StudyDiffuseShading.Model.Util;

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

        public void setPixel(int row, int col, Vector3D pixel) {
            var index = row * stride + col * bytesPerPixel;
            var b = MathUtil.colorFromDouble(pixel.Z);
            var g = MathUtil.colorFromDouble(pixel.Y);
            var r = MathUtil.colorFromDouble(pixel.X);
            var a = byte.MaxValue;
            pixels[index] = b;
            pixels[index + 1] = g;
            pixels[index + 2] = r;
            pixels[index + 3] = a;
        }

        public BitmapSource getImage() {
            return BitmapSource.Create(width, height, DPI, DPI, pf, null, pixels, stride);
        }
    }
}
