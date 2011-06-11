using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using StudyDiffuseShading.Model.BRDF;

namespace StudyDiffuseShading.Model.Material {
    public struct Matte {
        private Lambertian ambient;
        private Lambertian diffuse;


        public Matte(Color color, double ambient, double diffuse) {
            this.ambient = new Lambertian(ambient, color);
            this.diffuse = new Lambertian(diffuse, color);
        }
    }
}
