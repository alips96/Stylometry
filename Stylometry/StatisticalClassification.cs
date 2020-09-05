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

namespace Stylometry
{
    public class StatisticalClassification
    {
        internal static void StartTrainingAndTestData(List<TrainedData> featuresList)
        {
            /*double[][] inputs = GetInputs(featuresList);
            //{
            //    Sparse.FromDense(new double[] { 0, 0 }), // the XOR function takes two booleans
            //    Sparse.FromDense(new double[] { 0, 1 }), // and computes their exclusive or: the
            //    Sparse.FromDense(new double[] { 1, 0 }), // output is true only if the two booleans
            //    Sparse.FromDense(new double[] { 1, 1 })  // are different
            //};

            double[] outputs = GetOutputs(featuresList);
            //{
            //    0, // 0 xor 0 = 0 (inputs are equal)
            //    1, // 0 xor 1 = 1 (inputs are different)
            //    1, // 1 xor 0 = 1 (inputs are different)
            //    0, // 1 xor 1 = 0 (inputs are equal)
            //};

            var learn = new SequentialMinimalOptimizationRegression<Polynomial>()
            {
                Kernel = new Polynomial(2), // Polynomial Kernel of 2nd degree
                Complexity = 5
            };

            SupportVectorMachine<Polynomial> svm = learn.Learn(inputs, outputs);
            //var svm = learn.Learn(inputs, output);
            // Compute the predicted scores
            double[] predicted = svm.Score(inputs);

            // Compute the error between the expected and predicted
            double error = new SquareLoss(outputs).Loss(predicted);

            // Compute the answer for one particular example
            double fxy = svm.Score(inputs[0]); // 1.0003849827673186
            //IMulticlassClassifier<double[], int>
            //Accord.Statistics.Analysis.ConfusionMatrix s = new Accord.Statistics.Analysis.ConfusionMatrix(new int[2,3]);
            //s.pre
            //bool[] prediction = svm.Decide(inputs);*/
            double[][] inputs = // (x, y)
            {
                new double[] { 0,  1 }, // 2*0 + 1 =  1
                new double[] { 4,  3 }, // 2*4 + 3 = 11
                new double[] { 8, -8 }, // 2*8 - 8 =  8
                new double[] { 2,  2 }, // 2*2 + 2 =  6
                new double[] { 6,  1 }, // 2*6 + 1 = 13
                new double[] { 5,  4 }, // 2*5 + 4 = 14
                new double[] { 9,  1 }, // 2*9 + 1 = 19
                new double[] { 1,  6 }, // 2*1 + 6 =  8
            };

            double[] outputs = // f(x, y)
            {
                1, 11, 8, 6, 13, 14, 19, 8
            };

            // Create the sequential minimal optimization teacher
            var learn = new SequentialMinimalOptimizationRegression<Polynomial>()
            {
                Kernel = new Polynomial(2), // Polynomial Kernel of 2nd degree
                Complexity = 100
            };

            // Run the learning algorithm
            SupportVectorMachine<Polynomial> svm = learn.Learn(inputs, outputs);

            // Compute the predicted scores
            double[] predicted = svm.Score(inputs);

            // Compute the error between the expected and predicted
            double error = new SquareLoss(outputs).Loss(predicted);

            // Compute the answer for one particular example
            double fxy = svm.Score(inputs[0]); // 1.0003849827673186

        }

        private static double[][] GetInputs(List<TrainedData> featuresList)
        {
            int count = featuresList.Count;
            double[][] arr = new double[count][];

            for (int i = 0; i < count; i++)
            {
                TrainedData data = featuresList[i];

                arr[i] = new double[]
                {
                         //data.WordFrequency,
                         //data.AverageLetterLength,
                         data.AverageLetterLength / data.WordFrequency,
                         data.NounFrequency,
                         data.VerbFrequency,
                         data.MostCommonWordCount,
                         data.TagsDiversity
                };
            }

            return arr;
        }

        private static double[] GetOutputs(List<TrainedData> featuresList)
        {
            int count = featuresList.Count;
            double[] outputs = new double[count];
            int currentId = 0;
            int index = 1;

            for (int i = 0; i < count; i++)
            {
                if(featuresList[i].AuthorId != currentId)
                {
                    currentId = featuresList[i].AuthorId;
                    index += 10;
                }
                outputs[i] = index;
            }

            return outputs;
        }

        //private static Sparse<double>[] GetInputs(List<TrainedData> featuresList)
        //{
        //    int count = featuresList.Count;
        //    Sparse<double>[] arr = new Sparse<double>[count];

        //    for (int i = 0; i < count; i++)
        //    {
        //        TrainedData data = featuresList[i];

        //        arr[i] = Sparse.FromDense(new double[]
        //        {
        //             data.WordFrequency,
        //             data.AverageLetterLength,
        //             data.NounFrequency,
        //             data.VerbFrequency,
        //             data.MostCommonWordCount,
        //             data.TagsDiversity
        //        });
        //    }

        //    return arr;
        //}
    }
}
