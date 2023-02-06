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
                result.Add(new string(word));
                return;
            }

            var currentSymbolUpperCase = char.ToUpper(word[startIndex]);
            var currentSymbolLowerCase = char.ToLower(word[startIndex]);

            AlternateCharCases(word, startIndex + 1, result);

            if (!char.IsLetter(word[startIndex]) || currentSymbolUpperCase == currentSymbolLowerCase)
            {
                return;
            }
            
            word[startIndex] = currentSymbolUpperCase;
            AlternateCharCases(word, startIndex + 1, result);
            word[startIndex] = currentSymbolLowerCase;
        }
    }
}