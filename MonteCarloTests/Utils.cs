using System;

namespace MonteCarloTests
{
    public static class Utils
    {
        public static void AssertIsInRange(double target, double val, double range)
        {
            double r = Math.Abs(val - target);
            if (r < range)
                return;

            throw new ArgumentOutOfRangeException("target");
        }
    }
}

