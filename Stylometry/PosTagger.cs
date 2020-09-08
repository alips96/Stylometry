namespace Stylometry
{
    public class PosTagger
    {
        private static OpenNLP.Tools.PosTagger.EnglishMaximumEntropyPosTagger mPosTagger;

        internal static string[] PosTagTokens(string[] tokens)
        {
            if (mPosTagger == null)
            {
                mPosTagger = new OpenNLP.Tools.PosTagger.EnglishMaximumEntropyPosTagger("EnglishPOS.nbin");
            }

            return mPosTagger.Tag(tokens);
        }
    }
}
