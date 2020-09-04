using System;
using System.Collections.Generic;
using Iveonik.Stemmers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stylometry
{
    public class Stemmer
    {
        internal static List<string> GetStemmedWordsList(List<string> wordsList)
        {
            var stemmer = new EnglishStemmer();
            List<string> stemmedList = new List<string>();

            foreach (var item in wordsList)
            {
                stemmedList.Add(stemmer.Stem(item));
            }

            return stemmedList;
        }
    }
}
