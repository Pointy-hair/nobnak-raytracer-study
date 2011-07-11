using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyDiffuseShading.Model.Primitive;
using StudyDiffuseShading.Model.Helper;
using System.Windows.Media.Media3D;
using StudyDiffuseShading.Model.Util;
using StudyDiffuseShading.Model.Sampler;

namespace StudyDiffuseShading.Model.Lighting {
    public class Illumination {
        private readonly List<Triangle> lights;
        private Construction primitives;
        private IRandomFactory randomFactory;
        private TriangleSampler triangleSampler;


        public Illumination(Construction primitives, IRandomFactory randomFactory, TriangleSampler triangleSampler) {
            this.lights = new List<Triangle>();
            this.primitives = primitives;
            this.randomFactory = randomFactory;
            this.triangleSampler = triangleSampler;
        }

        public void addLight(Triangle aLight) {
            lights.Add(aLight);
        }

        public Vector3D sampleALight(Vector3D pointIlluminated, Vector3D normalIllumianted) {
            var random = randomFactory.makeRandom();

            var indexLight = (int)(lights.Count * random.NextDouble());
            var triangle = lights[indexLight];
            var sampleResult = triangleSampler.sample(triangle);
            var normalLight = triangle.getNormal(sampleResult.u, sampleResult.v);

            var vecR = pointIlluminated - sampleResult.p;
            var r = vecR.Length;
            var wo = vecR / r;

            return Constant.BLACK;
        }
    }
}
