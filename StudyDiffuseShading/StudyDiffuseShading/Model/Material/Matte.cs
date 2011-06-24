using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using StudyDiffuseShading.Model.BRDF;
using System.Windows.Media.Media3D;
using StudyDiffuseShading.Model.Lighting;
using StudyDiffuseShading.Model.Util;
using StudyDiffuseShading.Model.Primitive;
using StudyDiffuseShading.Model.Sampler;

namespace StudyDiffuseShading.Model.Material {
    public class Matte : IMaterial {
        private Lambertian diffuse;


        public Matte(Vector3D color, double diffuse) {
            this.diffuse = new Lambertian(diffuse, color);
        }

        public Vector3D shade(Tracer tracer, IHemispherecalSampler sampler, Collision collision) {
            Vector3D wi = SamplerUtil.sampleWi(collision.normal, sampler);
            Vector3D li = tracer.traceRay(new Ray(collision.point, wi));

            return diffuse.calcLo(collision.point, collision.wo, wi, collision.normal, li);
        }
    }
}
