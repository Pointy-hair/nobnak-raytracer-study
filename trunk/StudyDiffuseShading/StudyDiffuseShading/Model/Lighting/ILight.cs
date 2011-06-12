using System;
using System.Windows.Media.Media3D;

namespace StudyDiffuseShading.Model.Lighting {

    public interface ILight {
        Vector3D l();
        Vector3D getDirection(Vector3D point);
    }
}
