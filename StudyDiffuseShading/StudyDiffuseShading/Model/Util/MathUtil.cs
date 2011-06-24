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

        public static void generateXYFromZ(Vector3D z, out Vector3D x, out Vector3D y) {
# if falsen
            Vector3D unique = new Vector3D(0.0034, 1.0, 0.0071);

            w = Vector3D.CrossProduct(unique, u);
            w.Normalize();
            v = Vector3D.CrossProduct(w, u);
# endif

            Vector3D rotateZ = new Vector3D(z.Y, z.Z, -z.X);

            y = Vector3D.CrossProduct(rotateZ, z);
            y.Normalize();
            x = Vector3D.CrossProduct(y, z);
        }
        public static Vector3D reflectDirection(Vector3D n, Vector3D wi) {
            return 2 * Vector3D.DotProduct(n, wi) * n - wi;
        }
    }
}
