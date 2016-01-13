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
            var xr = Distribution.InitGaussian(10, 0.5);

            Console.WriteLine("Input:");
            PrintDistroChart(xr.RawItems, 20, 40);

            var e = new Expression("xr * 5", EvaluateOptions.IterateParameters | EvaluateOptions.IgnoreCase);
            e.Parameters["xr"] = xr;

            var result = (List<object>)e.Evaluate();

            Console.WriteLine("Result:");
            PrintDistroChart(GetDoubleArray(result), 20, 40);
        }


        private static double[] GetDoubleArray(List<object> objectList)
        {
            double[] result = new double[objectList.Count];

            for (int i = 0; i < objectList.Count; ++i) 
                result[i] = (double)objectList[i];

            return result;
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
