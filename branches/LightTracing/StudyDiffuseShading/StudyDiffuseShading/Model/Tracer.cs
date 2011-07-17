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
        private readonly Func<Triangle, bool> DEFAULT_FILTER;

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

            this.DEFAULT_FILTER = (t) => false;
        }


        public Vector3D traceFirstRay(Ray ray) {
            Triangle target;
            Collision collision;
            Vector3D result;
            if (!findTarget(ray, DEFAULT_FILTER, out target, out collision, out result))
                return result;

            result = target.matter.getLe(collision);
            result += target.matter.shade(collision);
            return result;
        }
        public Vector3D traceRay(Ray ray) {
            return traceRay(ray, DEFAULT_FILTER);
        }
        public Vector3D traceRay(Ray ray, Func<Triangle, bool> filter) {
            Triangle target;
            Collision collision;
            Vector3D result;
            if (!findTarget(ray, filter, out target, out collision, out result))
                return result;

            result = target.matter.shade(collision);
            return result;
        }

        private bool findTarget(Ray ray, Func<Triangle, bool> filter, out Triangle target, out Collision collision, out Vector3D result) {
            double nearest;
            if (!construction.findNearest(ray, double.MaxValue, out nearest, out target, out collision)) {
                 result = ambient.shade(collision);
                 return false;
            }

            result = Constant.BLACK;
            return !filter(target);
        }
    }
}
