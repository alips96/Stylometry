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

        /// <summary>
        /// Here we add all the text belonging to each author and then tokenize and divide it inot to separate 
        /// train and test lists.
        /// </summary>
        /// <param name="authorsList"></param>
        /// <returns></returns>
        internal static List<Tokenizer> TokenizeEveryText(List<Author> authorsList)
        {
            List<Tokenizer> myList = new List<Tokenizer>();

            //List<string> trainAndTestList = GetTrainAndTestList(0); //passing the first item of the list
            Dictionary<int, string> authorsDic = GetAuthorsDic(authorsList); //Gathering all the text.

            return myList;
        }

        private static Dictionary<int, string> GetAuthorsDic(List<Author> authorsList)
        {
            Dictionary<int, string> myDic = new Dictionary<int, string>();

            myDic.Add(authorsList[0].Id, authorsList[0].Text);

            for (int i = 1; i < authorsList.Count; i++)
            {
                if(authorsList[i].Id.CompareTo(authorsList[i - 1].Id) == 0)
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

        private static List<string> GetTrainAndTestList(int index)
        {
            throw new NotImplementedException();
        }
    }
}
