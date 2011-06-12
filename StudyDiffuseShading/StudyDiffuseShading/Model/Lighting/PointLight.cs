using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;

namespace StudyDiffuseShading.Model.Lighting {
    public class PointLight : ILight {
        private double ls;
        private Vector3D cl;
        private Vector3D location;


        public PointLight(double ls, Vector3D cl, Vector3D location) {
            this.ls = ls;
            this.cl = cl;
            this.location = location;
        }


        public Vector3D l() {
            return ls * cl;
        }

        public Vector3D getDirection(Vector3D point) {
            var wo = point - location;
            wo.Normalize();
            return wo;
        }
    }
}
