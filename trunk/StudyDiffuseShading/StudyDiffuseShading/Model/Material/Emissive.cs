using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using StudyDiffuseShading.Model.Sampler;
using StudyDiffuseShading.Model.Primitive;
using StudyDiffuseShading.Model.Helper;

namespace StudyDiffuseShading.Model.Material {
    public class Emissive : IMaterial {
        private readonly Vector3D ce;
        private readonly double le;
        private readonly double watte;


        public Emissive(Vector3D ce, double le, double watte) {
            this.ce = ce;
            this.le = le;
            this.watte = watte;
        }


        public double rho() { return 0; }

        public Vector3D shade(Tracer tracer, IRandomFactory randomFactory, IHemispherecalSampler sampler, Collision collision) {
            return le * ce * watte;
        }
        public Vector3D shadeDividedRho(Tracer tracer, IRandomFactory randomFactory, IHemispherecalSampler sampler, Collision collision) {
            throw new NotSupportedException();
        }
    }
}
