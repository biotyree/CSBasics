using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class QuotedFieldTaskTests
    {
        [TestCase("''", 0, "", 2)]
        [TestCase("'a'", 0, "a", 3)]
        [TestCase(@"'", 0, @"", 1, Description = "One single quote")]
        [TestCase(@"""\\""", 0, @"\", 4, Description = "quoted shield")]
        [TestCase(@"""", 0, @"", 1, Description = "One quote")]
        [TestCase(@"some_text 'QF' other_text", 10, @"QF", 4, Description = "without spaces")]
        [TestCase(@"some_text '   QF   ' other_text", 10, @"   QF   ", 10, Description = "with spaces")]
        [TestCase(@"some_text ""other_text", 10, @"other_text", 11, Description = "without end quote")]
        [TestCase(@"some_text 'QF \""' other_text", 10, @"QF """, 7, Description = "in single quote with quote")]
        [TestCase(@"some_text ""QF \"""" other_text", 10, @"QF """, 7, Description = "with shielded quote")]
        [TestCase(@"some_text ""QF \""text\"""" other_text", 10, @"QF ""text""", 13, Description = "with text in shielded quotes")]
        [TestCase(@"some_text 'QF \'' other_text", 10, @"QF '", 7, Description = "in single quote with shielded single quote")]
        [TestCase(@"some_text ""QF \'"" other_text", 10, @"QF '", 7, Description = "with shielded quote")]
        [TestCase(@"some_text ""QF 'text'"" other_text", 10, @"QF 'text'", 11, Description = "with text in single quotes")]
        public void Test(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
        }
    }

    class QuotedFieldTask
    {
        public static Token ReadQuotedField(string line, int startIndex)
        {
            var tokenValue = new StringBuilder();

            var i = startIndex + 1;
            while (i < line.Length)
            {
                if (line[i].Equals(line[startIndex]))
                {
                    i++;
                    break;
                }
                if (line[i].Equals('\\') && i + 1 < line.Length)
                    i++;
                tokenValue.Append(line[i++]);
            }
            return new Token(tokenValue.ToString(), startIndex, i - startIndex);
        }
    }
}
