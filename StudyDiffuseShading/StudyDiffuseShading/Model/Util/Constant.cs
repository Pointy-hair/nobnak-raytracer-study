using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;

namespace StudyDiffuseShading.Model.Util {
    public static class Constant {
        public const double EPSILON = 10e-6;
        public const double INV_PI = 1 / Math.PI;

        public static readonly Vector3D BLACK = new Vector3D(0, 0, 0);
        public static readonly Vector3D WHITE = new Vector3D(1, 1, 1);
    }
}
