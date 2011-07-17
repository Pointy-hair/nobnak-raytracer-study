using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudyDiffuseShading.Model.Util {
    public static class RandomUtil {
        public static readonly Random seeder = new Random();


        public static Random makeRandom() {
            int seed;
            lock (seeder) {
                seed = seeder.Next();
            }
            return new Random(seed);
        }
    }
}
