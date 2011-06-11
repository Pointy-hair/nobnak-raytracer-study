using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudyDiffuseShading.Model.Util {
    public static class MathUtil {
        public static byte colorFromDouble(double value) {
            return (byte)Math.Max(0, Math.Min(255, (int)(255 * value)));
        }
    }
}
