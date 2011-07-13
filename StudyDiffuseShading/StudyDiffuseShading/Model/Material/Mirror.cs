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
        private readonly Tracer tracer;

        
        public Mirror(double kr, Vector3D cr, Tracer tracer) {
            this.kr = kr;
            this.cr = cr;
            this.cacheRho = kr * cr;
            this.tracer = tracer;
        }


        #region Interface IMaterial
        public double rho() { return kr; }
        public Vector3D getLe(Collision collision) {
            return new Vector3D();
        }
        public Vector3D shade(Collision collision) {
            return kr * shadeDividedRho(collision);
        }
         public Vector3D shadeDividedRho(Collision collision) {
            var wi = MathUtil.reflectDirection(collision.normal, collision.wo);
            return MathUtil.multiply(cacheRho, tracer.traceRay(new Ray(collision.point, wi)));
        }
        #endregion Interface IMaterial
    }
}
