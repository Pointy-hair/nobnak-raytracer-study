using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace StudyDiffuseShading.Model.Sampler {
    public class JitteredSampler {
        private int n;
        private int count;
        private Random random;


        public JitteredSampler(int n) {
            this.n = n;
            this.count = n * n;
            this.random = new Random();
        }


        public int Count { get { return count; }}


        public IEnumerable<Vector> getSampler() {
            var invN = 1.0 / n;

            for (int col = 0; col < n; col++)
                for (int row = 0; row < n; row++)
                    yield return new Vector((col + random.NextDouble()) * invN, (row + random.NextDouble()) * invN);
        }
    }
}
