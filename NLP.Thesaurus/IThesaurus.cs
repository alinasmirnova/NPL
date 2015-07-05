namespace NLP.Thesaurus
{
    public interface IThesaurus
    {
        string[] Search(string prefix, int page = 1);
        Synset[] FindSynsets(string word);
    }
}
