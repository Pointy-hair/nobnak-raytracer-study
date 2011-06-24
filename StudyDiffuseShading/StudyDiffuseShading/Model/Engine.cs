using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyDiffuseShading.Model.Material;
using StudyDiffuseShading.Model.Primitive;
using StudyDiffuseShading.Model.Lighting;
using StudyDiffuseShading.Model.Util;
using System.Windows.Media.Media3D;
using StudyDiffuseShading.Model.Sampler;
using System.Diagnostics;

namespace StudyDiffuseShading.Model {
    public class Engine {
        private int width = 400;
        private int height = 300;

        private Screen screen;
        private Window window;
        private Tracer tracer;


        public Engine() {
            Vector3D eye = new Vector3D(278, 273, -800);
            double scale = 2;

            var diffuse = 0.8;
            var specular = 1.0;
            var primitives = new Construction();

            IMaterial matte = new Matte(Constant.WHITE, diffuse);
            IMaterial emitter = new Emissive(Constant.WHITE, 10.0);
# if false
            var rightMaterial = new Mirror(specular, Constant.GREEN);
            var leftMaterial = new Mirror(specular, Constant.RED); 
# else
            var rightMaterial = new Matte(Constant.GREEN, diffuse);
            var leftMaterial = new Matte(Constant.RED, diffuse);
# endif

            // 下面
            //white
            //552.8 0.0   0.0   
            //  0.0 0.0   0.0
            //  0.0 0.0 559.2
            //549.6 0.0 559.2
            primitives.add(new Triangle(
                new Vector3D(552.8, 0.0, 0.0),
                new Vector3D(0.0, 0.0, 0.0),
                new Vector3D(0.0, 0.0, 559.2),
                matte));
            primitives.add(new Triangle(
                new Vector3D(552.8, 0.0, 0.0),
                new Vector3D(0.0, 0.0, 559.2),
                new Vector3D(549.6, 0.0, 559.2),
                matte));

            // 右面

            // 上面

            // 背面

            // 左面

            this.screen = new Screen(width, height);
            this.window = new Window(width, height, scale, eye);
            var sampler = new SimpleHemispherecalSampler();
            this.tracer = new Tracer(primitives, new Ambient(0.05, Constant.WHITE), sampler, 10);
        }


        public Screen getResult() {
            return screen;
        }

        public void render() {
            int sampleN = 1;
            var sampler = new JitteredSampler(sampleN);

            for (int row = 0; row < height; row++) {
                for (int column = 0; column < width; column++) {
                    Vector3D color = new Vector3D();
                    foreach (var sample in sampler.getSampler()) {
                        var ray = window.getRay(row + sample.X, column + sample.Y);
                        color += tracer.traceRay(ray);
                    }
                    color /= sampler.Count;
                    screen.setPixel(row, column, color);

                }
            }
        }
    }
}
