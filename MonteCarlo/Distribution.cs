﻿using System;
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


        public Distribution(): this(DefaultSize) { }

        public Distribution(int initialSize)
        {
            mItems = new double[initialSize];
        }


        public double GetMean()
        {
            double result = 0;

            for (int i = 0; i < mItems.Length; ++i)
            {
                result += mItems[i];
            }

            return result / mItems.Length;
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
                result[i] = result.GetRandomNormal() * stdDev + mean;
            }

            return result;
        }

        public static Distribution InitGaussianRange(double fromValue, double toValue, int size = DefaultSize)
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

        private double GetRandomNormal()
        {
            return Math.Cos(2 * Math.PI * mRandom.NextDouble()) * Math.Sqrt(-2 * Math.Log(mRandom.NextDouble()));
        }

        private double GetRandomItem()
        {
            return this[mRandom.Next(Count)];
        }
    }
}

