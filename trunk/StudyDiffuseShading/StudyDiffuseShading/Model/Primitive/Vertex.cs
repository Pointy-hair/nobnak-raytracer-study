using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;

namespace StudyDiffuseShading.Model.Primitive {
    public struct Vertex {
        public readonly Vector3D position;
        public readonly Vector3D normal;


        public Vertex(Vector3D position, Vector3D normal) {
            this.position = position;
            this.normal = normal;
        }
    }
}
