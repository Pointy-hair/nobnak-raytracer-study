using System;
using System.Windows.Media.Media3D;
using StudyDiffuseShading.Model.Util;
using StudyDiffuseShading.Model.Primitive;
using StudyDiffuseShading.Model.Sampler;
using StudyDiffuseShading.Model.Helper;

namespace StudyDiffuseShading.Model.Material {
    public class Matte : IMaterial {
        private readonly double kd;
        private readonly Vector3D colorDiffuse;

        private Vector3D cacheRho;
        private Vector3D cacheF;


        public Matte(double kd, Vector3D cd) {
            this.kd = kd;
            this.colorDiffuse = cd;
            this.cacheRho = kd * cd;
            this.cacheF = Constant.INV_PI * cacheRho;
        }


        public double rho() { return kd; }

        public Vector3D shade(Tracer tracer, IRandomFactory randomFactory, IHemispherecalSampler sampler, Collision collision) {
            var random = randomFactory.makeRandom();
            var threthold = rho();
            if (random.NextDouble() >= threthold)
                return Constant.BLACK;

            return shadeDividedRho(tracer, randomFactory, sampler, collision);
        }
        public Vector3D shadeDividedRho(Tracer tracer, IRandomFactory randomFactory, IHemispherecalSampler sampler, Collision collision) {
            Vector3D wi = SamplerUtil.sampleWi(collision.normal, sampler, randomFactory);
            Vector3D li = tracer.traceRay(new Ray(collision.point, wi));

            return MathUtil.multiply(colorDiffuse, li);
        }
    }
}
