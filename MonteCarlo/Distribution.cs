using System;
using System.Collections.Generic;

namespace MonteCarlo
{
    public class Distribution
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


        public Distribution(): this(DefaultSize)
        {
            
        }

        public Distribution(int initialSize)
        {
            mItems = new double[initialSize];
        }


        public double GetMean()
        {
            throw new NotImplementedException();
        }

        public double GetVariance()
        {
            throw new NotImplementedException();
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

        public static Distribution InitGaussian(double mean, double variance)
        {
            throw new NotImplementedException();
        }

        public static Distribution InitGaussian(double fromValue, double toValue)
        {
            throw new NotImplementedException();
        }

        public static Distribution InitUniform(double mean, double variance)
        {
            throw new NotImplementedException();
        }

        public static Distribution InitConstant(double value)
        {
            var result = new Distribution(DefaultSize);

            for (int i = 0; i < DefaultSize; ++i) result[i] = value;

            return result;
        }

        private double GetRandomItem()
        {
            return this[mRandom.Next(Count)];
        }
    }
}

