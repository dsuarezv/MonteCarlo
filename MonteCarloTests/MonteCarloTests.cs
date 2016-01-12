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
            // This is the basic sample of using the library to solve
            // a Monte Carlo simulation. It has two inputs, one ranged from 1 to 2, 
            // and another from 8 to 9. The output of the process is a multiplication
            // of the two inputs.

            var input1 = Distribution.InitGaussian(1.5, 0.31);  // 1.5 +-0.5  -->  [1, 2]
            var input2 = Distribution.InitGaussian(8.5, 0.31);  // 8.5 +.0.5  -->  [8, 9]

            var result = Distribution.Apply(input1, input2, (x, y) => x * y);

            double min, max, mean = result.GetMean(out min, out max);
            double stdDev = result.GetStdDeviation();

            Utils.AssertIsInRange(stdDev, 2.69, 0.2);
            Utils.AssertIsInRange(mean, 12.73, 0.2);
        }
    }
}

