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
            //var xr = Distribution.InitGaussian(10, 0.5);
            var xr = Distribution.InitConstant(10);

            Console.WriteLine("Input:");
            PrintDistroChart(xr.RawItems, 20, 40);

            var e = new Expression("xr * const", EvaluateOptions.IterateParameters | EvaluateOptions.IgnoreCase);
            e.Parameters["xr"] = xr;
            e.Parameters["const"] = 5;

            var result = (List<object>)e.Evaluate();

            Console.WriteLine("Result:");
            PrintDistroChart(ModelFormula.GetDoubleArray(result), 20, 40);
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
