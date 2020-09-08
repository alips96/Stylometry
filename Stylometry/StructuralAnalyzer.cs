using System.Collections.Generic;

namespace Stylometry
{
    public class StructuralAnalyzer
    {
        internal static int CountWords(List<string> wordsList)
        {
            return wordsList.Count;
        }

        internal static int GetStopWordsFrequency(List<string> wordsList, int wordCount)
        {
            int counter = 0;

            foreach (string word in wordsList)
            {
                if (GrammaticalAnalyzer.stopWords.Contains(word))
                    counter++;
            }

            return counter;
        }
    }
}
