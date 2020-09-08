using System.Collections.Generic;

namespace Stylometry
{
    class GrammaticalAnalyzer
    {
        private static readonly HashSet<string> nounsSet = new HashSet<string>() { "NN", "NNS" };
        private static readonly HashSet<string> verbSet = new HashSet<string>() { "VB", "VBN", "VBZ", "VBD", "VBP" , "VBG"};

        public static readonly HashSet<string> stopWords = new HashSet<string>()
        {
            "i","a","about","an","are","as","at","be","by","for","in","is","it","of","to","the","will","who","there","and"
        };

        internal static int GetNumberOfNouns(string[] posArr)
        {
            int nounCounter = 0;

            foreach (var item in posArr)
            {
                if (nounsSet.Contains(item))
                {
                    nounCounter++;
                }
            }

            return nounCounter;
        }

        internal static int GetNumberOfVerbs(string[] posArr)
        {
            int verbCounter = 0;

            foreach (var item in posArr)
            {
                if (verbSet.Contains(item))
                {
                    verbCounter++;
                }
            }

            return verbCounter;
        }
    }
}
