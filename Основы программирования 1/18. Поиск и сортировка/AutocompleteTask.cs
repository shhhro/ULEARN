using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Autocomplete
{
    internal class AutocompleteTask
    {

        public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;

            if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return phrases[index];
            return null;
        }

        public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int wordsStartWithPrefix)
        {
            int startingIndex = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            int wordsByPrefixCount = GetCountOfWordsByPrefix(phrases, prefix);

            if (wordsByPrefixCount < wordsStartWithPrefix) wordsStartWithPrefix = wordsByPrefixCount;
            string[] allWordsWithPrefix = new string[wordsStartWithPrefix];
            for (int i = 0; i < wordsStartWithPrefix; i++)
                allWordsWithPrefix[i] = phrases[startingIndex + i];
            return allWordsWithPrefix;
        }

        public static int GetCountOfWordsByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            int leftBorder = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);
            int rightBorder = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count);
            return rightBorder - leftBorder - 1;
        }
    }
}