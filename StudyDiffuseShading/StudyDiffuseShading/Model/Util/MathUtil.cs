using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;

namespace StudyDiffuseShading.Model.Util {
    public static class MathUtil {
        public static byte colorFromDouble(double value) {
            return (byte)Math.Max(0, Math.Min(255, (int)(255 * value)));
        }

        public static Vector3D multiply(Vector3D a, Vector3D b) {
            return new Vector3D(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }
    }
}
