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

        public static Matrix3D translate(Vector3D d) {
            return new Matrix3D(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, d.X, d.Y, d.Z, 1);
        }
        public static Matrix3D switchCoordinate(Matrix3D src) {
            var reflect = new Matrix3D(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, -1, 0, 0, 0, 0, 1);
            return Matrix3D.Multiply(src, reflect);
        }

        public static Vector3D convertToVector(Point3D p) {
            return new Vector3D(p.X, p.Y, p.Z);
        }
        public static Point3D convertToPoint(Vector3D v) {
            return new Point3D(v.X, v.Y, v.Z);
        }
    }
}
