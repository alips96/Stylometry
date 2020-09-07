using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stylometry
{
    public class Tokenizer
    {
        public int AuthorId { get; set; }
        public List<string> TrainTokens { get; set; }
        public List<string> TestTokens { get; set; }

        public Tokenizer(int _authorId, List<string> _TrainTokens, List<string> _TestTokens)
        {
            AuthorId = _authorId;
            TrainTokens = _TrainTokens;
            TestTokens = _TestTokens;
        }

        /// <summary>
        /// Here we add all the text belonging to each author and then tokenize and divide it into to separate 
        /// train and test lists.
        /// </summary>
        /// <param name="authorsList"></param>
        /// <returns></returns>
        internal static List<Tokenizer> TokenizeEveryText(List<Author> authorsList)
        {
            List<Tokenizer> myList = new List<Tokenizer>();

            Dictionary<int, string> authorsDic = GetAuthorsDic(authorsList); //Gathering all the text.

            foreach (KeyValuePair<int, string> item in authorsDic)
            {
                List<string>[] trainAndTestArr = GetTrainAndTestList(item);

                myList.Add(new Tokenizer(item.Key, trainAndTestArr[0], trainAndTestArr[1]));
            }

            return myList;
        }

        private static List<string>[] GetTrainAndTestList(KeyValuePair<int, string> item)
        {
            List<string> trainTokens = new List<string>();
            List<string> testTokens = new List<string>();
            List<string>[] returnList = new List<string>[2];

            List<string> allTokens = item.Value.Split(new string[] { ". " }, StringSplitOptions.RemoveEmptyEntries).ToList();

            float splitIndex = 0.7f * allTokens.Count;

            for (int i = 0; i < allTokens.Count; i++)
            {
                if (i < splitIndex)
                {
                    trainTokens.Add(allTokens[i]);
                }
                else
                {
                    testTokens.Add(allTokens[i]);
                }
            }

            returnList[0] = trainTokens;
            returnList[1] = testTokens;

            return returnList;
        }

        private static Dictionary<int, string> GetAuthorsDic(List<Author> authorsList)
        {
            Dictionary<int, string> myDic = new Dictionary<int, string>
            {
                { authorsList[0].Id, authorsList[0].Text }
            };

            for (int i = 1; i < authorsList.Count; i++)
            {
                if (authorsList[i].Id.CompareTo(authorsList[i - 1].Id) == 0)
                {
                    myDic[authorsList[i].Id] += " " + authorsList[i].Text;
                }
                else
                {
                    myDic.Add(authorsList[i].Id, authorsList[i].Text);
                }
            }

            return myDic;
        }
    }
}
