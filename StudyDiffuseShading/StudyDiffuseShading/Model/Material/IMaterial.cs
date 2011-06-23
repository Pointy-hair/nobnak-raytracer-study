using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using StudyDiffuseShading.Model.Sampler;
using StudyDiffuseShading.Model.Primitive;

namespace StudyDiffuseShading.Model.Material {
    public interface IMaterial {
        Vector3D shadeOnPath(Tracer tracer, IHemispherecalSampler sampler, Collision collision);
    }
}
