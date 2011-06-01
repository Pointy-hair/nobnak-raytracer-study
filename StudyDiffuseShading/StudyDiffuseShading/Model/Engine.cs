using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using StudyDiffuseShading.Model.Primitive;
using System.Windows.Media;

namespace StudyDiffuseShading.Model {
    public class Engine {
        private Construction primitives;
        private Screen screen;
        private int width = 400;
        private int height = 300;


        public Engine() {
            this.primitives = new Construction();
            this.screen = new Screen(width, height);

            primitives.add(new Triangle(new Vector3D(0, 20, 10), new Vector3D(-20, -20, 10), new Vector3D(20, -20, 10)));
        }


        public Screen getResult() {
            return screen;
        }

        public void render() {
            Vector3D eye = new Vector3D(0, 0, 100);
            double scale = 1.0;

            for (int row = 0; row < height; row++) {
                for (int column = 0; column < width; column++) {
                    var screenX = scale * (column - width * 0.5 + 0.5);
                    var screenY = scale * ((height - row) - height * 0.5 + 0.5);

                    Ray ray = new Ray(eye, new Vector3D(screenX, screenY, 0) - eye);
                    ray.normalize();

                    double nearest;
                    Triangle target;
                    screen.setPixel(row, column, 
                        primitives.findNearest(ray, double.MaxValue, out nearest, out target) ? Colors.White : Colors.Black);
                }
            }
        }
    }
}
