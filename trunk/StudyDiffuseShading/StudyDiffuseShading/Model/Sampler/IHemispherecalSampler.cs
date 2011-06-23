using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;

namespace StudyDiffuseShading.Model.Sampler {


    public interface IHemispherecalSampler {
        Vector3D sample();
    }
}
