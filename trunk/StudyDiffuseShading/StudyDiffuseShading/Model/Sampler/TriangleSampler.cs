using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using StudyDiffuseShading.Model.Primitive;
using StudyDiffuseShading.Model.Helper;

namespace StudyDiffuseShading.Model.Sampler {
    public class TriangleSampler {
        private IRandomFactory randomFactory;


        public TriangleSampler(IRandomFactory randomFactory) {
            this.randomFactory = randomFactory;
        }


        public SamplePoint sample(Triangle triangle) {
            var random = randomFactory.makeRandom();
            var s = random.NextDouble();
            var t = random.NextDouble();

            var sqrtT = Math.Sqrt(t);

            var u = sqrtT * (1 - s);
            var v = s * sqrtT;
            var p = (1 - sqrtT) * triangle.a.position + u * triangle.b.position + v * triangle.c.position;
            return new SamplePoint(p, u, v);
        }
    }

    public struct SamplePoint {
        public readonly Vector3D p;
        public readonly double u;
        public readonly double v;

        public SamplePoint(Vector3D p, double u, double v) {
            this.p = p;
            this.u = u;
            this.v = v;
        }
    }
}
