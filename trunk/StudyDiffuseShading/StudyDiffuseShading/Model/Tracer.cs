using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using StudyDiffuseShading.Model.Material;
using StudyDiffuseShading.Model.Primitive;
using StudyDiffuseShading.Model.Util;
using StudyDiffuseShading.Model.Lighting;
using StudyDiffuseShading.Model.Sampler;
using StudyDiffuseShading.Model.Helper;

namespace StudyDiffuseShading.Model {
    public class Tracer {
        private Construction construction;
        private ILight environment;
        private IRandomFactory randomFactory;
        private IHemispherecalSampler sampler;

        public Tracer(Construction construction, ILight environment, IRandomFactory randomFactory, 
            IHemispherecalSamplerFactory samplerFactory) 
        {
            this.construction = construction;
            this.environment = environment;
            this.randomFactory = randomFactory;
            this.sampler = samplerFactory.makeSampler();
        }


        public Vector3D traceRay(Ray ray) {
            double nearest;
            Triangle target;
            Collision collision;
            if (!construction.findNearest(ray, double.MaxValue, out nearest, out target, out collision))
                return environment.l();

            var random = randomFactory.makeRandom();
            if (random.NextDouble() > target.matter.rho())
                return Constant.BLACK;

            Vector3D result = target.matter.shadeDividedRho(this, randomFactory, sampler, collision);
            return result;
        }
    }
}
