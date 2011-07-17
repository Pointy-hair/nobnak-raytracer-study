using System;
using StudyDiffuseShading.Model.Primitive;
using System.Windows.Media.Media3D;
namespace StudyDiffuseShading.Model.Lighting {
    public interface IIllumination {
        void addLight(Triangle aLight);
        Vector3D LeALight(Vector3D pointIlluminated, Vector3D normalIlluminated, out Vector3D wi);
        bool hasLight(Triangle aLight);
    }
}
