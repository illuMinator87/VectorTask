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
            // Uses the single threaded method, since input words are less than amount of threads
            List<string> inputWords = new() { "test", "testing", "trie", "trietest", "banana"};

            Trie trie = new();
            trie.InsertWordsMultithhreaded(inputWords);

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

            // Uses the multithreaded insert, but has only slightly more words than threads
            // This is supposed to check if the words are correctly split up between threads
            inputWords = new() { "Lorem", "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut" };
            trie = new();
            trie.InsertWordsMultithhreaded(inputWords);

            result = trie.FindWordsWithPrefix("Lorem"); //First word
            result.Should().HaveCount(1, $"the prefix 'Lorem' exists only once");
            result[0].Should().Be("Lorem");

            result = trie.FindWordsWithPrefix("ut"); //Last word
            result.Should().HaveCount(1, $"the prefix 'ut' exists only once");
            result[0].Should().Be("ut");

            result = trie.FindWordsWithPrefix("s"); //First word
            result.Should().HaveCount(3, $"there are 3 words that start with s");
        }

        [Test]
        public void TestInsertAndFindWithDemoData()
        {
            List<string> demoWords = Util.GenerateDemoWords();

            Trie trie = new Trie();
            trie.InsertWordsMultithhreaded(demoWords);

            List<string> result = trie.FindWordsWithPrefix("A");
            result.Should().HaveCount((int)Math.Pow(26, 3), $"the result should be 'A' plus all possible combinations for the last 3 letters: {(int)Math.Pow(26, 3)}");
            result.Contains("ABCD").Should().BeTrue();
            result.Contains("BAAA").Should().BeFalse();

            result = trie.FindWordsWithPrefix("BB");
            result.Should().HaveCount((int)Math.Pow(26, 2), $"the result should be 'BB' plus all possible combinations for the last 2 letters: {(int)Math.Pow(26, 2)}");
            result.Contains("BBCD").Should().BeTrue();
            result.Contains("BAAA").Should().BeFalse();

            result = trie.FindWordsWithPrefix("CCC");
            result.Should().HaveCount(26, $"the result should be 'CCC' plus one word for each of the 26 letters in the alphabet.");
            result.Contains("CCCD").Should().BeTrue();
            result.Contains("CCAA").Should().BeFalse();
        }
    }
}