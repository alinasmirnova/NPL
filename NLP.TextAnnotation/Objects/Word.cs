using NLP.Algorithms;
using NLP.Algorithms.Stemmer;

namespace NLP.TextAnnotation.Objects
{
    public class Word
    {
        static IStemmer stemmer = new Porter();

        private string full;
        private string stemmed;
        private PartOfSpeach partOfSpeach;

        public string Full
        {
            get { return full; }
        }

        public string Stemmed
        {
            get
            {
                if (stemmed == null)
                {
                    stemmed = stemmer.Stem(full);
                }
                return stemmed;
            }
        }

        public PartOfSpeach PartOfSpeach
        {
            get
            {
                if (partOfSpeach == PartOfSpeach.Unknown)
                {
                    stemmed = stemmer.Stem(full, out partOfSpeach);
                }
                return partOfSpeach;
            }
        }

        public Word(string word)
        {
            full = word;
            partOfSpeach = PartOfSpeach.Unknown;
        }
    }
}
