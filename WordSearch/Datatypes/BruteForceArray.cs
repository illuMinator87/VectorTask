using System.Collections.Concurrent;

namespace Datatypes
{
    public class BruteForceArray
    {
        private List<string> m_wordList;
        private ConcurrentBag<string> m_searchResult;

        public BruteForceArray(List<string> inputWords)
        {
            m_wordList = inputWords;
            m_searchResult = new ConcurrentBag<string>();
        }

        public List<string> FindWordsWithPrefix(string prefix)
        {
            m_searchResult.Clear();
            const int numThreads = 12;

            int chunkSize = m_wordList.Count / numThreads;

            List<Thread> threads = new();
            for (int i = 0; i < numThreads; i++)
            {
                int startIndex = i * chunkSize;
                int endIndex = ((i + 1) * chunkSize) - 1;

                Thread thread = new Thread(() => FindWordsWithPrefixInRange(startIndex, endIndex, prefix));
                threads.Add(thread);

                thread.Start();
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            return m_searchResult.ToList();
        }

        private void FindWordsWithPrefixInRange(int startIndex, int endIndex, string prefix)
        {
            if (m_wordList.Count <= endIndex) return;

            for (int i = startIndex; i <= endIndex; i++)
            {
                if (m_wordList[i].StartsWith(prefix))
                {
                    m_searchResult.Add(m_wordList[i]);
                }
            }
        }
    }
}
