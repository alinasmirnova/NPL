using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLP.Thesaurus.YARN
{
    public class Synset
    {
        public int id { get; set; }
        public int author_id { get; set; }
        public object approver_id { get; set; }
        public object approved_at { get; set; }
        public int revision { get; set; }
        public object deleted_at { get; set; }
        public DateTime updated_at { get; set; }
        public int[] words_ids { get; set; }
        public object default_definition_id { get; set; }
        public object default_synset_word_id { get; set; }
    }
}
