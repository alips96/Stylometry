using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Math;
using Accord.Statistics.Kernels;

namespace Stylometry
{
    public class StatisticalClassification
    {
        internal static void StartTrainingAndTestData(List<TrainedData> featuresList)
        {
            Sparse<double>[] inputs =
            {
                Sparse.FromDense(new double[] { 0, 0 }), // the XOR function takes two booleans
                Sparse.FromDense(new double[] { 0, 1 }), // and computes their exclusive or: the
                Sparse.FromDense(new double[] { 1, 0 }), // output is true only if the two booleans
                Sparse.FromDense(new double[] { 1, 1 })  // are different
            };

            int[] xor = // this is the output of the xor function
            {
                0, // 0 xor 0 = 0 (inputs are equal)
                1, // 0 xor 1 = 1 (inputs are different)
                1, // 1 xor 0 = 1 (inputs are different)
                0, // 1 xor 1 = 0 (inputs are equal)
            };

            var learn = new SequentialMinimalOptimization<Gaussian, Sparse<double>>()
            {
                UseComplexityHeuristic = true,
                UseKernelEstimation = true
            };

            var svm = learn.Learn(inputs, xor);
            //Accord.Statistics.Analysis.ConfusionMatrix s = new Accord.Statistics.Analysis.ConfusionMatrix(new int[2,3]);
            //s.pre
            bool[] prediction = svm.Decide(inputs);
            
        }
    }
}
