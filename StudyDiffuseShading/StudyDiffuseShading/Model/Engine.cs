using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyDiffuseShading.Model.Material;
using StudyDiffuseShading.Model.Primitive;
using StudyDiffuseShading.Model.Lighting;
using StudyDiffuseShading.Model.Util;
using System.Windows.Media.Media3D;

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
            primitives.add(new Triangle(
                new Vector3D(0, 20, 10),
                new Vector3D(-20, -20, 10),
                new Vector3D(20, -20, 10),
                new Matte(Constant.WHITE, 0.5, 1.0)));
            var illumination = new Illumination(new Ambient(0.05, Constant.WHITE));
            illumination.addLight(new StudyDiffuseShading.Model.Lighting.PointLight(2.0, Constant.WHITE, new Vector3D(0, 50, 50)));

            this.screen = new Screen(width, height);
            this.window = new Window(width, height, scale, eye);
            this.tracer = new Tracer(primitives, illumination);
        }


        public Screen getResult() {
            return screen;
        }

        public void render() {
            for (int row = 0; row < height; row++) {
                for (int column = 0; column < width; column++) {
                    Ray ray = window.getRay(row, column);
                    Vector3D color = tracer.traceRay(ray);
                    screen.setPixel(row, column, color);
                }
            }
        }
    }
}
