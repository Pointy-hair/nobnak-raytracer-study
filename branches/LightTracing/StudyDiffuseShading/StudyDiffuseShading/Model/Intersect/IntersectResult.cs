using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyDiffuseShading.Model.Material;
using StudyDiffuseShading.Model.Primitive;

namespace StudyDiffuseShading.Model.Intersect {
    public struct IntersectResult {
        public readonly double t;
        public readonly double u;
        public readonly double v;
        public readonly Triangle primitive;


        public IntersectResult(double t, double u, double v, Triangle primitive) {
            this.t = t;
            this.u = u;
            this.v = v;
            this.primitive = primitive;
        }
    }
}
