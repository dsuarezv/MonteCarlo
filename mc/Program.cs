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
            var op1 = new ModelFormula("op1") { Formula = "in1 * in2" };

            var model = new Model();
            model.Add(in1, in2, op1);
            model.Solve();

            Console.WriteLine("Result:");
            PrintDistroChart(op1.Output.RawItems, 20, 40);
        }



        private static void PrintDistroChart(double[] items, int numDivisions = 25, int maxColumns = 80)
        {
            var distro = Distribution.GetDistribution(numDivisions, items);

            int max = 0;
            foreach (var i in distro)
                if (i > max)
                    max = i;
            
            foreach (var i in distro)
            {
                int val = (int)((double)i / (double)max * (double)maxColumns);

                for (int j = 0; j < val; ++j)
                    Console.Write('=');

                Console.WriteLine();
            }

        }
    }
}
