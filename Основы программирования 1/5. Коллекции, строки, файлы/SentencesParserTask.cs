using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            return MakeSentencesList(SplitSentences(text));
        }

        public static List<List<string>> MakeSentencesList(string[] sentences)
        {
            var sentencesList = new List<List<string>>();
            foreach (string sentence in sentences)
            {
                List<string> words = MakeListOfWords(sentence);
                if (words.Count > 0)
                    sentencesList.Add(words);
            }
            return sentencesList;
        }

        public static string[] SplitSentences(string text)
        {
            var sentences = new List<string>();
            var separartingSymbols = new char[] { '.', ':', '?', '!', '(', ')', ';' };
            foreach (string sentence in text.ToLower().Split(separartingSymbols).ToArray())
                if (!string.IsNullOrEmpty(sentence)) sentences.Add(sentence);
            return sentences.ToArray();
        }

        public static List<string> MakeListOfWords(string sentence)
        {
            var listOfWords = new List<string>();
            int firstIndexOfWord = -1;

            for (int i = 0; i < sentence.Length; i++)
            {
                if (char.IsLetter(sentence[i]) || sentence[i] == '\'')
                {
                    if (firstIndexOfWord == -1)
                        firstIndexOfWord = i;
                }
                else
                {
                    if (firstIndexOfWord != -1)
                        listOfWords.Add(sentence.Substring(firstIndexOfWord, i - firstIndexOfWord));
                    firstIndexOfWord = -1;
                }
                if (CheckWordIsAllSentence(firstIndexOfWord, sentence, i))
                    listOfWords.Add(sentence.Substring(firstIndexOfWord, sentence.Length - firstIndexOfWord));
            }
            return listOfWords;
        }

        public static bool CheckWordIsAllSentence(int firstIndexOfWord, string sentence, int i)
        {
            return i == sentence.Length - 1 && firstIndexOfWord != -1;
        }
    }
}



/*using System.Collections.Generic;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            return MakeWordsA(SplitSentences(text));
        }
        public static List<string> SplitSentences(string text) //;:()?!
        {
            var sentences = new List<string>();
            var separartingSymbols = new char[] { '.', ':', '?', '!', '(', ')', ';' };
            foreach (var sentence in text.ToLower().Split(separartingSymbols))
                if (!string.IsNullOrEmpty(sentence)) sentences.Add(sentence);
            return sentences;
        }

        public static List<List<string>> MakeWordsA(List<string> sentences)
        {
            var listOfSentences = new List<List<string>>();

            foreach (var sentence in sentences)
            {
                var wordsInSentence = new List<string>();
                var firstLetterInWord = -1;
                for (int i = 0; i < sentence.Length; i++)
                {
                    if (i == sentence.Length - 1 && firstLetterInWord != -1)
                    {
                        wordsInSentence.Add(sentence.Substring(firstLetterInWord, i+1));
                        listOfSentences.Add(wordsInSentence);
                        return listOfSentences;
                    }
                    if ((char.IsLetter(sentence[i]) || sentence[i] == '\'') && firstLetterInWord == -1)
                    {
                        firstLetterInWord = i;
                        continue;
                    }
                    else
                    {
                        if (firstLetterInWord != -1)
                        {
                            if (i != sentence.Length - 1 || char.IsLetter(sentence[i + 1])) continue;
                            wordsInSentence.Add(sentence.Substring(firstLetterInWord, i - firstLetterInWord));
                        }

                        firstLetterInWord = -1;
                    }
                    listOfSentences.Add(wordsInSentence);
                }
            }
            return listOfSentences;
        }
    }
}*/