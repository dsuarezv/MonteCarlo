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
            var in1 = new ModelInput("in1") { Distribution = Distribution.InitGaussian(10, 0.31) };
            var in2 = new ModelInput("in2") { Distribution = Distribution.InitGaussian(3, 0.31) };
            var op1 = new ModelFormula("op1") { Formula = "in1 * in2" };

            var model = new Model();
            model.Add(in1, in2, op1);
            model.Solve();
                
            Utils.AssertIsInRange(op1.Output.GetMean(), 30, 0.5);
        }

        [Test]
        public void ConstantTest()
        {
            var in1 = new ModelInput("in1") { Distribution = Distribution.InitGaussian(10, 0.31) };
            var op1 = new ModelFormula("op1") { Formula = "in1 * 3" };

            var model = new Model();
            model.Add(in1, op1);
            model.Solve();

            Utils.AssertIsInRange(op1.Output.GetMean(), 30, 0.5);
        }
    }
}

