using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
namespace StudyDiffuseShading.Model {
    public class Window {
        private int width;
        private int height;
        private double scale;
        private Vector3D eye;


        public Window(int width, int height, double scale, Vector3D eye) {
            this.scale = scale;
            this.width = width;
            this.height = height;
            this.eye = eye;
        }


        public Ray getRay(int row, int column) {
            var screenX = scale * (column - width * 0.5 + 0.5);
            var screenY = scale * ((height - row) - height * 0.5 + 0.5);

            Ray ray = new Ray(eye, new Vector3D(screenX, screenY, 0) - eye);
            return ray;
        }
    }
}
