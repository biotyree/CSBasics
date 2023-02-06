using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketGoogle
{
    public class Indexer : IIndexer
    {
        private Dictionary<string, Dictionary<int, List<int>>> positionsByIdsByWords { get; set; }
        private Dictionary<int, List<string>> wordsById = new Dictionary<int, List<string>>();
        readonly static HashSet<char>  separators = new HashSet<char> { ' ', '.', ',', '!', '?', ':', '–', '\r', '\n' };
        

        public Indexer()
        {
            positionsByIdsByWords = new Dictionary<string, Dictionary<int, List<int>>>();
            wordsById = new Dictionary<int, List<string>>();
        }

        public void Add(int id, string documentText)
        {
            if (documentText.Length > 0)
            {
                var position = 0;
                while (position < documentText.Length)
                {
                    position = GetWordStartIndex(position, separators, documentText);
                    var word = GetWord(position, separators, documentText);
                    AddPositionByIdByWord(word, id, position);

                    position += word.Length + 1;
                }
            }
        }

        public List<int> GetIds(string word)
        {
            if (positionsByIdsByWords.ContainsKey(word))
                return positionsByIdsByWords[word].Keys.ToList();

            return new List<int>();
        }

        public List<int> GetPositions(int id, string word)
        {
            if (positionsByIdsByWords.ContainsKey(word) && positionsByIdsByWords[word].ContainsKey(id))
                return positionsByIdsByWords[word][id];

            return new List<int>();
        }

        public void Remove(int id)
        {
            if(wordsById.ContainsKey(id))
            {
                foreach (var word in wordsById[id])
                {
                    positionsByIdsByWords[word].Remove(id);
                }

                wordsById.Remove(id);
            }
        }

        private int GetWordStartIndex(int i, HashSet<char> separators, string documentText)
        {
            while (i < documentText.Length && separators.Contains(documentText[i]))
                i++;

            return i;
        }

        private string GetWord(int i, HashSet<char> separators, string documentText)
        {
            var j = 0;
            var wordBuilder = new StringBuilder();
            while (i + j < documentText.Length && !separators.Contains(documentText[i + j]))
                wordBuilder.Append(documentText[i + j++]);

            return wordBuilder.ToString();
        }

        private void AddPositionByIdByWord(string word, int id, int position)
        {
            if (positionsByIdsByWords.ContainsKey(word))
            {
                if (positionsByIdsByWords[word].ContainsKey(id))
                    positionsByIdsByWords[word][id].Add(position);
                else
                    positionsByIdsByWords[word][id] = new List<int> { position };
            }
            else
            {
                positionsByIdsByWords[word] = new Dictionary<int, List<int>>
                        {
                            { id, new List<int>() { position } }
                        };
            }

            AddWordById(id, word);
        }

        private void AddWordById(int textId, string word)
        {
            if (wordsById.ContainsKey(textId))
                wordsById[textId].Add(word);
            else
                wordsById[textId] = new List<string> { word };
        }
    }
}