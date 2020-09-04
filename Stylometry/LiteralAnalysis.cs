using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stylometry
{
    public class LiteralAnalysis
    {
        internal static int GetMostCommonWordFrequency(List<string> words)
        {
            Dictionary<string, int> frequencyDic = new Dictionary<string, int>();

            foreach (string item in words)
            {
                if (frequencyDic.ContainsKey(item))
                {
                    frequencyDic[item]++;
                }
                else
                {
                    frequencyDic.Add(item, 1);
                }
            }

            //sort by decending
            var sortedDict = from entry in frequencyDic orderby entry.Value descending select entry;

            return sortedDict.First().Value;
        }

        internal static int GetTagsDiversity(string[] posTags)
        {
            HashSet<string> tagsSet = new HashSet<string>();

            foreach (string item in posTags)
            {
                if (!tagsSet.Contains(item))
                {
                    tagsSet.Add(item);
                }
            }

            return tagsSet.Count;
        }
    }
}
