using Datatypes;
using System.Diagnostics;

namespace WordSearch
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> testWords = GenerateTestWords();
            Console.WriteLine($"Generated {testWords.Count} test words");

            Stopwatch watch = Stopwatch.StartNew();
            Trie searchTrie = new Trie();
            foreach (string word in testWords)
            {
                searchTrie.InsertWord(word);
            }
            watch.Stop();
            Console.WriteLine($"Building Search Trie took {watch.Elapsed.TotalMilliseconds} milliseconds");

            string searchString = "AA";
            Console.WriteLine($"Searching for substring {searchString}");

            watch.Restart();
            List<string> searchResult = searchTrie.FindWordsWithPrefix(searchString);
            watch.Stop();

            Console.WriteLine($"Done Searching... Result:");

            foreach (string resultWord in searchResult)
            {
                Console.WriteLine(resultWord);
            }

            Console.WriteLine($"Search took {watch.Elapsed.TotalMilliseconds} milliseconds");
        }

        public static List<string> GenerateTestWords()
        {
            List<string> words = new();

            char[] alphabet = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

            for (int i = 0; i < alphabet.Length; i++)
            {
                for (int j = 0; j < alphabet.Length; j++)
                {
                    for (int k = 0; k < alphabet.Length; k++)
                    {
                        for (int l = 0; l < alphabet.Length; l++)
                        {
                            words.Add($"{alphabet[i]}{alphabet[j]}{alphabet[k]}{alphabet[l]}");
                        }
                    }
                }
            }

            Random random = new();
            List<string> randomizedOrder = words.OrderBy(word => random.Next()).ToList();

            return randomizedOrder;
        }
    }
}