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
            GetAllWordsForNodeRecursive(prefix, currentNode, ref searchResult);

            return searchResult;
        }

        private void GetAllWordsForNodeRecursive(string prefix, Node startNode, ref List<string> searchResult)
        {
            foreach (char nodeChar in startNode.Children.Keys)
            {
                if (startNode.Children[nodeChar].IsLastCharOfWord)
                {
                    searchResult.Add(prefix + nodeChar);
                    continue;
                }

                GetAllWordsForNodeRecursive(prefix + nodeChar, startNode.Children[nodeChar], ref searchResult);
            }
        }
    }
}