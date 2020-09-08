using System.Collections.Generic;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Statistics.Kernels;
using Accord.Math.Optimization.Losses;

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

            double[] predicted = svm.Score(inputs);
        }

        internal static double[] TestData(List<TrainedData> featuresList)
        {
            double[][] inputs = GetInputs(featuresList);
            double[] predicted = svm.Score(inputs);
            double error = new SquareLoss(outputs).Loss(predicted);

            return predicted;
        }

        private static double[][] GetInputs(List<TrainedData> featuresList)
        {
            List<double[]> inputList = new List<double[]>();

            float stpWordsFreq = 0f;
            float nFrequency = 0f;
            float vFrequency = 0f;
            float mostCount = 0f;
            float tDiversity = 0f;

            int indexCounter = 0;

            int savedAuthorId = featuresList[0].AuthorId;

            foreach (var item in featuresList)
            {
                stpWordsFreq += item.StopWordsFrequency;
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
                        stpWordsFreq / indexCounter
                        , nFrequency / indexCounter
                        , vFrequency / indexCounter
                        , mostCount / indexCounter
                        , tDiversity / indexCounter
                    });

                    stpWordsFreq = nFrequency = vFrequency = mostCount = tDiversity = 0f;
                    indexCounter = 0;
                }
            }

            inputList.Add(new double[]
            {
                stpWordsFreq / indexCounter
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
                i += item * 10;
            }

            return i++;
        }
    }
}
