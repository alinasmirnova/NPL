using System;
using System.IO;
using System.Linq;
using NLP.Algorithms.Stemmer;

namespace NLP.PorterSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("porterTest.txt");
            
            string[] words;
            IStemmer stemmer = new Porter();
            foreach (var line in lines)
            {
                words = line.Split(' ').Where(word1 => !string.IsNullOrWhiteSpace(word1)).ToArray();
                var word = stemmer.Stem(words[0]);
                if (String.Compare(word, words[1], StringComparison.Ordinal) != 0)
                {
                    Console.WriteLine("Result = " + word + " for " + line);
                   word = stemmer.Stem(words[0]);
                }
            }
            Console.WriteLine("success!!!");
            Console.ReadKey();
        }
    }
}
