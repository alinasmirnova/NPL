using System;

namespace NLP.Thesaurus.YARN
{
    public class Word
    {
        public int id { get; set; }
        public int? author_id { get; set; }
        public object approver_id { get; set; }
        public object approved_at { get; set; }
        public DateTime updated_at { get; set; }
        public string word { get; set; }
        public string grammar { get; set; }
        public object deleted_at { get; set; }
        public int revision { get; set; }
        public object[] accents { get; set; }
        public string[] uris { get; set; }
        public float frequency { get; set; }
        public int rank { get; set; }
    }
}
