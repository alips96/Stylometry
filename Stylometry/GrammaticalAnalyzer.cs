using OpenNLP.Tools.Trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stylometry
{
    class GrammaticalAnalyzer
    {
        private static readonly HashSet<string> nounsSet = new HashSet<string>() { "NN", "NNS" };
        private static readonly HashSet<string> verbSet = new HashSet<string>() { "VB", "VBN", "VBZ" };

        private static readonly HashSet<string> stopWords = new HashSet<string>()
        {
            "i","a","about","an","are","as","at","be","by","for","in","is","it","of","to","the","will","who","there","and"
        };

        internal static float GetNumberOfNouns(string[] posArr, int wordCount)
        {
            int nounCounter = 0;

            foreach (var item in posArr)
            {
                if (nounsSet.Contains(item))
                {
                    nounCounter++;
                }
            }

            return NormalizeQuantity(nounCounter,wordCount);
        }

        private static float NormalizeQuantity(int nounCounter, int wordCount)
        {
            //float value = (float)nounCounter / wordCount;
            return (float) nounCounter / wordCount;
        }

        internal static float GetNumberOfVerbs(string[] posArr, int wordCount)
        {
            int verbCounter = 0;

            foreach (var item in posArr)
            {
                if (verbSet.Contains(item))
                {
                    verbCounter++;
                }
            }

            return NormalizeQuantity(verbCounter, wordCount);
        }

        internal static int RemoveStopWords(List<string> wordsList)
        {
            int counter = 0;

            foreach (var item in wordsList)
            {
                if (!stopWords.Contains(item))
                {
                    counter++;
                }
            }

            return counter;
        }
    }
}
