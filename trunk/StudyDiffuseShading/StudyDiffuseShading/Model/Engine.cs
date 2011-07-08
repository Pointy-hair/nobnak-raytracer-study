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
using StudyDiffuseShading.Model.Helper;

namespace StudyDiffuseShading.Model {
    public class Engine {
        private int width = 400;
        private int height = 300;
        private int sampleN = 1;

        private Screen screen;
        private Window window;
        private Func<Tracer> tracerFactory;
        private Matrix3D camera;


        public Engine(Matrix3D camera) {
            var diffuse = 0.95;
            var scale = 0.25;
            var distance = 100.0;
            this.camera = camera;


            this.screen = new Screen(width, height);
            this.window = new Window(width, height, scale, distance, camera);

            var primitives = new Construction();
            var seedFactory = new Random();
            this.tracerFactory = () => {
                var randomFactory = new RandomFactory();
                var samplerFactory = new HemiSamplerFactory();
                return new Tracer(primitives, new Ambient(0.05, Constant.WHITE), randomFactory, samplerFactory);
            };

            ExampleUtil.buildCornelBox(primitives, diffuse);
        }


        public int SampleNum {
            get { return sampleN; }
            set {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("SampleNum shoud be positive");
                sampleN = value;
            }
        }


        private class HemiSamplerFactory : IHemispherecalSamplerFactory {
            private IHemispherecalSampler sampler;

            public HemiSamplerFactory() {
                this.sampler = new SimpleHemispherecalSampler();
            }
            public IHemispherecalSampler makeSampler() {
                return sampler;
            }
        }


        public Screen getResult() {
            return screen;
        }

        public void render() {
            var sampler = new JitteredSampler(sampleN);
            Vector3D[] colors = new Vector3D[sampler.Count];
            Tracer tracer = tracerFactory();

            for (int row = 0; row < height; row++) {
                for (int column = 0; column < width; column++) {
                    Vector3D sum = new Vector3D();
                    Parallel.ForEach(sampler.getSampler(), (sample, state, i) => {
                        var ray = window.getRay(row + sample.X, column + sample.Y);
                        colors[i] = tracer.traceRay(ray);
                    });
                    foreach (var color in colors)
                        sum += color;
                    screen.setPixel(row, column, sum / sampler.Count);

                }
            }
        }
    }
}
