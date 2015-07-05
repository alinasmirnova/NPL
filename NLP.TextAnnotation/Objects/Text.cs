using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
    }
}
