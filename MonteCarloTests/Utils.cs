using System;
using NUnit.Framework;

namespace MonteCarloTests
{
    public static class Utils
    {
        public static void AssertIsInRange(double target, double val, double range)
        {
            double r = Math.Abs(val - target);
            Assert.IsTrue(r <= range);
        }
    }
}

