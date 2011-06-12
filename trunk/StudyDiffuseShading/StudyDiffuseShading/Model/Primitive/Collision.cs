using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;

namespace StudyDiffuseShading.Model.Primitive {
    public struct Collision {
        public readonly Vector3D point;
        public readonly Vector3D wo;
        public readonly Vector3D normal;

        public Collision(Vector3D point, Vector3D wo, Vector3D normal) {
            this.point = point;
            this.wo = wo;
            this.normal = normal;
        }
    }
}
