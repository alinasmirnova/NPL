using System;
using System.Runtime.InteropServices.WindowsRuntime;
using NLP.Algorithms;
using NLP.Algorithms.Stemmer;
using NLP.Thesaurus;

namespace NLP.TextAnnotation.Objects
{
    public class Word
    {
        static IStemmer stemmer = new Porter();

        private string full;
        private string stemmed;
        private PartOfSpeach partOfSpeach;
        private int id;
        private string infinitive;

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

        public int Id
        {
            get { return id; }
        }

        public string Infinitive
        {
            get { return infinitive; }
        }

        public Word(string word)
        {
            full = word;
            partOfSpeach = PartOfSpeach.Unknown;
        }

        /// <summary>
        /// Найти слово в тезаурусе. При этом заполняются поля Infinitive и Id, а так же уточняется часть речи (только для существительных)
        /// </summary>
        /// <param name="thesaurus"></param>
        /// <returns>True, если слово является существительным</returns>
        public bool IsNounFromThesaurus(IThesaurus thesaurus)
        {
            var info = thesaurus.Search(Stemmed);
            if (info == null)
            {
                return false;
            }

            id = info.Id;
            infinitive = info.Word;
            Console.WriteLine(info.Word);
            partOfSpeach = info.IsNoun ? PartOfSpeach.Noun : partOfSpeach;
            return info.IsNoun;
        }

        public override string ToString()
        {
            return Infinitive;
        }
    }
}
