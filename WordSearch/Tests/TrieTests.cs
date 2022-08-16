using Datatypes;
using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
    public class TrieTests
    {
        [Test]
        public void TestInsertAndFindWords()
        {
            List<string> inputWords = new() { "test", "testing", "trie", "trietest", "banana"};

            Trie trie = new();
            foreach (string word in inputWords)
            {
                trie.InsertWord(word);
            }

            List<string> result = trie.FindWordsWithPrefix("test");
            result.Should().HaveCount(2, $"the words 'test' and 'testing' should match the prefix 'test'");
            result[0].Should().Be("test");
            result[1].Should().Be("testing");

            result = trie.FindWordsWithPrefix("tr");
            result.Should().HaveCount(2, $"the words 'trie' and 'trietest' should match the prefix 'tr'");
            result[0].Should().Be("trie");
            result[1].Should().Be("trietest");

            result = trie.FindWordsWithPrefix("b");
            result.Should().HaveCount(1, $"the word 'banana' should match the prefix 'b'");
            result[0].Should().Be("banana");
        }
    }
}