using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stylometry
{
    public class Normalizer
    {
        internal static List<TrainedData> NormalizeFeaturesList(List<Feature> sentenceFeaturesList)
        {
            List<TrainedData> normalizedList = new List<TrainedData>();

            foreach (Feature item in sentenceFeaturesList)
            {
                normalizedList.Add
                    (
                    new TrainedData(
                    item.WordCount,
                    (float)item.AverageLetterCount / item.WordCountWithoutStopWords,
                    (float)item.NounFrequency / item.WordCountWithoutStopWords,
                    (float)item.VerbFrequency / item.WordCountWithoutStopWords,
                    (float)item.MostCommonWordCount / item.WordCount,
                    (float)item.TagsDiversity / item.WordCount,
                    item.AuthorId)
                    );
                //normalizedList.Add
                //(
                //new TrainedData(
                //item.WordCount,
                //(float)item.AverageLetterCount,
                //(float)item.NounFrequency,
                //(float)item.VerbFrequency,
                //(float)item.MostCommonWordCount,
                //(float)item.TagsDiversity,
                //item.AuthorId)
                //);
            }

            return normalizedList;
        }
    }
}
