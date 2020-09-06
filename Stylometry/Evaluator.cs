using Accord.Math;
using Accord.Statistics.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stylometry
{
    public class Evaluator
    {
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
            //int[] expected = { 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2 };

            ////And we have a set of values that have been predicted by a machine model:

            //int[] predicted = { 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 };
            // We can get different performance measures to assess how good our model was at 
            // predicting the true, expected, ground-truth labels for the decision problem:
            //var cm = new GeneralConfusionMatrix(classes: 4, expected: expected, predicted: predicted);
            var cm = new GeneralConfusionMatrix(expected, predicted);

            

            // We can get more information about our problem as well:
            int classes = cm.NumberOfClasses; // should be 3
            int samples = cm.NumberOfSamples; // should be 12

            // And multiple performance measures:
            double accuracy = cm.Accuracy;                      // should be 0.66666666666666663
            double error = cm.Error;                            // should be 0.33333333333333337
            double chanceAgreement = cm.ChanceAgreement;        // should be 0.33333333333333331
            double geommetricAgreement = cm.GeometricAgreement; // should be 0 (the classifier completely missed one class)
            double pearson = cm.Pearson;                        // should be 0.70710678118654757
            double kappa = cm.Kappa;                            // should be 0.49999999999999994
            double tau = cm.Tau;                                // should be 0.49999999999999994
            double chiSquare = cm.ChiSquare;                    // should be 12

            // and some of their standard errors:
            double kappaStdErr = cm.StandardError;              // should be 0.15590239111558091
            double kappaStdErr0 = cm.StandardErrorUnderNull;    // should be 0.16666666666666663
        }
    }
}
