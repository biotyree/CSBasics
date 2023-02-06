using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace Autocomplete
{
    internal class AutocompleteTask
    {
        /// <returns>
        /// Возвращает первую фразу словаря, начинающуюся с prefix.
        /// </returns>
        /// <remarks>
        /// Эта функция уже реализована, она заработает, 
        /// как только вы выполните задачу в файле LeftBorderTask
        /// </remarks>
        public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return phrases[index];
            
            return null;
        }

        /// <returns>
        /// Возвращает первые в лексикографическом порядке count (или меньше, если их меньше count) 
        /// элементов словаря, начинающихся с prefix.
        /// </returns>
        /// <remarks>Эта функция должна работать за O(log(n) + count)</remarks>
        public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
        {
            // тут стоит использовать написанный ранее класс LeftBorderTask
            var foundPhrases = new List<string>();

            var leftBorder = -1;
            var rightBorder = phrases.Count;
            leftBorder = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, leftBorder, rightBorder);
            if (rightBorder != 0 && ((leftBorder == -1 && phrases[0].StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) || leftBorder != -1))
            {
                var i = 0;
                while (i++ < count && ++leftBorder < rightBorder && phrases[leftBorder].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    foundPhrases.Add(phrases[leftBorder]);
            }
            return foundPhrases.ToArray();
        }

        /// <returns>
        /// Возвращает количество фраз, начинающихся с заданного префикса
        /// </returns>
        public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            // тут стоит использовать написанные ранее классы LeftBorderTask и RightBorderTask
            var rightBorder = phrases.Count;
            return RightBorderTask.GetRightBorderIndex(phrases, prefix, 0, rightBorder) - LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, rightBorder) - 1;
        }
    }



    [TestFixture]
    public class AutocompleteTests
    {
        /*
        [Test]
        public void TopByPrefix_IsEmpty_WhenNoPhrases()
        {
            var actualTopWords = AutocompleteTask.GetTopByPrefix(new List<string>(),"a", 1);
            CollectionAssert.IsEmpty(actualTopWords);
        }

        [Test]
        public void TopByPrefix_IsEmpty_WhenOneSamePhrase()
        {
            var actualTopWords = AutocompleteTask.GetTopByPrefix(new List<string>() { "ba" }, "ba", 1);
            CollectionAssert.IsEmpty(actualTopWords);
        }

        [Test]
        public void TopByPrefix_IsEmpty_WhenOnePriorPhrase()
        {
            var actualTopWords = AutocompleteTask.GetTopByPrefix(new List<string>() { "aa" }, "ba", 1);
            CollectionAssert.AreEqual(new string[] { "aa" }, actualTopWords);
        }

        [Test]
        public void TopByPrefix_IsEmpty_WhenOneSubsequentPhrase()
        {
            var actualTopWords = AutocompleteTask.GetTopByPrefix(new List<string>() { "ca" }, "ba", 1);
            CollectionAssert.IsEmpty(actualTopWords);
        }

        [Test]
        public void TopByPrefix_IsEmpty_WhenNoPriorPhrase()
        {
            var actualTopWords = AutocompleteTask.GetTopByPrefix(new List<string>() { "ba", "bc" }, "ba", 1);
            CollectionAssert.IsEmpty(actualTopWords);
        }

        [Test]
        public void TopByPrefix_IsContentRight()
        {
            var actualTopWords = AutocompleteTask.GetTopByPrefix(new List<string>() { "ab", "ba" }, "ba", 1);
            CollectionAssert.AreEqual(new string[] { "ab" }, actualTopWords);
        }

        [Test]
        public void TopByPrefix_IsEmpty_WhenNoPhrasesBefore()
        {
            var actualTopWords = AutocompleteTask.GetTopByPrefix(new List<string>() { "ba", "ab" }, "ba", 1);
            CollectionAssert.IsEmpty(actualTopWords);
        }

        [Test]
        public void TopByPrefix_IsMultipleContentRight()
        {
            var actualTopWords = AutocompleteTask.GetTopByPrefix(new List<string>() { "ab", "abc", "adf", "aqwoekr", "ba", "zzzazz" }, "ba", 2);
            CollectionAssert.AreEqual(new string[] { "aqwoekr", "adf" }, actualTopWords);
        }
        */
        /*
        Error on: Prefix [z], count 2, phrases [aa, ab, ac] 
result length should be 0, but was 1
Exception on: Prefix [z], count 2, phrases [] 
Index was outside the bounds of the array.
Error on: Prefix [c], count 10, phrases [a, b, c, c, d, e] 
result 0th element should be c, but was b
Error on: Prefix [c], count 1, phrases [a, b, c, c, d, e] 
result 0th element should be c, but was b
Error on: Prefix [b], count 2, phrases [a, bcdef] 
result 0th element should be bcdef, but was a
Error on: Prefix [bc], count 2, phrases [a, bcdef] 
result 0th element should be bcdef, but was a
Error on: Prefix [bcd], count 2, phrases [a, bcdef] 
result 0th element should be bcdef, but was a
Error on: Prefix [bcde], count 2, phrases [a, bcdef] 
result 0th element should be bcdef, but was a
Error on: Prefix [bcdef], count 2, phrases [a, bcdef] 
result 0th element should be bcdef, but was a
Error on: Prefix [bcdef], count 2, phrases [a, bcdef] 
result 0th element should be bcdef, but was a
Error on: Prefix [6], phrases [aa, ab, bc, bd, be, ca, cb] 
result should be 0, but was 1
        
         Error on: Prefix [c], count 10, phrases [a, b, c, c, d, e] 
result length should be 2, but was 4
Error on: Prefix [6], phrases [aa, ab, bc, bd, be, ca, cb] 
result should be 0, but was 1
         
         
         */

        [Test]
        public void TopByPrefix17()
        {
            var actualTopWords = AutocompleteTask.GetTopByPrefix(new List<string>() { "aa", "ab", "bc", "bd", "be", "ca", "cb" }, "6", 10);
            Assert.AreEqual(0, actualTopWords.Length);
        }

        [Test]
        public void TopBy1()
        {
            var actualTopWords = AutocompleteTask.GetTopByPrefix(new List<string>() { }, "z", 2);
            Assert.AreEqual(0, actualTopWords.Length);
        }

        [Test]
        public void TopBy2()
        {
            var actualTopWords = AutocompleteTask.GetTopByPrefix(new List<string>() { "aa", "ab", "ac" }, "z", 2);
            Assert.AreEqual(0, actualTopWords.Length);
        }

        [Test]
        public void TopBy3()
        {
            var actualTopWords = AutocompleteTask.GetTopByPrefix(new List<string>() { "a", "b", "c", "c", "d", "e" }, "c", 10);
            Assert.AreEqual(2, actualTopWords.Length);
        }

        //new one
        [Test]
        public void TopByPrefix1()
        {
            var actualTopWords = AutocompleteTask.GetTopByPrefix(new List<string>() { "aa", "ab", "ac", "bc" }, "a", 2);
            Assert.AreEqual(2, actualTopWords.Length);
        }

        [Test]
        public void TopByPrefix2()
        {
            var actualTopWords = AutocompleteTask.GetTopByPrefix(new List<string>() { "aa", "ab", "ac" }, "a", 2);
            Assert.AreEqual(2, actualTopWords.Length);
        }

        [Test]
        public void TopByPrefix3()
        {
            var actualTopWords = AutocompleteTask.GetTopByPrefix(new List<string>() { "aa", "ab" }, "a", 2);
            Assert.AreEqual(2, actualTopWords.Length);
        }

        [Test]
        public void TopByPrefix4()
        {
            var actualTopWords = AutocompleteTask.GetTopByPrefix(new List<string>() { "aa" }, "a", 2);
            Assert.AreEqual(1, actualTopWords.Length);
        }

        [Test]
        public void TopByPrefix5()
        {
            var actualTopWords = AutocompleteTask.GetTopByPrefix(new List<string>() { "a", "b", "c", "c", "d", "e" }, "a", 2);
            Assert.AreEqual(2, actualTopWords.Length);
        }

        [Test]
        public void TopByPrefxi6()
        {
            var actualTopWords = AutocompleteTask.GetTopByPrefix(new List<string>() { "aa", "bcdef" }, "", 2);
            Assert.AreEqual(2, actualTopWords.Length);
        }

        [Test]
        public void TopByPrefix7()
        {
            var actualTopWords = AutocompleteTask.GetTopByPrefix(new List<string>() { "aa", "bc", "bc", "bd", "be", "ca", "cb" }, "6", 0);
            Assert.AreEqual(1, actualTopWords.Length);
        }

        /*
         Error on: Prefix [a], count 2, phrases [aa, ab, ac, bc] 
result length should be 2, but was 0
Error on: Prefix [a], count 2, phrases [aa, ab, ac] 
result length should be 2, but was 0
Error on: Prefix [a], count 2, phrases [aa, ab] 
result length should be 2, but was 0
Error on: Prefix [a], count 2, phrases [aa] 
result length should be 1, but was 0
Error on: Prefix [c], count 10, phrases [a, b, c, c, d, e] 
result length should be 2, but was 4
Error on: Prefix [], count 2, phrases [a, bcdef] 
result length should be 2, but was 0
Error on: Prefix [6], phrases [aa, ab, bc, bd, be, ca, cb] 
result should be 0, but was 1
         */

        
        [Test]
        public void CountByPrefix()
        {
            var actualCount = AutocompleteTask.GetCountByPrefix(new List<string>() { "ab", "ac", "ba", "be", "bo", "xz", "zzz" }, "b");
            Assert.AreEqual(3, actualCount);
        }

        [Test]
        public void CountByPrefix1()
        {
            var actualCount = AutocompleteTask.GetCountByPrefix(new List<string>() { "a", "b", "c", "c", "d", "e" }, "c");
            Assert.AreEqual(2, actualCount);
        }

        [Test]
        public void CountByPrefix2()
        {
            var actualCount = AutocompleteTask.GetCountByPrefix(new List<string>() { "aa" }, "a");
            Assert.AreEqual(1, actualCount);
        }

        [Test]
        public void CountByPrefix_IsTotalCount_WhenEmptyPrefix()
        {
            var actualCount = AutocompleteTask.GetCountByPrefix(new List<string>() { "ab", "ac", "ba", "be", "bo", "xz", "zzz" }, "");
            Assert.AreEqual(7, actualCount);
        }

        [Test]
        public void CountByPrefix12()
        {
            var actualCount = AutocompleteTask.GetCountByPrefix(new List<string>() { "aa", "bc", "bc", "bd", "be", "ca", "cb" }, "6");
            Assert.AreEqual(1, actualCount);
        }

    }
}
