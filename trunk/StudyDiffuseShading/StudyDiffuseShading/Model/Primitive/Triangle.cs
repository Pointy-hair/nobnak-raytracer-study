using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using StudyDiffuseShading.Model.Intersect;
using StudyDiffuseShading.Model.Util;

namespace StudyDiffuseShading.Model.Primitive {
    public struct Triangle {
        public readonly Vector3D a;
        public readonly Vector3D b;
        public readonly Vector3D c;

        private RayTriangleIntersect intersectAlgorithm;

        public Triangle(Vector3D a, Vector3D b, Vector3D c) {
            this.a = a;
            this.b = b;
            this.c = c;

            this.intersectAlgorithm = IntersectFactory.makeRayTriangleIntersect();
        }


        public bool intersect(Ray ray, out IntersectResult result) {
            return intersectAlgorithm.intersect(ray, this, out result);
        }        
    }
}
