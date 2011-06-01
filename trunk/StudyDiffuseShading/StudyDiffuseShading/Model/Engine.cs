using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using StudyDiffuseShading.Model.Primitive;
using System.Windows.Media;

namespace StudyDiffuseShading.Model {
    public class Engine {
        private int width = 400;
        private int height = 300;

        private Screen screen;
        private Window window;
        private Tracer tracer;


        public Engine() {
            Vector3D eye = new Vector3D(0, 0, 100);
            double scale = 1.0;

            var primitives = new Construction();
            primitives.add(new Triangle(new Vector3D(0, 20, 10), new Vector3D(-20, -20, 10), new Vector3D(20, -20, 10)));

            this.screen = new Screen(width, height);
            this.window = new Window(width, height, scale, eye);
            this.tracer = new Tracer(primitives);
        }


        public Screen getResult() {
            return screen;
        }

        public void render() {
            for (int row = 0; row < height; row++) {
                for (int column = 0; column < width; column++) {
                    Ray ray = window.getRay(row, column);
                    screen.setPixel(row, column, tracer.traceRay(ray));
                }
            }
        }
    }
}
