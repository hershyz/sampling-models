using System;
using System.IO;
using System.Threading;

namespace sampling_models
{
    class Program
    {
        public static _Math math = new _Math();

        static void Main(string[] args)
        {
            Console.Title = "sampling-models by Hershyz";
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Data File Directory: ");
            string dataDirectory = Console.ReadLine();
            Console.WriteLine(' ');

            string[] rawValues = File.ReadAllLines(dataDirectory);
            double[] values = math.toDoubleArray(rawValues);
            double populationMean = math.mean(values);
            double populationSDev = math.sDev(values);
            double populationVariance = Math.Pow(populationSDev, 2);

            if (rawValues.Length < 20)
            {
                printWithSubtitle("Error", "Sample size too small.");
                return;
            }

            //Starts sampling the dataset:
            int length = rawValues.Length;
            int samples = length * 10; //Multiplies the length of the dataset to calculate number of samples.
            int maxSampleSize = length / 10; //Ensures that each sample is not more than 10 percent of the population.
            int minSampleSize = length / 20; //Ensures that each sample is not less than 5 percent of the population.
            int i = 0;
            Random rnd = new Random();

            //Average values for all measures:
            double[] averageMean = new double[samples];
            double[] averageSDev = new double[samples];
            double[] averageVariance = new double[samples];

            while (i < samples)
            {
                int currentSampleSize = rnd.Next(minSampleSize, maxSampleSize + 1); //Creates a random sample size between 5 and 10 percent of the population:
                double[] sample = math.getRandomSample(currentSampleSize, rawValues);

                double mean = math.mean(sample);
                double sDev = math.sDev(sample);
                double variance = Math.Pow(sDev, 2);

                averageMean[i] = mean;
                averageSDev[i] = sDev;
                averageVariance[i] = variance;

                printSampleSubtitle(i + 1, sample);
                Console.Write(" - ");
                printWithSubtitle("Mean", mean.ToString());
                Console.Write(" - ");
                printWithSubtitle("Standard Deviation", sDev.ToString());
                Console.Write(" - ");
                printWithSubtitle("Variance", variance.ToString());
                Console.WriteLine(' ');

                i++;
                Thread.Sleep(1);
            }

            //Prints population statistics:
            printWithSubtitle("Population Mean", populationMean.ToString());
            printWithSubtitle("Population Standard Deviation", populationSDev.ToString());
            printWithSubtitle("Population Variance", populationVariance.ToString());

            //Calculates the average measures of all samples taken:
            printWithSubtitle("Average Sample Mean", math.mean(averageMean).ToString());
            printWithSubtitle("Average Sample Standard Deviation", math.mean(averageSDev).ToString());
            printWithSubtitle("Average Sample Variance", math.mean(averageVariance).ToString());
        }

        private static void printWithSubtitle(string subtitle, string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(subtitle + ": ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(text);
        }

        private static void printSampleSubtitle(int sampleNumber, double[] values)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Sample ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(sampleNumber);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(": ");

            int i = 0;
            while (i < values.Length)
            {
                Console.Write(values[i]);

                if (i != values.Length - 1)
                {
                    Console.Write(", ");
                }

                i++;
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(' ');
        }
    }
}