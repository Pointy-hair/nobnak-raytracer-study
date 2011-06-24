using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using StudyDiffuseShading.Model.Util;

namespace StudyDiffuseShading.Model.Material {
    public class Mirror : IMaterial {
        private double kr;
        private Vector3D cr;
        private Vector3D cacheRho;

        
        public Mirror(double kr, Vector3D cr) {
            this.kr = kr;
            this.cr = cr;
            this.cacheRho = kr * cr;
        }


        public Vector3D shade(Tracer tracer, Sampler.IHemispherecalSampler sampler, Primitive.Collision collision) {
            var wi = MathUtil.reflectDirection(collision.normal, collision.wo);
            return kr * MathUtil.multiply(cacheRho, tracer.traceRay(new Ray(collision.point, wi)));
        }
    }
}
