using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class FieldParserTaskTests
    {
        public static void Test(string input, string[] expectedResult)
        {
            var actualResult = FieldsParserTask.ParseLine(input);
            Assert.AreEqual(expectedResult.Length, actualResult.Count);
            for (int i = 0; i < expectedResult.Length; ++i)
            {
                Assert.AreEqual(expectedResult[i], actualResult[i].Value);
            }
        }

        [TestCase("a", new[] { "a" })]
        [TestCase(" a", new[] { "a" })]
        [TestCase("a b", new[] { "a", "b" })]
        [TestCase("\" \"", new[] { " " })]
        [TestCase("a\"b\"", new[] { "a", "b" })]
        [TestCase("\"\'\'\"", new[] { "\'\'" })]
        [TestCase("\"\\\"", new[] { "\"" })]
        [TestCase("\'\\\'", new[] { "\'" })]
        [TestCase("\'a\'b", new[] { "a", "b" })]
        [TestCase("a  b", new[] { "a", "b" })]
        [TestCase("\"\\\\ \"", new[] { "\\ " })]
        [TestCase("\"\\\\\"", new[] { "\\" })]
        [TestCase("\'  ", new[] { "  " })]
        [TestCase("\'\'", new[] { "" })]
        [TestCase("\'\"\"\'", new[] { "\"\"" })]
        [TestCase("", new string[] { })]

        public static void RunTests(string input, string[] expectedOutput)
        {
            Test(input, expectedOutput);
        }
    }

    public class FieldsParserTask
    {
        public static List<Token> ParseLine(string line)
        {
            var tokenList = new List<Token>();
            var newElementIndex = 0;

            var i = 0;
            while (i < line.Length)
            {
                if (("\'\"".ToCharArray().Contains(line[i])))
                {
                    tokenList.Add(ReadQuotedField(line, i));
                    i = tokenList[-1].GetIndexNextToToken();
                }
                if (i < line.Length && line[i] == ' ')
                    i = GetNextNotSpaceIndex(line, i);
                else if (i < line.Length)
                {
                    tokenList.Add(ReadField(line, i));
                    newElementIndex = tokenList.Count - 1;
                    i = tokenList[newElementIndex].GetIndexNextToToken();
                }
            }

            return tokenList;
        }

        private static int GetNextNotSpaceIndex(string line, int startIndex)
        {
            var i = startIndex;

            while (i < line.Length && line[i] == ' ')
                i++;

            return i;
        }

        private static Token ReadField(string line, int startIndex)
        {
            var i = startIndex - 1;
            while (++i < line.Length)
                if (line[i] == ' ' || "\"\'".ToCharArray().Contains(line[i]))
                    break;

            return new Token(line.Substring(startIndex, i - startIndex), startIndex, i - startIndex);
        }

        public static Token ReadQuotedField(string line, int startIndex)
        {
            return QuotedFieldTask.ReadQuotedField(line, startIndex);
        }
    }
}