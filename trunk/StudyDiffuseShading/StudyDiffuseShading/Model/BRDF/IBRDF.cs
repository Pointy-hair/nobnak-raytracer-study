using System;
using System.Windows.Media.Media3D;
using StudyDiffuseShading.Model.Sampler;
using StudyDiffuseShading.Model.Primitive;


namespace StudyDiffuseShading.Model.BRDF {

    interface IBRDF {
        Vector3D f(Vector3D p, Vector3D wo, Vector3D wi);
        Vector3D rho();
        Vector3D calcLo(Vector3D p, Vector3D wo, Vector3D wi, Vector3D n, Vector3D li);
    }
}
