using System;

namespace ExternalService
{
    // Thread static random wrapper.
    // Random instance should be create once in case of multiple usage in one-per-request scenario.
    static class ThreadRandom
    {
        [ThreadStatic]
        private static Random random;


        private static Random Random
        {
            get { return random ?? (random = new Random()); }
        }


        public static int Next()
        {
            return Random.Next();
        }

        public static int Next(int max)
        {
            return Random.Next(max);
        }

        public static int Next(int min, int max)
        {
            return Random.Next(min, max);
        }
    }
}