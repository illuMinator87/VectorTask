namespace Datatypes
{
    public class Node
    {
        public Dictionary<char, Node> Children { get; set; }
        public bool IsLastCharOfWord { get; set; }
        public Mutex Mutex { get; set; }

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
    }
}