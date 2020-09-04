using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemmaSharp;
using LemmaSharp.Classes;

namespace Stylometry
{
    public class Lemmatizer
    {
        internal static List<string> GetLemmatizedWordsList(List<string> stemmedWordsList, string[] posTags)
        {
            List<string> lemmatizedList = new List<string>();

            for (int i = 0; i < stemmedWordsList.Count; i++)
            {
                string tag = posTags[i];
                string word = stemmedWordsList[i];

                if (tag.CompareTo("VBZ") == 0)
                {
                    var dataFilepath = "full7z-mlteast-en.lem";
                    var stream = File.OpenRead(dataFilepath);

                    var lemmatizer = new LemmaSharp.Classes.Lemmatizer(stream);
                    word = lemmatizer.Lemmatize(word);
                }

                lemmatizedList.Add(word);
            }

            return lemmatizedList;
        }
    }
}
