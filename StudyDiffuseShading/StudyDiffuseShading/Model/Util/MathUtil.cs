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

        public static void constructCoordinate(Vector3D u, out Vector3D v, out Vector3D w) {
            Vector3D flipU = new Vector3D(u.Y, u.Z, -u.X);
            v = Vector3D.CrossProduct(u, flipU);
            v.Normalize();
            w = Vector3D.CrossProduct(u, v);
        }
    }
}
