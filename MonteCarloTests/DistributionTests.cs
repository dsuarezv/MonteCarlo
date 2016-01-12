using NUnit.Framework;
using System;
using MonteCarlo;

namespace MonteCarloTests
{
    [TestFixture()]
    public class DistributionTests
    {
        [Test()]
        public void DistBasic()
        {
            var d1 = Distribution.InitConstant(4);
            var d2 = Distribution.InitConstant(6);

            var res = Distribution.Apply(d1, d2, (x, y) => x * y);

            for (int i = 0; i < d1.Count; ++i)
                Assert.AreEqual(res[i], 24);
        }
    }
}

