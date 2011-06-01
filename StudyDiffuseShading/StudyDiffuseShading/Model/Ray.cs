using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;

namespace StudyDiffuseShading.Model {
    public struct Ray {
        public readonly Vector3D origin;
        public readonly Vector3D direction;


        public Ray(Vector3D origin, Vector3D direction) {
            this.origin = origin;
            this.direction = direction;
        }

        public void normalize() {
            direction.Normalize();
        }
    }
}
