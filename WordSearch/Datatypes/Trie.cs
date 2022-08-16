namespace Datatypes
{
    public class Trie
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
    }
}