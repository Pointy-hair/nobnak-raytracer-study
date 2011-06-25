using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using StudyDiffuseShading.Model.Util;
namespace StudyDiffuseShading.Model {
    public class Window {
        private readonly Point3D DEFAULT_EYE = new Point3D(0, 0, 0);

        private int width;
        private int height;
        private double scale;
        private double distance;
        private Matrix3D camera;


        public Window(int width, int height, double scale, double distance, Matrix3D camera) {
            this.scale = scale;
            this.width = width;
            this.height = height;
            this.distance = distance;
            this.camera = camera;
        }


        public Ray getRay(double row, double column) {
            var screenX = scale * (column - width * 0.5);
            var screenY = scale * ((height - row) - height * 0.5);

            var eye = Point3D.Multiply(DEFAULT_EYE, camera);
            var lookat = new Vector3D(screenX, screenY, -distance);
            var dir = Vector3D.Multiply(lookat, camera);

            Ray ray = new Ray(MathUtil.convertToVector(eye), dir);
            return ray;
        }
    }
}
