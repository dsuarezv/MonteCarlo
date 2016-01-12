using System;
using NUnit.Framework;
using MonteCarlo;

namespace MonteCarloTests
{
    public class MonteCarloTests
    {
        [Test]
        public void MonteCarloBasic()
        {
            var input1 = Distribution.InitGaussian(1.5, 0.31);
            var input2 = Distribution.InitGaussian(8.5, 0.31);

            var result = Distribution.Apply(input1, input2, (x, y) => x * y);

            double min, max, mean = result.GetMean(out min, out max);
            double stdDev = result.GetStdDeviation();

            Utils.AssertIsInRange(stdDev, 2.69, 0.2);
            Utils.AssertIsInRange(mean, 12.73, 0.2);
        }
    }
}

