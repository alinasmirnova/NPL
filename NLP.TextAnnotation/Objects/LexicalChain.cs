using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLP.Thesaurus;

namespace NLP.TextAnnotation.Objects
{
    public class LexicalChain
    {
        public bool IsMentioned;
        public int Frequancy;
        public List<SynsetInfo> Synsets;
        public List<Word> Words;

        public LexicalChain()
        {
            Frequancy = 0;
            Synsets = new List<SynsetInfo>();
            Words = new List<Word>();
        }

        public override string ToString()
        {
            var result = "Frequancy = " + Frequancy + " ";
            result += Synsets.Take(Synsets.Count - 1)
                                .Aggregate("{", (s, info) => s + info.Id + ", ");
            result += Synsets.Last().Id + "}";

            result += Words.Take(Words.Count - 1).Aggregate(" ", (s, info) => s + info.Infinitive + ",");
            result += Words.Last().Infinitive;
            return result;
        }
    }
}
