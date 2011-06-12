using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudyDiffuseShading.Model.Lighting {
    public class Illumination {
        public readonly Ambient ambient;
        public readonly List<ILight> lights;


        public Illumination(Ambient ambient) {
            this.ambient = ambient;
            this.lights = new List<ILight>();
        }

        public void addLight(ILight aLight) {
            lights.Add(aLight);
        }
    }
}
