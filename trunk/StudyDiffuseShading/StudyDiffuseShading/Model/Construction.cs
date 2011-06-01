using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using StudyDiffuseShading.Model.Primitive;
using StudyDiffuseShading.Model.Intersect;

namespace StudyDiffuseShading.Model {
    public class Construction {
        private List<Triangle> primitives;


        public Construction() {
            this.primitives = new List<Triangle>();
        }


        public void add(Triangle newTriangle) {
            primitives.Add(newTriangle);
        }


        public bool findNearest(Ray ray, double farDistance, out double nearest, out Triangle target) {
            nearest = double.PositiveInfinity;
            target = new Triangle();

            foreach (var primitive in primitives) {
                IntersectResult result;
                if (primitive.intersect(ray, out result) && result.t < nearest) {
                    nearest = result.t;
                    target = primitive;
                }
            }

            return nearest < farDistance;
        }
    }
}
