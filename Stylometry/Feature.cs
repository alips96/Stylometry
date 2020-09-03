using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stylometry
{
    public class Feature
    {
        public string Sentence { get; set; }
        public int WordCount { get; set; }
        public int AverageLetterCount { get; set; }
        public int NounCount { get; set; }
        public int VerbCount { get; set; }
        public int MostCommonWordCount { get; set; }
        public int SecondMostCommonWordCount { get; set; }
        public int AuthorId { get; set; }

        public List<string> TrainTokens { get; set; }
        public List<string> TestTokens { get; set; }

        public Feature(string _Sentence, int _WordCound, int _AverageLetterCount
            , int _NounCount, int _VerbCount, int _MostCommonWordCount,
            int _SecondMostCommonWordCount, int _AuthorId)
        {
            Sentence = _Sentence;
            WordCount = _WordCound;
            AverageLetterCount = _AverageLetterCount;
            NounCount = _NounCount;
            VerbCount = _VerbCount;
            MostCommonWordCount = _MostCommonWordCount;
            SecondMostCommonWordCount = _SecondMostCommonWordCount;
            AuthorId = _AuthorId;
        }

        internal static List<Feature> ExtractFeatures(List<Tokenizer> sentenceList)
        {
            List<Feature> featureList = new List<Feature>();

            foreach (Tokenizer tokenizer in sentenceList)
            {
                foreach (string sentence in tokenizer.TrainTokens)
                {
                    if (sentence != "..") //exeption
                    {
                        featureList.Add(GenerateFeatureInstance(sentence, tokenizer.AuthorId));
                    }
                }
            }

            return featureList;
        }

        private static Feature GenerateFeatureInstance(string sentence, int authorId)
        {
            int wordCount, averageLetterCount, nounCount, verbCount, mostCommonWordCount, secondMostCommonWordCount;
            wordCount = averageLetterCount = nounCount = verbCount = mostCommonWordCount = secondMostCommonWordCount = 0;

            List<string> wordsList = WordTokenizer.TokenizeWords(sentence);

            wordCount = StructuralAnalyzer.CountWords(wordsList);
            averageLetterCount = StructuralAnalyzer.GetAverageLetterCount(wordsList, wordCount);



            return new Feature(sentence, wordCount, averageLetterCount, nounCount, verbCount, mostCommonWordCount, secondMostCommonWordCount, authorId);
        }
    }
}
