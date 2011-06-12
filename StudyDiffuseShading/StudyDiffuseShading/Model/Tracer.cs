using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using StudyDiffuseShading.Model.Material;
using StudyDiffuseShading.Model.Primitive;
using StudyDiffuseShading.Model.Util;
using StudyDiffuseShading.Model.Lighting;

namespace StudyDiffuseShading.Model {
    public class Tracer {
        private Construction construction;
        private Illumination illumination;


        public Tracer(Construction construction, Illumination illumination) {
            this.construction = construction;
            this.illumination = illumination;
        }


        public Vector3D traceRay(Ray ray) {
            double nearest;
            Triangle target;
            Collision collision;
            if (!construction.findNearest(ray, double.MaxValue, out nearest, out target, out collision))
                return Constant.BLACK;

            return target.matter.shade(illumination, collision);
        }
    }
}
