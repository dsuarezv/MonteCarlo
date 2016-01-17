using System;
using MonteCarlo;
using NCalc;
using System.Collections.Generic;

namespace mc
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var in1 = new ModelInput("in1") { Distribution = Distribution.InitGaussian(10, 0.31) };
            var in2 = new ModelInput("in2") { Distribution = Distribution.InitGaussian(3, 0.31) };
            var in3 = new ModelInput("in3") { Distribution = Distribution.InitGaussian(5, 0.61) };
            var in4 = new ModelInput("c") { Distribution = Distribution.InitConstant(10) };
            var op1 = new ModelFormula("op1") { Formula = "in1 * in2" };
            var op2 = new ModelFormula("op2") { Formula = "op1 * in3 * c" };

            var model = new Model();
            model.Add(in1, in2, op1, in3, in4, op2);
            model.Solve();


            Console.WriteLine("Result: mean: {0} stddev: {1}", op2.Output.GetMean(), op2.Output.GetStdDeviation());
            Console.WriteLine("Result histogram:");
            PrintDistroChart(op2.Output.RawItems);
        }



        private static void PrintDistroChart(double[] items, int numDivisions = 25, int maxColumns = 80)
        {
            var distro = Distribution.GetHistogram(numDivisions, items);

            foreach (var i in distro)
            {
                int val = (int)(i * maxColumns);

                for (int j = 0; j < val; ++j)
                    Console.Write('=');

                Console.WriteLine();
            }

        }
    }
}
