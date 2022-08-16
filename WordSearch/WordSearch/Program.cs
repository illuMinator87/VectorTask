using Datatypes;
using System.Diagnostics;

namespace WordSearch
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> testWords = Util.GenerateDemoWords();
            Console.WriteLine($"Generated {testWords.Count} test words");

            Stopwatch watch = Stopwatch.StartNew();
            Trie searchTrie = new Trie();
            searchTrie.InsertWordsMultithhreaded(testWords);
            watch.Stop();
            Console.WriteLine($"Building Search Trie Multithreaded took {watch.Elapsed.TotalMilliseconds} milliseconds");


            string searchString = "AA";
            Console.WriteLine($"Searching for substring {searchString}");

            watch.Restart();
            List<string> searchResult = searchTrie.FindWordsWithPrefix(searchString);
            watch.Stop();

            Console.WriteLine($"Done Searching... Result:");
            Console.WriteLine($"Search took {watch.Elapsed.TotalMilliseconds} milliseconds");
        }
    }
}