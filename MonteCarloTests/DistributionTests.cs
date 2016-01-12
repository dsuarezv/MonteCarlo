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

        [Test]
        public void DistInitGaussian()
        {
            var d1 = Distribution.InitGaussian(10, 0.5);

            var mean = d1.GetMean();
            var stdDev = d1.GetStdDeviation();

            Utils.AssertIsInRange(mean, 10, 0.05);
            Utils.AssertIsInRange(stdDev, 0.5, 0.05);
        }

        [Test]
        public void DistPercentiles()
        {
            var d1 = Distribution.InitGaussian(10, 0.5);

            var percentiles = d1.GetPercentiles();

            var fivePercent = percentiles[4];
            var ninetyFivePercent = percentiles[94];
            var diff = ninetyFivePercent - fivePercent;
            var range = diff / 2;



            return;
        }

        [Test]
        public void DistDistribution()
        {
            var d1 = Distribution.InitGaussian(10, 0.5);
            var dist = d1.GetDistribution(50);

            return;
        }
    }
}

