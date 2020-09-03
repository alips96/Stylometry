using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stylometry
{
    public class StructuralAnalyzer
    {
        internal static int CountWords(List<string> wordsList)
        {
            return wordsList.Count;
        }

        internal static int GetAverageLetterCount(List<string> wordsList, int wordCount)
        {
            int counter = 0;

            foreach (string word in wordsList)
            {
                counter += word.Length;
            }

            return counter / wordCount;
        }
    }
}
