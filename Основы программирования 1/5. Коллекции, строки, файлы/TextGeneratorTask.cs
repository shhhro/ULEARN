using System.Collections.Generic;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(Dictionary<string, string> nextWords, string phraseBeginning, int wordsCount)
        {
            string phrase = phraseBeginning;
            
            if (wordsCount == 0 || nextWords.Count == 0) return phrase;

            if (!phraseBeginning.Contains(" "))
            {
                if (nextWords.ContainsKey(phraseBeginning))
                {
                    wordsCount--;
                    phrase = phraseBeginning + " " + nextWords[phraseBeginning];
                }
                else
                    return phrase;
            }
            return CombinePartsOfPhrase(phrase, nextWords, wordsCount);
            
        }
        public static string CombinePartsOfPhrase(string phrase, Dictionary<string, string> nextWords, int wordsCount)
        {
            for (int i = 0; i < wordsCount; i++)
            {
                string[] words = phrase.Split(' ');
                var countOfWords = words.Length;
                var lastWord = words[countOfWords - 1];
                var lastTwoWords = words[countOfWords - 2] + " " + words[countOfWords - 1];
                if (countOfWords >= 2 && nextWords.ContainsKey(lastTwoWords))
                    phrase += " " + nextWords[lastTwoWords];

                else if (countOfWords >= 1 && nextWords.ContainsKey(lastWord))
                    phrase += " " + nextWords[lastWord];
            }
            return phrase;
        }
    }
}
