using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using StudyDiffuseShading.Model.Sampler;
using StudyDiffuseShading.Model.Helper;

namespace StudyDiffuseShading.Model.Util {
    public static class SamplerUtil {
        public static Vector3D sampleWi(Vector3D n, IHemispherecalSampler sampler, IRandomFactory randomFactory) {
            Vector3D x, y;
            MathUtil.generateXYFromZ(n, out x, out y);

            Vector3D sample = sampler.sample(randomFactory);
            Vector3D wi = sample.X * x + sample.Y * y + sample.Z * n;
            wi.Normalize();

            return wi;
        }
    }
}
