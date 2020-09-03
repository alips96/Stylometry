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

        public Form1()
        {
            InitializeComponent();
            CreateAuthorsList();
            TokenizeAuthorsText();
            ExtractSentenceFeatures();
        }

        private void ExtractSentenceFeatures()
        {
            sentenceFeaturesList = Feature.ExtractFeatures(sentencetokenizedList);
        }

        private void TokenizeAuthorsText()
        {
            sentencetokenizedList = Tokenizer.TokenizeEveryText(authorsList);
        }

        private void CreateAuthorsList()
        {
            authorsList = ProcessCorpus("Corpus.csv");
            StartButton.Text = authorsList[0].Text;
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
