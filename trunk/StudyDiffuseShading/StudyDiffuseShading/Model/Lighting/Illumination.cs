using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyDiffuseShading.Model.Primitive;

namespace StudyDiffuseShading.Model.Lighting {
    public class Illumination {
        public readonly List<Triangle> lights;


        public Illumination() {
            this.lights = new List<Triangle>();
        }

        public void addLight(Triangle aLight) {
            lights.Add(aLight);
        }
    }
}
