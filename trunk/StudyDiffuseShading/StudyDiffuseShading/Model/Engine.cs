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
            Vector3D eye = new Vector3D(0, 0, 100);
            double scale = 1.0;

            var diffuse = 0.8;
            var primitives = new Construction();

            // 右面
            primitives.add(new Triangle(
                new Vector3D(80, 80, 0),
                new Vector3D(80, -80, 0),
                new Vector3D(80, -80, 40),
                new Matte(Constant.GREEN, 0.5, diffuse)));
            primitives.add(new Triangle(
                new Vector3D(80, 80, 0),
                new Vector3D(80, -80, 40),
                new Vector3D(80, 80, 40),
                new Matte(Constant.GREEN, 0.5, diffuse)));

            // 下面
            primitives.add(new Triangle(
                new Vector3D(-80, -80, 0),
                new Vector3D(-80, -80, 40),
                new Vector3D(80, -80, 40),
                new Matte(Constant.WHITE, 0.5, diffuse)));
            primitives.add(new Triangle(
                new Vector3D(-80, -80, 0),
                new Vector3D(80, -80, 40),
                new Vector3D(80, -80, 0),
                new Matte(Constant.WHITE, 0.5, diffuse)));

            // 上面
            IMaterial emitter;
            emitter = new Emissive(Constant.WHITE, 10.0);
            primitives.add(new Triangle(
                new Vector3D(-20, 79.9, 20),
                new Vector3D(-20, 79.9, 10),
                new Vector3D(20, 79.9, 10),
                emitter));
            primitives.add(new Triangle(
                new Vector3D(-20, 79.9, 20),
                new Vector3D(20, 79.9, 10),
                new Vector3D(20, 79.9, 20),
                emitter));
            primitives.add(new Triangle(
                new Vector3D(-80, 80, 40),
                new Vector3D(-80, 80, 0),
                new Vector3D(80, 80, 0),
                new Matte(Constant.WHITE, 0.5, diffuse)));
            primitives.add(new Triangle(
                new Vector3D(-80, 80, 40),
                new Vector3D(80, 80, 0),
                new Vector3D(80, 80, 40),
                new Matte(Constant.WHITE, 0.5, diffuse)));

            // 背面
            primitives.add(new Triangle(
                new Vector3D(-80, 80, 0),
                new Vector3D(-80, -80, 0),
                new Vector3D(80, -80, 0),
                new Matte(Constant.WHITE, 0.5, diffuse)));
            primitives.add(new Triangle(
                new Vector3D(-80, 80, 0),
                new Vector3D(80, -80, 0),
                new Vector3D(80, 80, 0),
                new Matte(Constant.WHITE, 0.5, diffuse)));

            // 左面
            primitives.add(new Triangle(
                new Vector3D(-80, 80, 40),
                new Vector3D(-80, -80, 40),
                new Vector3D(-80, -80, 0),
                new Matte(Constant.RED, 0.5, diffuse)));
            primitives.add(new Triangle(
                new Vector3D(-80, 80, 40),
                new Vector3D(-80, -80, 0),
                new Vector3D(-80, 80, 0),
                new Matte(Constant.RED, 0.5, diffuse)));

            var illumination = new Illumination(new Ambient(0.1, Constant.WHITE));
            illumination.addLight(new StudyDiffuseShading.Model.Lighting.PointLight(2.0, Constant.WHITE, new Vector3D(0, 00, 50)));

            this.screen = new Screen(width, height);
            this.window = new Window(width, height, scale, eye);
            var sampler = new SimpleHemispherecalSampler();
            this.tracer = new Tracer(primitives, illumination, sampler, 10);
        }


        public Screen getResult() {
            return screen;
        }

        public void render() {
            int sampleN = 10;
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
