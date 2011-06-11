using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using StudyDiffuseShading.Model.Material;
using StudyDiffuseShading.Model.Primitive;
using StudyDiffuseShading.Model.Util;

namespace StudyDiffuseShading.Model {
    public class Tracer {
        private Construction construction;


        public Tracer(Construction construction) {
            this.construction = construction;
        }


        public Vector3D traceRay(Ray ray) {
            double nearest;
            Triangle target;
            if (!construction.findNearest(ray, double.MaxValue, out nearest, out target))
                return Constant.BLACK;

            return Constant.WHITE;
        }
    }
}
