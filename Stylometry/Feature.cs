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
        public int WordCountWithoutStopWords { get; set; }
        public int AverageLetterCount { get; set; }
        public float NounFrequency { get; set; }
        public float VerbFrequency { get; set; }
        public int MostCommonWordCount { get; set; }
        public int TagsDiversity { get; set; }
        public int AuthorId { get; set; }

        public Feature(string _Sentence, int _WordCount, int _WordCountWithoutStopWords, int _AverageLetterCount
            , int _NounFrequency, int _VerbFrequency, int _MostCommonWordCount,
            int _TagsDiversity, int _AuthorId)
        {
            Sentence = _Sentence;
            WordCount = _WordCount;
            WordCountWithoutStopWords = _WordCountWithoutStopWords;
            AverageLetterCount = _AverageLetterCount;
            NounFrequency = _NounFrequency;
            VerbFrequency = _VerbFrequency;
            MostCommonWordCount = _MostCommonWordCount;
            TagsDiversity = _TagsDiversity;
            AuthorId = _AuthorId;
        }

        internal static List<Feature> ExtractFeatures(List<Tokenizer> sentenceList, bool isTrainTokens)
        {
            List<Feature> featureList = new List<Feature>();

            foreach (Tokenizer tokenizer in sentenceList)
            {
                List<string> execuitiveList = isTrainTokens ? tokenizer.TrainTokens : tokenizer.TestTokens; //specifying whether it is the train data or test data.

                foreach (string sentence in execuitiveList)
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
            int wordCount, averageLetterCount, mostCommonWordFrequency, tagsDiversity, nounFrequency, verbFrequency;

            List<string> wordsList = WordTokenizer.TokenizeWords(sentence);

            wordCount = StructuralAnalyzer.CountWords(wordsList);
            averageLetterCount = StructuralAnalyzer.GetAverageLetterCount(wordsList, wordCount);

            string[] posTags = PosTagger.PosTagTokens(wordsList.ToArray());

            //eliminating stop words of count
            int wordsCountWithoutStopWords = GrammaticalAnalyzer.RemoveStopWords(wordsList);
            nounFrequency = GrammaticalAnalyzer.GetNumberOfNouns(posTags);
            verbFrequency = GrammaticalAnalyzer.GetNumberOfVerbs(posTags);

            List<string> stemmedWordsList = Stemmer.GetStemmedWordsList(wordsList);

            mostCommonWordFrequency = LiteralAnalysis.GetMostCommonWordFrequency(stemmedWordsList);
            tagsDiversity = LiteralAnalysis.GetTagsDiversity(posTags);

            return new Feature(sentence, wordCount, wordsCountWithoutStopWords, averageLetterCount,
                nounFrequency, verbFrequency, mostCommonWordFrequency, tagsDiversity, authorId);
        }
    }
}
