using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using StudyDiffuseShading.Model.Sampler;
using StudyDiffuseShading.Model.Primitive;

namespace StudyDiffuseShading.Model.Material {
    public class Emissive : IMaterial {
        private Vector3D ce;
        private double ls;


        public Emissive(Vector3D ce, double ls) {
            this.ce = ce;
            this.ls = ls;
        }


        public Vector3D shade(Tracer tracer, IHemispherecalSampler sampler, Collision collision) {
            return ce * ls;
        }
    }
}
