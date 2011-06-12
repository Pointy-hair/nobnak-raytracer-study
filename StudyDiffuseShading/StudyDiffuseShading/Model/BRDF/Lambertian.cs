using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using StudyDiffuseShading.Model.Util;
using System.Windows.Media.Media3D;

namespace StudyDiffuseShading.Model.BRDF {
    public class Lambertian : StudyDiffuseShading.Model.BRDF.IBRDF {
        private double kd;
        private Vector3D colorDiffuse;

        private Vector3D tmpRho;
        private Vector3D tmpF;


        public Lambertian(double kd, Vector3D cd) {
            this.kd = kd;
            this.colorDiffuse = cd;
            this.tmpRho = kd * cd;
            this.tmpF = Constant.INV_PI * tmpRho;
        }


        public Vector3D f(Vector3D p, Vector3D wo, Vector3D wi) {
            return tmpF;
        }

        public Vector3D rho() {
            return tmpRho;
        }
    }
}
