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
using System.Threading.Tasks;

namespace StudyDiffuseShading.Model {
    public class Engine {

        private int width = 400;
        private int height = 300;
        private int sampleN = 10;
        private int depth = 10;

        private Screen screen;
        private Window window;
        private Func<Tracer> tracerFactory;
        private Matrix3D camera;


        public Engine(Matrix3D camera) {
            var diffuse = 0.8;
            var scale = 0.2;
            var distance = 100.0;
            this.camera = camera;


            this.screen = new Screen(width, height);
            this.window = new Window(width, height, scale, distance, camera);

            var primitives = new Construction();
            var seedFactory = new Random();
            this.tracerFactory = () => {
                var sampler = new SimpleHemispherecalSampler(seedFactory.Next());
                return new Tracer(primitives, new Ambient(0.05, Constant.WHITE), sampler, depth);
            };

            ExampleUtil.buildCornelBox(primitives, diffuse);
        }


        public Screen getResult() {
            return screen;
        }

        public void render() {
            var sampler = new JitteredSampler(sampleN);
            Vector3D[] colors = new Vector3D[sampler.Count];
            Tracer[] tracers = new Tracer[sampler.Count];
            foreach (var i in Enumerable.Range(0, sampler.Count))
                tracers[i] = tracerFactory();

            for (int row = 0; row < height; row++) {
                for (int column = 0; column < width; column++) {
                    Vector3D sum = new Vector3D();
#if true
                    Parallel.ForEach(sampler.getSampler(), (sample, state, i) => {
                        var ray = window.getRay(row + sample.X, column + sample.Y);
                        var tracer = tracers[i];
                        colors[i] = tracer.traceRay(ray);
                    });
                    foreach (var color in colors)
                        sum += color;
#else
                    foreach (var sample in sampler.getSampler()) {
                        var ray = window.getRay(row + sample.X, column + sample.Y);
                        var tracer = tracers[0];
                        sum += tracer.traceRay(ray);
                    }
#endif
                    screen.setPixel(row, column, sum / sampler.Count);

                }
            }
        }
    }
}
