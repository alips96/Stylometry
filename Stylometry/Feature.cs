using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Stylometry
{
    public class Feature
    {
        public string Sentence { get; set; }
        public int WordCount { get; set; }
        public int WordCountWithoutStopWords { get; set; }
        public int StopWordFrequency { get; set; }
        public float NounFrequency { get; set; }
        public float VerbFrequency { get; set; }
        public int MostCommonWordCount { get; set; }
        public int TagsDiversity { get; set; }
        public int AuthorId { get; set; }

        public Feature(string _Sentence, int _WordCount, int _StopWordFrequency, int _AverageLetterCount
            , int _NounFrequency, int _VerbFrequency, int _MostCommonWordCount,
            int _TagsDiversity, int _AuthorId)
        {
            Sentence = _Sentence;
            WordCount = _WordCount;
            WordCountWithoutStopWords = _StopWordFrequency;
            StopWordFrequency = _AverageLetterCount;
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
                    if (sentence.Length > 2) //exeption
                    {
                        featureList.Add(GenerateFeatureInstance(sentence, tokenizer.AuthorId));
                    }
                }
            }

            return featureList;
        }

        private static Feature GenerateFeatureInstance(string sentence, int authorId)
        {
            int wordCount, stopWordFrequency, mostCommonWordFrequency, tagsDiversity, nounFrequency, verbFrequency;

            List<string> wordsList = Tokenizer.SplitWords(sentence);

            //Structural Analsis
            wordCount = StructuralAnalyzer.CountWords(wordsList);
            stopWordFrequency = StructuralAnalyzer.GetStopWordsFrequency(wordsList, wordCount);

            //Grammatical Analysis
            string[] posTags = PosTagger.PosTagTokens(wordsList.ToArray());
            int wordsCountWithoutStopWords = wordsList.Count - stopWordFrequency;
            nounFrequency = GrammaticalAnalyzer.GetNumberOfNouns(posTags);
            verbFrequency = GrammaticalAnalyzer.GetNumberOfVerbs(posTags);

            //Literal Analysis
            List<string> stemmedWordsList = Stemmer.GetStemmedWordsList(wordsList);
            mostCommonWordFrequency = LiteralAnalysis.GetMostCommonWordFrequency(stemmedWordsList);
            tagsDiversity = LiteralAnalysis.GetTagsDiversity(posTags);

            return new Feature(sentence, wordCount, wordsCountWithoutStopWords, stopWordFrequency,
                nounFrequency, verbFrequency, mostCommonWordFrequency, tagsDiversity, authorId);
        }
    }
}
