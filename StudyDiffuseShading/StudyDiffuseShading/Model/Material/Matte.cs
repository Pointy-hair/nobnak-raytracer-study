using System;
using System.Windows.Media.Media3D;
using StudyDiffuseShading.Model.Util;
using StudyDiffuseShading.Model.Primitive;
using StudyDiffuseShading.Model.Sampler;
using StudyDiffuseShading.Model.Helper;
using StudyDiffuseShading.Model.Lighting;

namespace StudyDiffuseShading.Model.Material {
    public class Matte : IMaterial {
        private readonly double kd;
        private readonly Vector3D colorDiffuse;
        private Construction primitives;
        private IIllumination lights;
        private Tracer tracer;
        private IRandomFactory randomFactory;
        private IHemispherecalSamplerFactory hemiSamplerFactory;

        private Vector3D cacheRho;
        private Vector3D cacheF;
        private Func<Triangle, bool> lightFilter;


        public Matte(double kd, Vector3D cd, Construction primitives, IIllumination lights, 
            Tracer tracer, IRandomFactory randomFactory, IHemispherecalSamplerFactory hemiSamplerFactory) {
            this.kd = kd;
            this.colorDiffuse = cd;
            this.primitives = primitives;
            this.lights = lights;
            this.tracer = tracer;
            this.randomFactory = randomFactory;
            this.hemiSamplerFactory = hemiSamplerFactory;

            this.cacheRho = kd * cd;
            this.cacheF = Constant.INV_PI * cacheRho;
            this.lightFilter = (t) => lights.hasLight(t);
        }


        #region Interface IMaterial
        public double rho() { return kd; }
        public Vector3D getLe(Collision collision) {
            return new Vector3D();
        }
        public Vector3D shade(Collision collision) {
            Vector3D result = Constant.BLACK;

            result += shadeDirectTerm(collision.point, collision.normal);
            result += shadeIndirectTerm(ref collision);

            return result;
        }
        public Vector3D shadeDividedRho(Collision collision) {
            var hemiSampler = hemiSamplerFactory.makeSampler();
            Vector3D wi = SamplerUtil.sampleWi(collision.normal, hemiSampler, randomFactory);
            Vector3D li = tracer.traceRay(new Ray(collision.point, wi), lightFilter);

            return MathUtil.multiply(colorDiffuse, li);
        }
        #endregion Interface IMaterial

        private Vector3D shadeDirectTerm(Vector3D pointIlluminated, Vector3D normalIlluminated) {
            Vector3D wi;
            var le = lights.LeALight(pointIlluminated, normalIlluminated, out wi);
            var coswi = Vector3D.DotProduct(normalIlluminated, wi);
            if (coswi <= Constant.EPSILON)
                return Constant.BLACK;

            return MathUtil.multiply(cacheF, le) * coswi;
        }
        private Vector3D shadeIndirectTerm(ref Collision collision) {
            var random = randomFactory.makeRandom();
            var threthold = rho();
            if (random.NextDouble() >= threthold)
                return Constant.BLACK;

            return shadeDividedRho(collision);
        }
    }
}
