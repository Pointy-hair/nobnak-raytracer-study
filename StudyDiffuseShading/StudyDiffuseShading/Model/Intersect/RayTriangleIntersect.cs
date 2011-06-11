using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using StudyDiffuseShading.Model.Util;
using StudyDiffuseShading.Model.Material;
using System.Windows.Media;
using StudyDiffuseShading.Model.Primitive;

namespace StudyDiffuseShading.Model.Intersect {
    public class RayTriangleIntersect {
        public bool intersect(Ray ray, Triangle triangle, out IntersectResult result) {
            result = new IntersectResult();

            // from Fast, minimum storage Ray/Triangle intersection
            var e1 = triangle.b.position - triangle.a.position;
            var e2 = triangle.c.position - triangle.a.position;

            var p = Vector3D.CrossProduct(ray.direction, e2);

            var det = Vector3D.DotProduct(p, e1);
            if (det < Constant.EPSILON)
                return false;

            var t = ray.origin - triangle.a.position;

            var uNumerator = Vector3D.DotProduct(p, t);
            if (uNumerator < 0 || det < uNumerator)
                return false;

            var q = Vector3D.CrossProduct(t, e1);

            var vNumerator = Vector3D.DotProduct(q, ray.direction);
            if (vNumerator < 0 || det < (uNumerator + vNumerator))
                return false;

            var tNumerator = Vector3D.DotProduct(q, e2);
            var invDet = 1 / det;
            result = new IntersectResult(tNumerator * invDet, uNumerator * invDet, vNumerator * invDet, triangle);

            return true;
        }
    }
}
