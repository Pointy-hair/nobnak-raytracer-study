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
    public struct Matte {
        private Lambertian ambient;
        private Lambertian diffuse;


        public Matte(Vector3D color, double ambient, double diffuse) {
            this.ambient = new Lambertian(ambient, color);
            this.diffuse = new Lambertian(diffuse, color);
        }

        public Vector3D shade(Illumination illumination, Collision collision) {
            var l = MathUtil.multiply(ambient.rho(), illumination.ambient.l());
            
            foreach (var light in illumination.lights) {
                var wi = - light.getDirection(collision.point);
                var ndotwi = Vector3D.DotProduct(collision.normal, wi);
                if (ndotwi > Constant.EPSILON)
                    l += MathUtil.multiply(diffuse.f(collision.point, collision.wo, wi), light.l()) * ndotwi;
            }

            return l;
        }

        public Vector3D shadeOnPath(Tracer tracer, ISampler sampler, Collision collision) {
            Vector3D wi;
            double pdf;
            Vector3D f = diffuse.sampleF(collision.point, collision.wo, collision.normal, sampler, out wi, out pdf);

            double ndotwi = Vector3D.DotProduct(collision.normal, wi);
            return MathUtil.multiply(f, tracer.traceRay(new Ray(collision.point, wi))) * ndotwi / pdf;
        }
    }
}
