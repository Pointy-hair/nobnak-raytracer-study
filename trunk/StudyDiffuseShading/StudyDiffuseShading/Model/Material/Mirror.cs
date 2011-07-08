using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using StudyDiffuseShading.Model.Util;
using StudyDiffuseShading.Model.Helper;
using StudyDiffuseShading.Model.Sampler;
using StudyDiffuseShading.Model.Primitive;

namespace StudyDiffuseShading.Model.Material {
    public class Mirror : IMaterial {
        private readonly double kr;
        private readonly Vector3D cr;
        private readonly Vector3D cacheRho;

        
        public Mirror(double kr, Vector3D cr) {
            this.kr = kr;
            this.cr = cr;
            this.cacheRho = kr * cr;
        }


        public double rho() { return kr; }

        public Vector3D shade(Tracer tracer, IRandomFactory randomFactory, IHemispherecalSampler sampler, Collision collision) {
            return kr * shadeDividedRho(tracer, randomFactory, sampler, collision);
        }
         public Vector3D shadeDividedRho(Tracer tracer, IRandomFactory randomFactory, IHemispherecalSampler sampler, Collision collision) {
            var wi = MathUtil.reflectDirection(collision.normal, collision.wo);
            return MathUtil.multiply(cacheRho, tracer.traceRay(new Ray(collision.point, wi)));
        }
   }
}
