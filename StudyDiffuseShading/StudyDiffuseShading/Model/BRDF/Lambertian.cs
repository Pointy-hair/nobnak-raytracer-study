using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using StudyDiffuseShading.Model.Util;
using System.Windows.Media.Media3D;
using StudyDiffuseShading.Model.Sampler;

namespace StudyDiffuseShading.Model.BRDF {
    public class Lambertian : IBRDF {
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

        public Vector3D rho() {
            return cacheRho;
        }


        public Vector3D calcLo(Vector3D p, Vector3D wo, Vector3D wi, Vector3D n, Vector3D li) {
            Vector3D sampleF = f(p, wo, wi);

            return MathUtil.multiply(sampleF, li) * Math.PI;
        }
    }
}
