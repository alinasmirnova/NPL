﻿namespace NLP.Thesaurus
{
    public interface IThesaurus
    {
        WordInfo Search(string prefix);
        SynsetInfo[] Synsets(long id);
    }
}
