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
        private readonly double ls;


        public Emissive(Vector3D ce, double ls) {
            this.ce = ce;
            this.ls = ls;
        }


        public double rho() { return 1; }

        public Vector3D shade(Tracer tracer, IRandomFactory randomFactory, IHemispherecalSampler sampler, Collision collision) {
            return ls * shadeDividedRho(tracer, randomFactory, sampler, collision);
        }
        public Vector3D shadeDividedRho(Tracer tracer, IRandomFactory randomFactory, IHemispherecalSampler sampler, Collision collision) {
            return ce;
        }
    }
}
