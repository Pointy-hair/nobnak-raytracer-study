using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;

namespace StudyDiffuseShading.Model.Lighting {
    public class Ambient : ILight {
        private double ls;
        private Vector3D color;


        public Ambient(double ls, Vector3D color) {
            this.ls = ls;
            this.color = color;
        }
        

        public Vector3D l() {
            return ls * color;
        }


        public Vector3D getDirection(Vector3D point) {
            return new Vector3D();
        }
    }
}
