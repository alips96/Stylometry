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
            PrintResults();
        }

        private void PrintResults()
        {
            outputLabel.Text = "Accuracy: " + Evaluator.cm.Accuracy + "\n\n" + "Precision:\n";

            foreach (var item in Evaluator.cm.Precision)
            {
                outputLabel.Text += item + "    ";
            }

            outputLabel.Text += "\n\nRecall:\n";

            foreach (var item in Evaluator.cm.Recall)
            {
                outputLabel.Text += item + "    ";
            }
        }

        private void TestData()
        {
            double[] predicted = StatisticalClassification.TestData(normalizedFeaturesList);
            Evaluator.EvaluateResults(predicted);
        }

        private void NormalizeFeatures()
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
            NormalizeFeatures();
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
