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
        public const double DEFAULT_GAMMA = 2.2;

        private PixelFormat pf = PixelFormats.Bgra32;

        private int width;
        private int height;
        private Vector3D[] pixels;
        private double gamma;

        public Screen(int width, int height) {
            setSize(width, height);
            setGamma(DEFAULT_GAMMA);
        }

        public void setSize(int width, int height) {
            this.width = width;
            this.height = height;
            pixels = new Vector3D[width * height];
        }

        public void setGamma(double gamma) {
            this.gamma = gamma;
        }

        public void setPixel(int row, int col, Vector3D pixel) {
            pixels[row * width + col] = pixel;
        }
        public Vector3D getPixel(int row, int col) {
            return pixels[row * width + col];
        }

        public BitmapSource getImage() {
            var bytesPerPixel = pf.BitsPerPixel / 8;
            var stride = width * (bytesPerPixel);
            var bitmap = new byte[stride * height];

            foreach (var row in Enumerable.Range(0, height))
                foreach (var col in Enumerable.Range(0, width)) {
                    var pixel = getPixel(row, col);
                    var index = row * stride + col * bytesPerPixel;
                    var invGamma = 1 / gamma;
                    var b = MathUtil.colorFromDouble(Math.Pow(pixel.Z, invGamma));
                    var g = MathUtil.colorFromDouble(Math.Pow(pixel.Y, invGamma));
                    var r = MathUtil.colorFromDouble(Math.Pow(pixel.X, invGamma));
                    var a = byte.MaxValue;
                    bitmap[index] = b;
                    bitmap[index + 1] = g;
                    bitmap[index + 2] = r;
                    bitmap[index + 3] = a;
                }

            return BitmapSource.Create(width, height, DPI, DPI, pf, null, bitmap, stride);
        }
    }
}
