﻿using System;
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
                if(item.WordCountWithoutStopWords != 0)
                {
                    normalizedList.Add
                    (
                        new TrainedData
                        (
                        item.WordCount,
                        (float)item.StopWordFrequency / item.WordCount,
                        (float)item.NounFrequency / item.WordCountWithoutStopWords,
                        (float)item.VerbFrequency / item.WordCountWithoutStopWords,
                        (float)item.MostCommonWordCount / item.WordCount,
                        (float)item.TagsDiversity / item.WordCount,
                        item.AuthorId)
                    );
                }
            }

            return normalizedList;
        }
    }
}
