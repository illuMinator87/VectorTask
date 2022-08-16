namespace WordSearch
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
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