using System;
using System.Collections.Generic;

namespace Passwords
{
    public class CaseAlternatorTask
    {
        public static List<string> AlternateCharCases(string lowercaseWord)
        {
            var result = new List<string>();
            AlternateCharCases(lowercaseWord.ToCharArray(), 0, result);
            return result;
        }

        static void AlternateCharCases(char[] word, int index, List<string> result)
        {
            if (index == word.Length)
            {
                if (!result.Contains(new string(word)))
                    result.Add(new string(word));
                return;
            }
            if (!char.IsLetter(word[index]))
                AlternateCharCases(word, index + 1, result);
            else
            {
                word[index] = char.ToLower(word[index]);
                AlternateCharCases(word, index + 1, result);
                word[index] = char.ToUpper(word[index]);
                AlternateCharCases(word, index + 1, result);
            }    
        }
    }
}