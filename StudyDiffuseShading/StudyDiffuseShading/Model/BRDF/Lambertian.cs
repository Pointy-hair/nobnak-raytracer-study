using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using StudyDiffuseShading.Model.Util;
using System.Windows.Media.Media3D;
using StudyDiffuseShading.Model.Sampler;

namespace StudyDiffuseShading.Model.BRDF {
    public class Lambertian : StudyDiffuseShading.Model.BRDF.IBRDF {
        private double kd;
        private Vector3D colorDiffuse;

        private Vector3D cacheRho;
        private Vector3D cacheF;


        public Lambertian(double kd, Vector3D cd) {
            this.kd = kd;
            this.colorDiffuse = cd;
            this.cacheRho = kd * cd;
            this.cacheF = Constant.INV_PI * cacheRho;
        }


        public Vector3D f(Vector3D p, Vector3D wo, Vector3D wi) {
            return cacheF;
        }

        public Vector3D sampleF(Vector3D p, Vector3D wo, Vector3D n, ISampler sampler, out Vector3D wi, out double pdf) {
            Vector3D x, y;
            MathUtil.generateXYFromZ(n, out x, out y);

            Vector3D sample = sampler.sampleOnHemisphere();
            wi = sample.X * x + sample.Y * y + sample.Z * n;
            wi.Normalize();

            double cosTheta = Vector3D.DotProduct(n, wi);
            double sinTheta = Math.Sqrt(1.0 - cosTheta * cosTheta);
            pdf = cosTheta * sinTheta * Constant.INV_PI;

            return cacheF;
        }

        public Vector3D rho() {
            return cacheRho;
        }
    }
}
