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

        private Screen screen;
        private Window window;
        private Func<Tracer> tracerFactory;


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

            // 照明
            //343.0 548.8 227.0
            //343.0 548.8 332.0
            //213.0 548.8 332.0
            //213.0 548.8 227.0
            primitives.add(new Triangle(
                new Vector3D(343.0, 548, 227.0),
                new Vector3D(343.0, 548, 332.0),
                new Vector3D(213.0, 548, 332.0),
                emitter));
            primitives.add(new Triangle(
                new Vector3D(343.0, 548, 227.0),
                new Vector3D(213.0, 548, 332.0),
                new Vector3D(213.0, 548, 227.0),
                emitter));

            // 上面
            //556.0 548.8   0.0
            //556.0 548.8 559.2
            //0.0 548.8 559.2
            //0.0 548.8   0.0
            primitives.add(new Triangle(
                new Vector3D(556.0, 548.8, 0.0),
                new Vector3D(556.0, 548.8, 559.2),
                new Vector3D(0.0, 548.8, 559.2),
                matte));
            primitives.add(new Triangle(
                new Vector3D(556.0, 548.8, 0.0),
                new Vector3D(0.0, 548.8, 559.2),
                new Vector3D(0.0, 548.8, 0.0),
                matte));

            // 背面
            //549.6   0.0 559.2
            //0.0   0.0 559.2
            //0.0 548.8 559.2
            //556.0 548.8 559.2
            primitives.add(new Triangle(
                new Vector3D(549.6, 0.0, 559.2),
                new Vector3D(0.0, 0.0, 559.2),
                new Vector3D(0.0, 548.8, 559.2),
                matte));
            primitives.add(new Triangle(
                new Vector3D(549.6, 0.0, 559.2),
                new Vector3D(0.0, 548.8, 559.2),
                new Vector3D(556.0, 548.8, 559.2),
                matte));

            // 左面
            //0.0   0.0 559.2   
            //0.0   0.0   0.0
            //0.0 548.8   0.0
            //0.0 548.8 559.2
            primitives.add(new Triangle(
                new Vector3D(0.0, 0.0, 559.2),
                new Vector3D(0.0, 0.0, 0.0),
                new Vector3D(0.0, 548.8, 0.0),
                leftMaterial));
            primitives.add(new Triangle(
                new Vector3D(0.0, 0.0, 559.2),
                new Vector3D(0.0, 548.8, 0.0),
                new Vector3D(0.0, 548.8, 559.2),
                leftMaterial));

            // 右面
            //552.8   0.0   0.0
            //549.6   0.0 559.2
            //556.0 548.8 559.2
            //556.0 548.8   0.0
            primitives.add(new Triangle(
                new Vector3D(552.8, 0.0, 0.0),
                new Vector3D(549.6, 0.0, 559.2),
                new Vector3D(556.0, 548.8, 559.2),
                rightMaterial));
            primitives.add(new Triangle(
                new Vector3D(552.8, 0.0, 0.0),
                new Vector3D(556.0, 548.8, 559.2),
                new Vector3D(556.0, 548.8, 0.0),
                rightMaterial));


            this.screen = new Screen(width, height);
            this.window = new Window(width, height, scale, eye);
            var sampler = new SimpleHemispherecalSampler();
            this.tracerFactory = () => {
                return new Tracer(primitives, new Ambient(0.05, Constant.WHITE), sampler, 10);
            };
        }


        public Screen getResult() {
            return screen;
        }

        public void render() {
            int sampleN = 10;
            var sampler = new JitteredSampler(sampleN);
            Vector3D[] colors = new Vector3D[sampler.Count];
            Tracer[] tracers = new Tracer[sampler.Count];
            foreach (var i in Enumerable.Range(0, sampler.Count))
                tracers[i] = tracerFactory();

            for (int row = 0; row < height; row++) {
                for (int column = 0; column < width; column++) {
                    Parallel.ForEach(sampler.getSampler(), (sample, state, i) => {
                        var ray = window.getRay(row + sample.X, column + sample.Y);
                        var tracer = tracers[i];
                        colors[i] = tracer.traceRay(ray);
                    });
                    Vector3D sum = new Vector3D();
                    foreach (var color in colors)
                        sum += color;
                    screen.setPixel(row, column, sum / sampler.Count);

                }
            }
        }
    }
}
