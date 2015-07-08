using System;
using NLP.TextAnnotation.Objects;

using System.IO;

using System.Text.RegularExpressions;
using NLP.Thesaurus.YARN;


namespace NLP.TextAnnotation
{
    class Program
    {
        static void Main(string[] args)
        {
            //загрузка текста
            var text = new Text(File.ReadAllText("textSample.txt"));
            
            //выделяем предложения
            var sentenses = text.GetSentenses();

            var words = text.GetWords();
            var t = new ThesaurusOnline();
            var m = t.Search("кот");
            //составляем тематические узлы
            //выделяем "главные" предложения
            //составляем аннотацию


        }
    }
}
