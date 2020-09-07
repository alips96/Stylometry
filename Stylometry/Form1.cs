using Accord.MachineLearning.VectorMachines;
using Accord.Statistics.Kernels;
using OpenNLP.Tools.Tokenize;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Stylometry
{
    public partial class Form1 : Form
    {
        List<Author> authorsList;
        List<Tokenizer> sentencetokenizedList;
        List<Feature> sentenceFeaturesList;
        List<TrainedData> normalizedFeaturesList;

        public Form1()
        {
            InitializeComponent();
            CreateAuthorsList();
            TokenizeAuthorsText();
            ExtractSentenceFeatures(true); //to train data
            TrainData();
            ExtractSentenceFeatures(false); //to test data
            TestData();
        }

        private void TestData()
        {
            double[] predicted = StatisticalClassification.TestData(normalizedFeaturesList);
            Evaluator.EvaluateResults(predicted);
        }

        private void NormalizerFeatures()
        {
            normalizedFeaturesList = Normalizer.NormalizeFeaturesList(sentenceFeaturesList);
        }

        private void TrainData()
        {
            StatisticalClassification.TrainData(normalizedFeaturesList);
        }

        private void ExtractSentenceFeatures(bool isTrainData)
        {
            sentenceFeaturesList = Feature.ExtractFeatures(sentencetokenizedList, isTrainData);
            NormalizerFeatures();
        }

        private void TokenizeAuthorsText()
        {
            sentencetokenizedList = Tokenizer.TokenizeEveryText(authorsList);
        }

        private void CreateAuthorsList()
        {
            authorsList = ProcessCorpus("Corpus.csv");
        }

        private List<Author> ProcessCorpus(string path)
        {
            return File.ReadAllLines(path).
                Skip(1).
                Where(row => row.Length > 0).
                Select(Author.ParseRow).ToList();
        }
    }
}
