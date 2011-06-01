using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace StudyDiffuseShading.Model {
    public class Tracer {
        private Construction construction;


        public Tracer(Construction construction) {
            this.construction = construction;
        }


        public Color traceRay(Vector3D ray) {
            return new Color();
        }
    }
}
