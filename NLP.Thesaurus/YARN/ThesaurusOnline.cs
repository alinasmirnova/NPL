using System;
using System.IO;
using System.Net;

namespace NLP.Thesaurus.YARN
{
    public class ThesaurusOnline : IThesaurus
    {
        private string GetResponseFromUrl(string url)
        {
            var request = (HttpWebRequest) WebRequest.Create(url);
            request.ContentType = "application/json; charset=utf-8";
            var response = (HttpWebResponse) request.GetResponse();

            using (var stream = new StreamReader(response.GetResponseStream()))
            {
                return stream.ReadToEnd();
            }
            
        }

        public WordInfo Search(string prefix)
        {
            var response = GetResponseFromUrl("http://russianword.net/words.json?q=" + prefix);
            Console.WriteLine(response);
            return new WordInfo();
        }
    }
}
