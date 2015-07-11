 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
 using NLP.Algorithms;
 using NLP.Thesaurus;

namespace NLP.TextAnnotation.Objects
{
    public class Text
    {
        private string text;

        public Text(string text)
        {
            this.text = string.Join(" ", Regex.Split(text, @"(?:\r\n|\n|\r)")); ;
        }

        public Sentense[] GetSentenses()
        {
            return Regex.Matches(text, @"([А-ЯA-Z]((т.п.|т.д.|пр.)|[^?!.\(]|\([^\)]*\))*[.?!])")
                        .Cast<Match>()
                        .Select(s => new Sentense(s.Value))
                        .ToArray();
        }

        public string[] GetWords()
        {
            return Regex.Matches(text.ToLower(), @"([А-Яа-я\-])*")
                        .Cast<Match>()
                        .Select(w => w.Value)
                        .Where(w => !string.IsNullOrWhiteSpace(w))
                        .ToArray();
        }

        public List<Word> GetNounList(IThesaurus thesaurus)
        {
            return GetWords()
                .Select(w => new Word(w))
                .Where(w => w.PartOfSpeach == PartOfSpeach.Noun)
                .Where(w => w.IsNounFromThesaurus(thesaurus))
                .ToList();
        } 

        public LexicalChain[] GetLexicalChains(IThesaurus thesaurus)
        {
            var chains = new List<LexicalChain>();
            var nouns = GetNounList(thesaurus);

            for (int i = 0; i < nouns.Count; i++)
            {
                var synsets = thesaurus.Synsets(nouns[i].Id);
                int maxComNum = 0;
                int maxComIndex = -1;
                
                for (int j = 0; j < chains.Count; j++)
                {
                    int curComNum = chains[i].Synsets.Count(s => 
                        synsets.Count(syn =>  syn.Id == s.Id) > 0
                        );

                    if (curComNum > maxComNum)
                    {
                        maxComNum = curComNum;
                        maxComIndex = j;
                    }
                }

                if (maxComIndex > -1)
                {
                    chains[maxComIndex].Synsets = chains[maxComIndex].Synsets.Where(s => 
                        synsets.Count(syn => syn.Id == s.Id) > 0
                        ).ToList();
                }
                else
                {
                    maxComIndex = chains.Count;
                    chains.Add(new LexicalChain());
                    chains[maxComIndex].Synsets = synsets.ToList();
                }

                chains[maxComIndex].Words.Add(nouns[i]);
                var infinitive = nouns[i].Infinitive;

                chains[maxComIndex].Frequancy += nouns.Count(word => string.CompareOrdinal(word.Infinitive, infinitive) == 0);
                var removed = nouns.RemoveAll(word => string.CompareOrdinal(word.Infinitive, infinitive) == 0);
                i--;
            }
            return chains.ToArray();

        }
    }
}
