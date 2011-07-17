using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using StudyDiffuseShading.Model.Helper;

namespace StudyDiffuseShading.Model.Sampler {
    public class ConstantSampler : IHemispherecalSampler {
        private Vector3D wi;


        public ConstantSampler(Vector3D wi) {
            this.wi = wi;
            this.wi.Normalize();
        }


        public Vector3D sample(IRandomFactory randomFactory) {
            return wi;
        }
    }
}
