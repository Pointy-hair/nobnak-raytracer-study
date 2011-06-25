﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;

namespace StudyDiffuseShading.Model.Sampler {
    public class SimpleHemispherecalSampler : IHemispherecalSampler {
        private Random randomer;


        public SimpleHemispherecalSampler(int seed) {
            this.randomer = new Random(seed);
        }

        
        public Vector3D sample() {
            double rand1, rand2; 

            rand1 = randomer.NextDouble();
            rand2 = randomer.NextDouble();

            double cosPhi = Math.Cos(2.0 * Math.PI * rand1);
            double sinPhi = Math.Sin(2.0 * Math.PI * rand1);

            double cosTheta2 = 1.0 - rand2;
            double cosTheta = Math.Sqrt(cosTheta2);
            double sinTheta = Math.Sqrt(1.0 - cosTheta2);

            var u = sinTheta * cosPhi;
            var v = sinTheta * sinPhi;
            var w = cosTheta;

            return new Vector3D(u, v, w);
        }
    }
}
