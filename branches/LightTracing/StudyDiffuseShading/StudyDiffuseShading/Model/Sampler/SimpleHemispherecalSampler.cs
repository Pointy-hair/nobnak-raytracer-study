using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using StudyDiffuseShading.Model.Helper;

namespace StudyDiffuseShading.Model.Sampler {
    public class SimpleHemispherecalSampler : IHemispherecalSampler {


        public SimpleHemispherecalSampler() {
        }

        
        public Vector3D sample(IRandomFactory randomFactory) {
            double rand1, rand2;

            var randomer = randomFactory.makeRandom();

            rand1 = randomer.NextDouble();
            rand2 = randomer.NextDouble();

            double cosPhi = Math.Cos(2.0 * Math.PI * rand1);
            double sinPhi = Math.Sin(2.0 * Math.PI * rand1);

            double cosTheta2 = 1.0 - rand2;
            double cosTheta = Math.Sqrt(cosTheta2);
            double sinTheta = Math.Sqrt(1.0 - cosTheta2);

            var u = sinTheta * cosPhi;
            var v = sinTheta * sinPhi;
            var w = cosTheta;

            return new Vector3D(u, v, w);
        }
    }
}
