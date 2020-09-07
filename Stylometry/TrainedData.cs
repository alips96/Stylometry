using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stylometry
{
    public class TrainedData
    {
        public int WordFrequency { get; set; }
        public float StopWordsFrequency { get; set; }
        public float NounFrequency { get; set; }
        public float VerbFrequency { get; set; }
        public float MostCommonWordCount { get; set; }
        public float TagsDiversity { get; set; }
        public int AuthorId { get; set; }

        public TrainedData(int wordFrequency, float stopWordsFrequency,
            float nounFrequency, float verbFrequency, float mostCommonWordCount, float tagsDiversity, int authorId)
        {
            WordFrequency = wordFrequency;
            StopWordsFrequency = stopWordsFrequency;
            NounFrequency = nounFrequency;
            VerbFrequency = verbFrequency;
            MostCommonWordCount = mostCommonWordCount;
            TagsDiversity = tagsDiversity;
            AuthorId = authorId;
        }

    }
}
