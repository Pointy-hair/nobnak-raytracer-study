using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using StudyDiffuseShading.Model.Helper;

namespace StudyDiffuseShading.Model.Sampler {


    public interface IHemispherecalSampler {
        Vector3D sample(IRandomFactory randomFactory);
    }

    public interface IHemispherecalSamplerFactory {
        IHemispherecalSampler makeSampler();
    }
}
