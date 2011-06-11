using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using StudyDiffuseShading.Model.Util;

namespace StudyDiffuseShading.Model.BRDF {
    public class Lambertian {
        private double kd;
        private Color cd;

        private Color tmpRho;
        private Color tmpF;


        public Lambertian(double kd, Color cd) {
            this.kd = kd;
            this.cd = cd;
            this.tmpRho = Color.Multiply(cd, (float)kd);
            this.tmpF = Color.Multiply(tmpRho, (float) Constant.INV_PI);
        }


        public Color f() {
            return tmpF;
        }

        public Color rho() {
            return tmpRho;
        }
    }
}
