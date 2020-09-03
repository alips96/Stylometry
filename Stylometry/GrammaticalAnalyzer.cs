using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stylometry
{
    class GrammaticalAnalyzer
    {
        //private string mModelPath = @"C:\Users\ATS\Documents\Visual Studio 2012\Projects\Google_page_speed_json\Google_page_speed_json\bin\Release\";
        private static OpenNLP.Tools.PosTagger.EnglishMaximumEntropyPosTagger mPosTagger;

        internal static int GetNumberOfNouns(List<string> wordsList, int wordCount)
        {
            string[] s = PosTagTokens(wordsList.ToArray());
            return 0;
        }

        private static string[] PosTagTokens(string[] tokens)
        {
            if (mPosTagger == null)
            {
                mPosTagger = new OpenNLP.Tools.PosTagger.EnglishMaximumEntropyPosTagger("EnglishPOS.nbin");
            }

            return mPosTagger.Tag(tokens);
        }
    }
}
