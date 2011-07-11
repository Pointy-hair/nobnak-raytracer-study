using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using StudyDiffuseShading.Model.Intersect;
using StudyDiffuseShading.Model.Util;
using System.Windows.Media;
using StudyDiffuseShading.Model.Material;

namespace StudyDiffuseShading.Model.Primitive {
    public struct Triangle {
        public readonly Vertex a;
        public readonly Vertex b;
        public readonly Vertex c;
        public readonly IMaterial matter;

        public readonly double area;
        public readonly int cacheHashCode;

        private RayTriangleIntersect intersectAlgorithm;


        public Triangle(Vector3D a, Vector3D b, Vector3D c, IMaterial matter) 
            : this(a, b, c, Vector3D.CrossProduct(b - a, c - a), matter) { }

        public Triangle(Vector3D posA, Vector3D posB, Vector3D posC, Vector3D commonNormal, IMaterial matter)
            : this(new Vertex(posA, commonNormal), new Vertex(posB, commonNormal), new Vertex(posC, commonNormal), matter) { }

        public Triangle(Vertex a, Vertex b, Vertex c, IMaterial matter) {
            this.a = a;
            this.b = b;
            this.c = c;
            this.matter = matter;
            this.area = Vector3D.CrossProduct(b.position - a.position, c.position - a.position).Length * 0.5;
            this.cacheHashCode = generateHash(a, b, c);

            this.intersectAlgorithm = IntersectFactory.makeRayTriangleIntersect();
        }


        public bool intersect(Ray ray, out IntersectResult result) {
            return intersectAlgorithm.intersect(ray, this, out result);
        }

        public Vector3D getNormal(double u, double v) {
            var normal = (1 - u - v) * a.normal + u * b.normal + v * c.normal;
            normal.Normalize();
            return normal;
        }

        #region Overrides
        public override int GetHashCode() {
            return cacheHashCode;
        }
        private static int generateHash(Vertex a, Vertex b, Vertex c) {
            var result = a.position.GetHashCode();
            var mod = 53;
            result = result * mod + b.position.GetHashCode();
            result = result * mod + c.position.GetHashCode();
            return result;
        }
        #endregion Overrides
    }
}
