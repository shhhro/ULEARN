using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var biGramms = GetListOfNGramms(text, 2, 1);
            var triGramms = GetListOfNGramms(text, 3, 2);

            var frequencyOfNgramms = new Dictionary<string, string>();
            AddMostFrequentWords(frequencyOfNgramms, biGramms);
            AddMostFrequentWords(frequencyOfNgramms, triGramms);

            return frequencyOfNgramms;
        }
        public static string GetKeyForNGramms(int nGramm, List<string> sentence, int index)
        {
            if (nGramm == 3) 
                return sentence[index] + " " + sentence[index + 1];
            return sentence[index];
        }
        public static Dictionary<string, Dictionary<string, int>> GetListOfNGramms(List<List<string>> text, int n, int indexOfLastElement)
        {
            Dictionary<string, Dictionary<string, int>> nGramms = new Dictionary<string, Dictionary<string, int>>();
            foreach (var sentence in text)
            {
                for (int index = 0; index < sentence.Count - indexOfLastElement; index++)
                {
                    if (!nGramms.ContainsKey(GetKeyForNGramms(n, sentence, index)))
                        nGramms.Add(GetKeyForNGramms(n, sentence, index), new Dictionary<string, int>());

                    if (!nGramms[GetKeyForNGramms(n, sentence, index)].ContainsKey(sentence[index + indexOfLastElement]))
                        nGramms[GetKeyForNGramms(n, sentence, index)].Add(sentence[index + indexOfLastElement], 1);

                    nGramms[GetKeyForNGramms(n, sentence, index)][sentence[index + indexOfLastElement]] += 1;
                }
            }
            return nGramms;
        }

        public static void AddMostFrequentWords(Dictionary<string, string> frequencyOfNgramms,Dictionary<string, Dictionary<string, int>> nGramms)
        {
            foreach (var firstPartOfNgramm in nGramms)
            {
                var maxCountOfNgramms = 0;
                var mostFrequentWord = "";
                foreach (var secondWord in firstPartOfNgramm.Value)
                {
                    if (secondWord.Value > maxCountOfNgramms)
                    {
                        maxCountOfNgramms = secondWord.Value;
                        mostFrequentWord = secondWord.Key;
                    }
                    if (secondWord.Value == maxCountOfNgramms)
                    {
                        if (string.CompareOrdinal(mostFrequentWord, secondWord.Key) > 0)
                        {
                            mostFrequentWord = secondWord.Key;
                        }
                    }
                }
                frequencyOfNgramms.Add(firstPartOfNgramm.Key,mostFrequentWord);
            }
        }
    }
}