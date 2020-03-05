using System;

namespace sampling_models
{
    class _Math
    {
        //Converts string array to double array:
        public double[] toDoubleArray(string[] rawValues)
        {
            double[] values = new double[rawValues.Length];
            int i = 0;

            while (i < rawValues.Length)
            {
                values[i] = Convert.ToDouble(rawValues[i]);
                i++;
            }

            return values;
        }

        //Calculates a mean from a floating array of values:
        public double mean(double[] values)
        {
            int i = 0;
            double total = 0;

            while (i < values.Length)
            {
                total = total + values[i];
                i++;
            }

            return total / values.Length;
        }

        //Calculates a standard deviation from a floating array of values:
        public double sDev(double[] values)
        {
            double _mean = mean(values);
            double[] sqrDifferences = new double[values.Length];
            int i = 0;

            while (i < values.Length)
            {
                double difference = values[i] - _mean;
                sqrDifferences[i] = difference * difference;
                i++;
            }

            return Math.Sqrt(mean(sqrDifferences));
        }

        //Returns a random sample of double arrays:
        public double[] getRandomSample(int size, string[] rawValues)
        {
            Random rnd = new Random();
            int i = 0;
            double[] sample = new double[size];

            while (i < size)
            {
                sample[i] = Convert.ToDouble(rnd.Next(rawValues.Length + 1));
                i++;
            }

            return sample;
        }
    }
}