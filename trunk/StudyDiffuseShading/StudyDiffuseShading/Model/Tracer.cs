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
        private IMaterial ambient;
        private IRandomFactory randomFactory;
        private IHemispherecalSampler sampler;
        private double maxRadiance;

        public Tracer(Construction construction, IMaterial ambient, IRandomFactory randomFactory, 
            IHemispherecalSamplerFactory samplerFactory)
            : this(construction, ambient, randomFactory, samplerFactory, 1.0) {
        }
        public Tracer(Construction construction, IMaterial ambient, IRandomFactory randomFactory,
            IHemispherecalSamplerFactory samplerFactory, double maxRadiance) {
            this.construction = construction;
            this.ambient = ambient;
            this.randomFactory = randomFactory;
            this.sampler = samplerFactory.makeSampler();
            this.maxRadiance = maxRadiance;
        }
        


        public Vector3D traceRay(Ray ray) {
            double nearest;
            Triangle target;
            Collision collision;
            if (!construction.findNearest(ray, double.MaxValue, out nearest, out target, out collision))
                return ambient.shade(collision);

            Vector3D result = target.matter.shade(collision);
            return result;
        }
    }
}
