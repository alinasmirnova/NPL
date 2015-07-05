using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLP.Algorithms.Stemmer
{
    public interface IStemmer
    {
        string Stem(string word);
        string Stem(string word, out PartOfSpeach partOfSpeach);
    }
}
