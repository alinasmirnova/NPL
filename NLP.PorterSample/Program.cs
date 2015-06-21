using System;
using System.IO;
using System.Linq;
using NLP.Preprocessing;

namespace NLP.PorterSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("porterTest.txt");
            string[] words;
            foreach (var line in lines)
            {
                words = line.Split(' ').Where(word1 => !string.IsNullOrWhiteSpace(word1)).ToArray();
                var word = Porter.Stem(words[0]).Stemmed;
                if (String.Compare(word, words[1], StringComparison.Ordinal) != 0)
                {
                    Console.WriteLine("Result = " + word + " for " + line);
                   word = Porter.Stem(words[0]).Stemmed;
                }
            }
            Console.WriteLine("success!!!");
            Console.ReadKey();
        }
    }
}
