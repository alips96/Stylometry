using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.MachineLearning;
using Accord.Math;
using Accord.Statistics.Kernels;
using Accord.Math.Optimization.Losses;
using System.Windows.Forms;
using org.w3c.dom.css;

namespace Stylometry
{
    public class StatisticalClassification
    { 
        public static double[] outputs;
        private static SupportVectorMachine<Polynomial> svm;

        internal static void TrainData(List<TrainedData> featuresList)
        {
            // Generate always same random numbers
            Accord.Math.Random.Generator.Seed = 0;

            double[][] inputs = GetInputs(featuresList);

            outputs = GetOutputs(inputs);


            var learn = new SequentialMinimalOptimizationRegression<Polynomial>()
            {
                Kernel = new Polynomial(2), // Polynomial Kernel of 2nd degree
                Complexity = 100
            };

            double[] output1 = outputs;
            svm = learn.Learn(inputs, outputs); //learn svm.

            double[] predicted = svm.Score(inputs); // Compute the predicted scores

            // Compute the error between the expected and predicted
            double error = new SquareLoss(outputs).Loss(predicted);
        }

        internal static double[] TestData(List<TrainedData> featuresList)
        {
            double[][] inputs = GetInputs(featuresList);
            double[] predicted = svm.Score(inputs);
            //double[] output1 = outputs;
            double error = new SquareLoss(outputs).Loss(predicted);

            return predicted;
        }

        private static double[][] GetInputs(List<TrainedData> featuresList)
        {
            List<double[]> inputList = new List<double[]>();

            float avgLetterLength = 0f;
            float nFrequency = 0f;
            float vFrequency = 0f;
            float mostCount = 0f;
            float tDiversity = 0f;

            int indexCounter = 0;

            int savedAuthorId = featuresList[0].AuthorId;

            foreach (var item in featuresList)
            {
                avgLetterLength += item.AverageLetterLength;
                nFrequency += item.NounFrequency;
                vFrequency += item.VerbFrequency;
                mostCount += item.MostCommonWordCount;
                tDiversity += item.TagsDiversity;
                indexCounter++;

                if(item.AuthorId != savedAuthorId)
                {
                    savedAuthorId = item.AuthorId;
                    inputList.Add(new double[] 
                    { 
                        avgLetterLength / indexCounter
                        , nFrequency / indexCounter
                        , vFrequency / indexCounter
                        , mostCount / indexCounter
                        , tDiversity / indexCounter
                    });

                    avgLetterLength = nFrequency = vFrequency = mostCount = tDiversity = 0f;
                    indexCounter = 0;
                }
            }

            inputList.Add(new double[]
            {
                avgLetterLength / indexCounter
                , nFrequency / indexCounter
                , vFrequency / indexCounter
                , mostCount / indexCounter
                , tDiversity / indexCounter
            });

            return inputList.ToArray();
        }

        private static double[] GetOutputs(double[][] inputs)
        {
            double[] outputs = new double[inputs.Length];

            for (int i = 0; i < inputs.Length; i++)
            {
                double fx = AddFormula(inputs[i]);
                outputs[i] = fx;
            }

            return outputs;
        }

        private static double AddFormula(double[] vs)
        {
            double i = 0;

            foreach (var item in vs)
            {
                i += item;
            }

            return i++;
        }
    }
}
