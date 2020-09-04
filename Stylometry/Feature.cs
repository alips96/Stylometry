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
        public float NounFrequency { get; set; }
        public float VerbFrequency { get; set; }
        public int MostCommonWordCount { get; set; }
        public int SecondMostCommonWordCount { get; set; }
        public int AuthorId { get; set; }

        public List<string> TrainTokens { get; set; }
        public List<string> TestTokens { get; set; }

        public Feature(string _Sentence, int _WordCount, int _AverageLetterCount
            , float _NounFrequency, float _VerbFrequency, int _MostCommonWordCount,
            int _SecondMostCommonWordCount, int _AuthorId)
        {
            Sentence = _Sentence;
            WordCount = _WordCount;
            AverageLetterCount = _AverageLetterCount;
            NounFrequency = _NounFrequency;
            VerbFrequency = _VerbFrequency;
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
            int wordCount, averageLetterCount, mostCommonWordCount, secondMostCommonWordCount;
            mostCommonWordCount = secondMostCommonWordCount = 0;

            float nounFrequency, verbFrequency;

            List<string> wordsList = WordTokenizer.TokenizeWords(sentence);

            wordCount = StructuralAnalyzer.CountWords(wordsList);
            averageLetterCount = StructuralAnalyzer.GetAverageLetterCount(wordsList, wordCount);

            string[] posTags = PosTagger.PosTagTokens(wordsList.ToArray());

            //eliminating stop words of count
            int wordsCountWithoutStopWords = GrammaticalAnalyzer.RemoveStopWords(wordsList);
            nounFrequency = GrammaticalAnalyzer.GetNumberOfNouns(posTags, wordsCountWithoutStopWords);
            verbFrequency = GrammaticalAnalyzer.GetNumberOfVerbs(posTags, wordsCountWithoutStopWords);

            List<string> stemmedWordsList = Stemmer.GetStemmedWordsList(wordsList);
            //List<string> lemmatizedWordsList = Lemmatizer.GetLemmatizedWordsList(stemmedWordsList, posTags);

            return new Feature(sentence, wordCount, averageLetterCount, nounFrequency, verbFrequency, mostCommonWordCount, secondMostCommonWordCount, authorId);
        }
    }
}
