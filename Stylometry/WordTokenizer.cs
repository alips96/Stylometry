using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.MachineLearning;

namespace Stylometry
{
    public class WordTokenizer
    {
        internal static List<string> TokenizeWords(string sentence)
        {
            char[] splitTokens = new char[] { ' ', ':', '(', ')', '.', '!', '?', ',', '*', '{', '}', '[', ']' };
            List<string> words = sentence.Split(splitTokens, StringSplitOptions.RemoveEmptyEntries).ToList();
            words = FixFloatNumbers(words);
            return words;
        }

        private static List<string> FixFloatNumbers(List<string> words)
        {
            List<string> newList = new List<string>();


            for (int i = 0; i < words.Count - 1; i++)
            {
                if (char.IsDigit(words[i][0]) && char.IsDigit(words[i + 1][0]))
                {
                    newList.Add(words[i] + "." + words[i + 1]);
                    i++;
                }
                else
                {
                    newList.Add(words[i].ToLower());
                }
            }

            newList.Add(words[words.Count - 1].ToLower());

            return newList;
        }
    }
}
