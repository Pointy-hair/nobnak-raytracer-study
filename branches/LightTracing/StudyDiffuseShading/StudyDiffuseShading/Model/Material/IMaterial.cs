using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using StudyDiffuseShading.Model.Sampler;
using StudyDiffuseShading.Model.Primitive;
using StudyDiffuseShading.Model.Helper;

namespace StudyDiffuseShading.Model.Material {
    public interface IMaterial {
        double rho();
        Vector3D getLe(Collision collision);
        Vector3D shade(Collision collision);
        Vector3D shadeDividedRho(Collision collision);
    }
}
