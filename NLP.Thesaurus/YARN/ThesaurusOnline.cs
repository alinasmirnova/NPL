using System;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;

namespace NLP.Thesaurus.YARN
{
    public class ThesaurusOnline : IThesaurus
    {
        private string GetResponseFromUrl(string url)
        {
            var request = (HttpWebRequest) WebRequest.Create(url);
            request.ContentType = "application/json; charset=utf-8";
            var response = (HttpWebResponse) request.GetResponse();

            var stream = response.GetResponseStream();
            if (stream == null)
            {
                return string.Empty;
            }

            using (var streamReader = new StreamReader(stream))
            {
                return streamReader.ReadToEnd();
            }
            
        }

        public WordInfo Search(string prefix)
        {
            var response = GetResponseFromUrl("http://russianword.net/words.json?q=" + prefix);
            var word = JsonConvert.DeserializeObject<Word[]>(response).FirstOrDefault();

            if (word == null)
            {
                return null;
            }

            return new WordInfo
            {
                Id = word.id,
                Word = word.word,
                IsNoun = word.grammar == "n"
            };
        }

        public SynsetInfo[] Synsets(long id)
        {
            var response = GetResponseFromUrl("http://russianword.net/words/" + id + "/synsets.json");

            return JsonConvert.DeserializeObject<Synset[]>(response)
                              .Select(s => new SynsetInfo {Id = s.id})
                              .ToArray();
        }
    }
}
