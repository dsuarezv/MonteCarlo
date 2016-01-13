using System;
using System.Collections.Generic;
using System.Collections;

namespace MonteCarlo
{
    public class Distribution: IEnumerable
    {
        private const int DefaultSize = 5000;

        private Random mRandom = new Random();
        private double[] mItems;


        public double this[int index]
        {
            get { return mItems[index]; }
            set { mItems[index] = value; }
        }

        public int Count
        {
            get { return mItems.Length; }
        }

        public double[] RawItems
        {
            get { return mItems; }
        }

        public Distribution(): this(DefaultSize) { }

        public Distribution(int initialSize)
        {
            mItems = new double[initialSize];
        }



        public double GetMean()
        {
            double min, max;
            return GetMean(out min, out max);
        }

        public double GetMean(out double min, out double max)
        {
            return GetMean(out min, out max, mItems);
        }

        public static double GetMean(out double min, out double max, double[] items)
        {
            double result = 0;
            min = double.MaxValue;
            max = double.MinValue;

            for (int i = 0; i < items.Length; ++i)
            {
                var v = items[i];
                result += v;

                if (v < min) min = v;
                if (v > max) max = v;
            }

            return result / items.Length;
        }

        public double GetVariance()
        {
            double result = 0;
            double avg = GetMean();

            for (int i = 0; i < mItems.Length; ++i)
            {
                double v = mItems[i] - avg;
                result += v * v;
            }

            return result / mItems.Length;
        }

        public double GetStdDeviation()
        {
            return Math.Sqrt(GetVariance());
        }

        public double[] GetPercentiles()
        {
            var sorted = (double[])mItems.Clone();
            Array.Sort(sorted);

            int numSteps = 100;
            int step = sorted.Length / numSteps;

            var result = new double[numSteps];

            for (int i = 0; i < numSteps; ++i)
            {
                result[i] = sorted[i * step];
            }

            return result;
        }

        public int[] GetDistribution(int numDivisions)
        {
            return GetDistribution(numDivisions, mItems);
        }

        public static int[] GetDistribution(int numDivisions, double[] items)
        {
            double min, max, mean = GetMean(out min, out max, items);
            var range = max - min;
            var result = new int[numDivisions];

            for (int i = 0; i < items.Length; ++i)
            {
                var val = items[i] - min;
                var idx = (int)(val / range * numDivisions);
                if (idx >= result.Length)
                    idx = result.Length - 1;

                result[idx]++;
            }

            return result;
        }


        public static Distribution Apply(Distribution d1, Func<double, double> op)
        {
            var result = new Distribution(d1.Count);

            for (int i = 0; i < d1.Count; ++i)
            {
                result[i] = op(d1.GetRandomItem());
            }

            return result;
        }

        public static Distribution Apply(Distribution d1, Distribution d2, Func<double, double, double> op)
        {
            var result = new Distribution(d1.Count);

            for (int i = 0; i < d1.Count; ++i)
            {
                result[i] = op(d1.GetRandomItem(), d2.GetRandomItem());
            }

            return result;
        }

        public static Distribution InitGaussian(double mean, double stdDev, int size = DefaultSize)
        {
            var result = new Distribution(size);

            for (int i = 0; i < result.Count; ++i)
            {
                result[i] = result.GetRandomGaussian() * stdDev + mean;
            }

            return result;
        }

        public static Distribution InitGaussianFromRange(double fromValue, double toValue, int size = DefaultSize)
        {
            var d = toValue - fromValue;
            var mean = d / 2 + fromValue;
            var stdDev = d / 2 * 0.9;

            return InitGaussian(mean, stdDev, size);
        }

        public static Distribution InitUniform(double mean, double stdDev)
        {
            throw new NotImplementedException();
        }

        public static Distribution InitConstant(double value, int size = DefaultSize)
        {
            var result = new Distribution(size);

            for (int i = 0; i < DefaultSize; ++i) result[i] = value;

            return result;
        }

        private double GetRandomGaussian()
        {
            return Math.Cos(2 * Math.PI * mRandom.NextDouble()) * Math.Sqrt(-2 * Math.Log(mRandom.NextDouble()));
        }

        internal double GetRandomItem()
        {
            return this[mRandom.Next(Count)];
        }


        // __ IEnumerable ____________________________________________________________


        public IEnumerator GetEnumerator()
        {
            return new RandomDistributionIterator(this);
        }

    }




    public class RandomDistributionIterator: IEnumerator
    {
        private Distribution mTarget;
        private int mCurrent = -1;

        internal RandomDistributionIterator(Distribution target)
        {
            mTarget = target;
        }


        public bool MoveNext()
        {
            ++mCurrent;

            return (mCurrent < mTarget.Count);
        }

        public void Reset()
        {
            mCurrent = -1;
        }
                
        public object Current
        {
            get
            {
                return mTarget.GetRandomItem();
            }
        }

    }
}

