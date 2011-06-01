using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyDiffuseShading.Model.Intersect;

namespace StudyDiffuseShading.Model.Util {
    public static class IntersectFactory {

        public static RayTriangleIntersect makeRayTriangleIntersect() {
            return new RayTriangleIntersect();
        }
    }
}
