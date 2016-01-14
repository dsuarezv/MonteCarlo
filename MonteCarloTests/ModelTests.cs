using System;
using NUnit.Framework;
using MonteCarlo;

namespace MonteCarloTests
{
    public class ModelTests
    {
        [Test]
        public void Basic()
        {
            var in1 = new ModelInput() { Name = "in1", Distribution = Distribution.InitGaussian(10, 0.31) };
            var in2 = new ModelInput() { Name = "in2", Distribution = Distribution.InitGaussian(3, 0.31) };
            var op1 = new ModelFormula() { Name = "op1", Formula = "in1 * in2" };

            var model = new Model();
            model.Add(in1, in2, op1);
            model.Solve();
                
            Utils.AssertIsInRange(op1.Output.GetMean(), 30, 0.5);
        }
    }
}

