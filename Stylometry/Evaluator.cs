using Accord.Statistics.Analysis;
using System;

namespace Stylometry
{
    public class Evaluator
    {
        public static GeneralConfusionMatrix cm;

        internal static void EvaluateResults(double[] predicted1)
        {
            double[] expected1 = StatisticalClassification.outputs;

            int[] expected = new int[expected1.Length];

            for (int i = 0; i < expected1.Length; i++)
            {
                expected[i] = Convert.ToInt32(expected1[i]);
            }

            int[] predicted = new int[predicted1.Length];

            for (int i = 0; i < predicted1.Length; i++)
            {
                predicted[i] = Convert.ToInt32(predicted1[i]);
            }

            cm = new GeneralConfusionMatrix(expected, predicted);

            int classes = cm.NumberOfClasses;
            int samples = cm.NumberOfSamples;

            // And multiple performance measures:
            double accuracy = cm.Accuracy;
            double[] precision = cm.Precision;
            double[] recall = cm.Recall;
        }
    }
}
