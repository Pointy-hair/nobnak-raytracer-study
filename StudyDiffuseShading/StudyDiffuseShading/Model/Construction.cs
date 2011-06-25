using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using StudyDiffuseShading.Model.Material;
using StudyDiffuseShading.Model.Intersect;
using StudyDiffuseShading.Model.Primitive;
using StudyDiffuseShading.Model.Util;

namespace StudyDiffuseShading.Model {
    public class Construction {
        private List<Triangle> primitives;


        public Construction() {
            this.primitives = new List<Triangle>();
        }


        public void add(Triangle newTriangle) {
            primitives.Add(newTriangle);
        }


        public bool findNearest(Ray ray, double farDistance, out double nearest, out Triangle target, out Collision collision) {
            nearest = double.PositiveInfinity;
            target = new Triangle();

            IntersectResult result = new IntersectResult();
            foreach (var primitive in primitives) {
                IntersectResult tmpResult;
                if (primitive.intersect(ray, out tmpResult) && tmpResult.t < nearest) {
                    result = tmpResult;
                    nearest = tmpResult.t;
                    target = primitive;
                }
            }

            var found = Constant.EPSILON < nearest && nearest < farDistance;

            collision = new Collision();
            if (found) {
                collision = new Collision(ray.origin + result.t * ray.direction, -ray.direction, target.getNormal(result.u, result.v));
            }

            return found;
        }
    }
}
