using System;

namespace Datatypes
{
    public class Node
    {
        public Dictionary<char, Node> Children { get; set; }
        public bool IsLastCharOfWord { get; set; }

        public Node()
        {
            Children = new Dictionary<char, Node>();
            IsLastCharOfWord = false;
        }
    }

    public class Trie
    {
        public Node RootNode { get; private set; }

        public Trie()
        {
            RootNode = new Node();
        }

        public void InsertWords(List<string> words)
        {
            foreach (string word in words)
            {
                InsertWord(word);
            }
        }

        public void InsertWord(string word)
        {
            Node currentNode = RootNode;

            foreach (char letter in word)
            {
                if (!currentNode.Children.ContainsKey(letter))
                {
                    currentNode.Children.Add(letter, new Node());
                }
                currentNode = currentNode.Children[letter];
            }
            currentNode.IsLastCharOfWord = true;
        }

        public void InsertWordsMultithhreaded(List<string> words)
        {
            const int numThreads = 12;

            int chunkSize = words.Count / numThreads;

            List<Thread> threads = new();
            for (int i = 0; i < numThreads; i++)
            {
                int startIndex = i * chunkSize;
                int endIndex = ((i + 1) * chunkSize) - 1;

                Thread thread = new Thread(() => InsertWordsThreadSafe(startIndex, endIndex, words));
                threads.Add(thread);

                thread.Start();
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }
        }

        private void InsertWordsThreadSafe(int startIndex, int endIndex, List<string> words)
        {
            for (int i = startIndex; i <= endIndex; i++)
            {
                string word = words[i];
                Node currentNode = RootNode;
                foreach (char letter in word)
                {
                    lock (currentNode)
                    {
                        if (!currentNode.Children.ContainsKey(letter))
                        {
                            currentNode.Children.Add(letter, new Node());
                        }
                    }
                    currentNode = currentNode.Children[letter];
                }
                currentNode.IsLastCharOfWord = true;
            }
        }

        public List<string> FindWordsWithPrefix(string prefix)
        {
            Node currentNode = RootNode;

            foreach (char letter in prefix)
            {
                if (!currentNode.Children.ContainsKey(letter))
                {
                    return new List<string>();
                }
                currentNode = currentNode.Children[letter];
            }

            List<string> searchResult = new();

            if (currentNode.IsLastCharOfWord)
            {
                searchResult.Add(prefix);
            }

            GetAllWordsForNodeRecursive(prefix, currentNode, ref searchResult);

            return searchResult;
        }

        private void GetAllWordsForNodeRecursive(string prefix, Node startNode, ref List<string> searchResult)
        {
            foreach (char nodeChar in startNode.Children.Keys)
            {
                Node childNode = startNode.Children[nodeChar];
                if (childNode.IsLastCharOfWord)
                {
                    searchResult.Add(prefix + nodeChar);
                }

                if (!childNode.Children.Any())
                {
                    continue;
                }

                GetAllWordsForNodeRecursive(prefix + nodeChar, startNode.Children[nodeChar], ref searchResult);
            }
        }
    }
}