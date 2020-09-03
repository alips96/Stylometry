using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stylometry
{
    public class Feature
    {
        public int Sentence { get; set; }
        public int WordCount { get; set; }
        public int AverageLetterCount { get; set; }
        public int NounCount { get; set; }
        public int VerbCount { get; set; }
        public int MostCommonWordCount { get; set; }
        public int SecondMostCommonWordCount { get; set; }
        public int AuthorId { get; set; }

        public List<string> TrainTokens { get; set; }
        public List<string> TestTokens { get; set; }

        public Feature(int _Sentence, int _WordCound, int _AverageLetterCount
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
    }
}
