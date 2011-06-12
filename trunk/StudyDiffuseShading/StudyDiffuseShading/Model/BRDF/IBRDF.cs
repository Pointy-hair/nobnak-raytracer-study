using System;
using System.Windows.Media.Media3D;


namespace StudyDiffuseShading.Model.BRDF {

    interface IBRDF {
        Vector3D f(Vector3D p, Vector3D wo, Vector3D wi);
        Vector3D rho();
    }
}
