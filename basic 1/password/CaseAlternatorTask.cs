using System;
using System.Collections.Generic;
using System.Linq;

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

        static void AlternateCharCases(char[] word, int startIndex, List<string> result)
        {
            if (startIndex == word.Length)
            {
                var wordToAdd = new string(word);

                if (!result.Contains(wordToAdd))
                    result.Add(wordToAdd);
                result.OrderBy(i => i, System.StringComparer.Ordinal);

                return;
            }
            if (!char.IsLetter(word[startIndex]))
            {
                AlternateCharCases(word, startIndex + 1, result);
            }
            else
            {
                AlternateCharCases(word, startIndex + 1, result);
                word[startIndex] = char.ToUpper(word[startIndex]);
                AlternateCharCases(word, startIndex + 1, result);
            }
        }
    }
}